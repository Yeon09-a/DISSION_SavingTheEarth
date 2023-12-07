using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerDir
{ // ĳ���� ���� ����
    Up,
    Down,
    Right,
    Left
};

public enum MapName
{ // �� ������
    SaveTitle,
    Title,
    BaseMap,
    SeaMap,
    Test
}

public class Player : Character
{
    public MonsterDic monDic;
    
    public RuntimeAnimatorController weaponAnimatorController;

    public PlayerDir playerDir = PlayerDir.Down; // �÷��̾� ���� ����

    private RaycastHit2D hit; // ����ĳ��Ʈ �ᱣ���� �����ϱ� ���� ����ü ����
    private Vector3 interPos; // ��ȣ�ۿ��� ����(���� �߻��� ����)
    private float rayLength; // ���� ����

    public Action OpenShop; // ������ ���� �׼�
    public Action OpenFarmTool; // ��� ���� UI ����
    public Action CloseFarmTool; // ��� ���� UI �ݱ�
    public Action OpenBox; // �ڽ� ����
    public Action<int> ChangeFarmTool; // ��� ���� �ٲٱ�
    public Action<int> ChangeCurTool; // ��� ���� Num
    public PlayerFarm playerFarm;
    public Func<float, float, float, float> UpdatePlayerHp;

    GameObject scanObject; // ���̿� �浹�� ������Ʈ ����

    public BoxCollider2D moveCollider;

    public DialogManager dialogManager;
    public QuestManager questManager;


    protected override void Start()
    {
        base.Start();

        if (GameManager.instance.curMap == MapName.SeaMap)
        {
            myAnimator.runtimeAnimatorController = weaponAnimatorController;
            Camera.main.transform.localPosition = new Vector3(0, 2.12f, -10);

            if (myRigidbody != null)
            {
                myRigidbody.gravityScale = 10f;
            }

            // MoveCollider ������Ʈ�� �ڽ� �ݶ��̴��� ã�� ��Ȱ��ȭ
            if (moveCollider != null)
            {
                if (moveCollider != null)
                {
                    moveCollider.enabled = false;
                }
            }
        }

        if (GameManager.instance.preMap == MapName.Title)
        {
            transform.position = new Vector3(12.63f, 3.3f, 0);
            scanObject = GameObject.Find("Start");
            Debug.Log(scanObject.name);
            dialogManager.SenceObject(scanObject);
            dialogManager.Talk();
        }
        else if (GameManager.instance.preMap == MapName.SaveTitle)
        {
            transform.position = DataManager.instance.nowPlayerData.playerPos;
        }
        else if (GameManager.instance.preMap == MapName.BaseMap)
        {
            transform.position = new Vector3(-0.02f, -3.16f, 0);
            GetComponent<CapsuleCollider2D>().isTrigger = false;

            scanObject = GameObject.Find("Start_sea"); // SeaMap ���� ���
            Debug.Log(scanObject.name);
            dialogManager.SenceObject(scanObject);
            dialogManager.Talk();

            questManager.questId = 60; // questId ���� ����
        }
        else if (GameManager.instance.preMap == MapName.SeaMap)
        {
            GetComponent<CapsuleCollider2D>().isTrigger = true;
            myRigidbody.gravityScale = 0f;
            moveCollider.enabled = true;
        } else if (GameManager.instance.preMap == MapName.Test)
        {
            transform.position = new Vector3(137.82f, 0.11f, 0);
            GetComponent<CapsuleCollider2D>().isTrigger = false;
        }
    }

    // override�� ��ӹ��� Ŭ������ �޼ҵ� �߿��� virtual�� ����� �κ��� ������
    protected override void Update()
    {
        GetInput();
        // base�� ��ӹ��� Ŭ������ ����� ����Ŵ
        base.Move();
        // Ű���� ��ȣ�ۿ� 
        if (Input.GetKeyDown(KeyCode.E)) // ��ȣ�ۿ� Ű
        {


            if (playerDir == PlayerDir.Down) // ������ �Ʒ��� ���
            {
                interPos = Vector3.down;
                rayLength = 1.1f;
            }
            else if (playerDir == PlayerDir.Up) // ������ ���� ���
            {
                interPos = Vector3.up;
                rayLength = 1.1f;
            }
            else if (playerDir == PlayerDir.Right) // ������ �������� ���
            {
                interPos = Vector3.right;
                rayLength = 1.0f;
            }
            else if (playerDir == PlayerDir.Left) // ������ ������ ���
            {
                interPos = Vector3.left;
                rayLength = 1.0f;
            }

            hit = Physics2D.Raycast(transform.position, interPos, rayLength, 1 << 6); // ���� �߻�


            // hit = Physics2D.Raycast(transform.position, interPos, rayLength, 1 << 6); // ���� �߻�
            //Debug.DrawRay(transform.position, interPos * rayLength, Color.red);
            if (hit.collider != null) // �浹�� ������Ʈ�� ���� ���
            {
                // ���⿡�� ��ȣ�ۿ�
                // hit.collider�� ���̿� �浹�� ������Ʈ
                //scanObject = hit.collider.gameObject;
                scanObject = hit.collider.gameObject;
                dialogManager.SenceObject(scanObject);

                if (scanObject.CompareTag("Door"))
                {
                    if (scanObject.name.Equals("OutDoor") && questManager.questId >= 50)
                    {
                        GameManager.instance.curMap = MapName.SeaMap;
                        GameManager.instance.preMap = MapName.BaseMap;

                        SceneLoadingManager.LoadScene("SeaMap");
                    }
                    else if (scanObject.name.Equals("GHDoor") && (questManager.questId == 20 || questManager.questId >= 40))
                    {
                        transform.position = new Vector3(0.074f, 46.486f, 0);
                        playerFarm.enabled = true;
                        OpenFarmTool();
                    }
                    else if (scanObject.name.Equals("MainDoor"))
                    {
                        transform.position = new Vector3(0.754f, 2.722f, 0);
                        playerFarm.enabled = false;
                        CloseFarmTool();
                    }
                }
                else if (scanObject.CompareTag("FarmTool"))
                {
                    if (scanObject.name.Equals("Hoe"))
                    {
                        ChangeFarmTool(1);
                        ChangeCurTool(1);
                    }
                    else if (scanObject.name.Equals("Water"))
                    {
                        ChangeFarmTool(2);
                        ChangeCurTool(2);
                    }
                    else if (scanObject.name.Equals("Basket"))
                    {
                        ChangeFarmTool(3);
                        ChangeCurTool(3);
                    }
                }
                else if (scanObject.name.Equals("Box"))
                {
                    OpenBox();
                }
            }
            else
            {
                scanObject = null;
            }

            dialogManager.Talk();

        }
        else if (Input.GetKeyDown(KeyCode.F))
        {

            if (GameManager.instance.curMap != MapName.BaseMap)
            {
                if (playerDir == PlayerDir.Right) // ������ �������� ���
                {
                    myAnimator.SetTrigger("AttackRight");
                    speed = 0;
                }
                else if (playerDir == PlayerDir.Left) // ������ ������ ���
                {
                    myAnimator.SetTrigger("AttackLeft");
                    speed = 0;
                }
            }
        }
        HandleLayers();
    }

    // Ű���� �Է°��� ���� (���� �� ���� ����)
    public void GetInput()
    {
        Vector2 moveVector;

        // Input.GetAxisRaw(): ����, ���� ��ư �Է½ÿ� -1f, 1f ��ȯ, �������� ���� 0f ��ȯ
        moveVector.x = Input.GetAxisRaw("Horizontal"); // AŰ or ���� ȭ��ǥ -1f, DŰ or ������ ȭ��ǥ 1f �� ��ȯ
        moveVector.y = Input.GetAxisRaw("Vertical"); // SŰ or �Ʒ��� ȭ��ǥ -1f, WŰ or ���� ȭ��ǥ 1f �� ��ȯ

        direction = moveVector;

        // �÷��̾��� �ִϸ��̼ǿ� ���� ���� ���� ����
        if (myAnimator.GetCurrentAnimatorStateInfo(1).IsName("walk_Up"))
        {
            playerDir = PlayerDir.Up;
        }
        else if (myAnimator.GetCurrentAnimatorStateInfo(1).IsName("walk_Down"))
        {
            playerDir = PlayerDir.Down;
        }
        else if (myAnimator.GetCurrentAnimatorStateInfo(1).IsName("walk_Right"))
        {
            playerDir = PlayerDir.Right;
        }
        else if (myAnimator.GetCurrentAnimatorStateInfo(1).IsName("walk_Left"))
        {
            playerDir = PlayerDir.Left;
        }


        // �޸���
        if (Input.GetKey(KeyCode.LeftShift))
            speed = 5;
        else
            speed = 3;

        // ��ȭ�� �̵� ����
        if (dialogManager.isTalk)
            speed = 0;
    }

    // ���콺 ��ȣ�ۿ� �߰��ϱ�

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MonsterAttack"))
        {
            MonsterAttack ma = collision.GetComponent<MonsterAttack>();
            float attack = monDic.monsters[ma.id].damage[ma.num];
            hp = UpdatePlayerHp(fullHp, hp, attack);

            if (hp != -1f)
            {
                StartCoroutine(BeAttacked());
            } else
            {
                Debug.Log("���� ����");
            }


        }
    }

    IEnumerator BeAttacked()
    {
        transform.GetComponent<SpriteRenderer>().color = new Color(255 / 255f, 133 / 255f, 133 / 255f);
        yield return new WaitForSeconds(0.5f);
        transform.GetComponent<SpriteRenderer>().color = Color.white;
    }

}