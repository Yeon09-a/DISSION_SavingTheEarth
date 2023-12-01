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
    SeaMap
}

public class Player : Character
{

    public PlayerDir playerDir = PlayerDir.Down; // �÷��̾� ���� ����

    private RaycastHit2D hit; // ����ĳ��Ʈ �ᱣ���� �����ϱ� ���� ����ü ����
    private Vector3 interPos; // ��ȣ�ۿ��� ����(���� �߻��� ����)
    private float rayLength; // ���� ����

    public Action OpenShop; // ������ ���� �׼�

    GameObject scanObject; // ���̿� �浹�� ������Ʈ ����

    public DialogManager dialogManager;

    public GameObject ScanObject { get; private set; } // scanObject�� �ܺο��� �б� ���� ������Ƽ

    protected override void Start()
    {
        base.Start();
        if (GameManager.instance.preMap == MapName.Title)
        {
            transform.position = new Vector3(12.63f, 3.3f, 0);
        }
        else if (GameManager.instance.preMap == MapName.SaveTitle)
        {
            transform.position = DataManager.instance.nowPlayerData.playerPos;
        }
        else if (GameManager.instance.preMap == MapName.BaseMap)
        {
            transform.position = new Vector3(0f, -2f, 0);
        }
        else if (GameManager.instance.preMap == MapName.SaveTitle)
        {
            transform.position = DataManager.instance.nowPlayerData.playerPos;
        }
        else if (GameManager.instance.preMap == MapName.SeaMap)
        {
            // SeaMap�� �ش��ϴ� �ʱ� ��ġ ���� ���ذ�
            //transform.position = new Vector3(0f, -3.167668f, 0);
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
            //Debug.DrawRay(transform.position, interPos * rayLength, Color.red);
            if (hit.collider != null) // �浹�� ������Ʈ�� ���� ���
            {
                // ���⿡�� ��ȣ�ۿ�
                // hit.collider�� ���̿� �浹�� ������Ʈ
                ScanObject = scanObject;
                scanObject = hit.collider.gameObject;

                if (scanObject.CompareTag("ItemDialog"))
                {
                    ScanObject = scanObject; // ScanObject ������ ����
                    dialogManager.Talk(scanObject);
                }
            }
            else
            {
                ScanObject = null;
                scanObject = null;
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
    }

    // ���콺 ��ȣ�ۿ� �߰��ϱ�



}