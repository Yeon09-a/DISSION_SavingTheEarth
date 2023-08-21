using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : Character
{
    [Header("BossInfo")]
    public GameObject beam; // �� ������
    public GameObject[] etcMons; // ��Ÿ ����
    public GameObject spawnCircle; // ���� ��

    // �ӽ�
    private Vector2[] boundarysX = { new Vector2(-9.0f, -4.5f), new Vector2(-4.5f, 0.0f), new Vector2(0.0f, 4.5f), new Vector2(4.5f, 9.0f)};
    private Vector2[] boundarysY = { new Vector2(5.0f, 0.0f), new Vector2(-5.0f, 0.0f)};

    public static bool isAttaking; // ���� ������ Ȯ��

    public delegate void bossDel(); // ���� ��������Ʈ
    public static event bossDel startBeamAnimation; // �� �ִϸ��̼� �̺�Ʈ
    private event bossDel spawnEtcMons; // ��Ÿ ���� ���� �̺�Ʈ

    // ���� ���� ����
    private int maxBeam = 8; // �� ����(�ӽ�)
    private int attackNum; // ���� ��ȣ(1, 2, 3)
    private int prevAttactNum; // ���� ���� ��ȣ
    private int attackCount = 0; // ���� Ƚ�� 
    private float paralysisTime = 10.0f; // ���� �ð�
    private bool isHpHalf = false; // ���� hp�� �� �������� üũ
    private bool isShield = false; // �ǵ� �������� üũ
    private int maxEtcMons = 8; // ��Ÿ ���� �ִ� ��(�ӽ�)

    private Player player;

    // ���� ����
    public Monster bossMonster;

    // Start is called before the first frame update
    void Start()
    {
        hp = bossMonster.hp;
        player = GameObject.FindWithTag("Player").GetComponent<Player>();

        StartCoroutine(Attack());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Attack()
    {
        while (true)
        {
            attackNum = isHpHalf ? Random.Range(1, 4) : Random.Range(1, 3); // ü���� �� ������ ��� 1~3����, �׷��� ���� ��� 1~2 ����
            if (attackNum == 1 || prevAttactNum == 2) // �� ����
            {
                prevAttactNum = 1;
                ShootBeam();
            }
            else if (attackNum == 2) // ���� ����
            {
                prevAttactNum = 2;
                StartCoroutine(Paralysis());
            }
            else if (attackNum == 3) // �ǵ� ����
            {
                prevAttactNum = 3;
                MakeShield();
            }

            attackCount++;

            if (attackCount < 5 && attackNum != 3)
            {
                yield return new WaitForSeconds(6.0f);
            }
            else if (attackCount == 5)
            { // ���� 5�� ��Ÿ��
                attackCount = 0;
                yield return new WaitForSeconds(15.0f);
            }
            else // ���� 3�϶� 
            {
                yield return new WaitWhile(() => isShield);
                spawnEtcMons = null;
            }
        }
    }

    private void ShootBeam() // �� ����
    {
        Debug.Log("����1");
        for (int i = 0; i < maxBeam; i++)
        {
            Instantiate(beam, setPos(i), Quaternion.identity); // ��ġ�� �Ŀ� �ʿ� ���缭 ����
        }

        startBeamAnimation();
    }

    IEnumerator Paralysis() // ���� ����
    {
        Debug.Log("����2");
        if (player.playerDir == PlayerDir.Up)
        {
            Debug.Log("���� ����");
            player.SetSpeed(0.0f);
            player.SetAnimator(false);

            yield return new WaitForSeconds(paralysisTime);

            Debug.Log("���� ��");
            player.SetSpeed(5.0f);
            player.SetAnimator(true);
        }
    }

    private void MakeShield() // �ǵ� ����
    {
        isShield = true;
        for (int i = 0; i < maxEtcMons; i++)
        {
            spawnEtcMons += () => StartCoroutine(SpawnEtcMons(i, etcMons[Random.Range(1, 3)]));
        }

        spawnEtcMons();
    }

    // �� ��ġ ���� �Լ�
    private Vector2 setPos(int i)
    {
        Vector2 pos = new Vector2(0f, 0f);

        if(i % 4 == 0)
        {
            pos.x = Random.Range(boundarysX[0].x, boundarysX[0].y);
        }
        else if (i % 4 == 1)
        {
            pos.x = Random.Range(boundarysX[1].x, boundarysX[1].y);
        } 
        else if (i % 4 == 2)
        {
            pos.x = Random.Range(boundarysX[2].x, boundarysX[2].y); 
        }
        else if (i % 4 == 3)
        {
            pos.x = Random.Range(boundarysX[3].x, boundarysX[3].y);
        }

        if(i < 4)
        {
            pos.y = Random.Range(boundarysY[0].x, boundarysY[0].y);
        } 
        else {
            pos.y = Random.Range(boundarysY[1].x, boundarysY[1].y);
        }


        return pos;
    }

    // ü�� ����
    public void DecreaseHp(float attack)
    {
        if (!isShield)
        {
            hp -= attack;
            if (hp < bossMonster.hp / 2.0f)
            {
                isHpHalf = true;
            }
        }
    }

    // etcMons ����
    IEnumerator SpawnEtcMons(int i, GameObject etcMon)
    {
        GameObject circle = Instantiate(spawnCircle, setPos(i), Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        Instantiate(etcMon, setPos(i), Quaternion.identity).tag = "EtcMon";
        yield return new WaitForSeconds(0.5f);
        Destroy(circle);
    }

}
