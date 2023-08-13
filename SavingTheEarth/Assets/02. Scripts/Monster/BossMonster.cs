using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : MonoBehaviour
{
    public GameObject beam;
    private GameObject[] beams;
    private int beamCount = 0;
    private Vector2[] boundarysX = { new Vector2(-9.0f, -4.5f), new Vector2(-4.5f, 0.0f), new Vector2(0.0f, 4.5f), new Vector2(4.5f, 9.0f)};
    private Vector2[] boundarysY = { new Vector2(5.0f, 0.0f), new Vector2(-5.0f, 0.0f)};

    public static bool isAttaking; // ���� ������ Ȯ��

    public delegate void beamDel();
    public static event beamDel startBeamAnimation;

    // ���� ���� ����
    private int maxBeam = 8; // �� ����(�ӽ�)
    private int attackNum; // ���� ��ȣ(1, 2, 3)
    private int attackCount = 0; // ���� Ƚ�� 
    private bool isEyeOpen = false; // ��
    //private Player player;

    // ���� ����
    public Monster bossMonster;
    //private float mHP; // character ��ӹ޾� ���.

    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindWithTag("Player").GetComponent<Player>();

        beams = new GameObject[maxBeam]; // �ӽ�

        //ShootBeam();
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
            attackNum = Random.Range(1, 3);
            if (attackNum == 1)
            {
                ShootBeam();
            }
            else if (attackNum == 2)
            {
                Paralysis();
            }

            attackCount++;

            if (attackCount < 5)
            {
                yield return new WaitForSeconds(6.0f);
            } else { // ���� 5�� ��Ÿ��
                yield return new WaitForSeconds(30.0f);
            }
        }
    }

    private void ShootBeam() // �� ����
    {
        Debug.Log("����1");
        for (int i = 0; i < maxBeam; i++)
        {
            beams[i] = Instantiate(beam, setBeamPos(i), Quaternion.identity); // ��ġ�� �Ŀ� �ʿ� ���缭 ����
        }

        startBeamAnimation();
    }

    private void Paralysis() // ���� ����
    {
        isEyeOpen = true;
        /*if() // ��������Ʈ�� ���� �ٶ󺸴��� �ƴ� ���� ���� ����
         * ������ ����(�Ŀ� player ��ũ��Ʈ�� bool �� ���� �־ ������ ����)
         * ����� 4������?
         */

        Debug.Log("����2");
    }

    private void MakeShield()
    {
        //  ���� ü�� < maxü�� / 2.0f �� ��� ���� ��ȯ
        // �ǵ� �ۿ�. ������ ���� �� �ִ����� ���� bool üũ ���. bool ����
    }

    // �� ��ġ ���� �Լ�
    private Vector2 setBeamPos(int i)
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

}
