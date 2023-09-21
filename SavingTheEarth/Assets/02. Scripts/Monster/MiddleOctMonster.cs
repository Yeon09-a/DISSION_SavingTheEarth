using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleOctMonster : MonoBehaviour
{
    public GameObject beam;
    private GameObject[] beams;

    private Vector2[] boundarysX = { new Vector2(-9.0f, -4.5f), new Vector2(-4.5f, 0.0f), new Vector2(0.0f, 4.5f), new Vector2(4.5f, 9.0f) };
    private Vector2[] boundarysY = { new Vector2(5.0f, 0.0f), new Vector2(-5.0f, 0.0f) };

    public GameObject bubble;
    private GameObject[] bubbles;

    // ��� �߻� ��ġ ���ϱ� 
    private int[] bubbleRot = {-50, -35, -18, 0, 18, 35, 50};

    public static bool isAttaking; // ���� ������ Ȯ��

    // �� ����
    private int maxBeam = 8; // �ӽ�

    private int maxBubble = 7; // �ӽ�

    public delegate void attackDel();
    public static event attackDel startBeamAnimation;
    public static event attackDel startBubbleMove;

    // ���� ��ȣ
    private int attackNum; // 1, 2, 3

    // Start is called before the first frame update
    void Start()
    {
        beams = new GameObject[maxBeam]; // �ӽ�
        bubbles = new GameObject[maxBubble]; // �ӽ�

        //ShootBeam();
        //ShootBubble();
        StartCoroutine(Attack());
    }

    IEnumerator Attack() // ���� �Լ�
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
                ShootBubble();
            }

            yield return new WaitForSeconds(6.0f);
        }
    }

    private void ShootBeam() // �� �߻� �Լ�
    {
        Debug.Log("�Թ� ���");
        for (int i = 0; i < maxBeam; i++)
        {
            beams[i] = Instantiate(beam, setBeamPos(i), Quaternion.identity); // ��ġ�� �Ŀ� �ʿ� ���缭 ����
        }

        startBeamAnimation();
    }


    private Vector2 setBeamPos(int i) // �� ��ġ ����
    {
        Vector2 pos = new Vector2(0f, 0f);

        if (i % 4 == 0)
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

        if (i < 4)
        {
            pos.y = Random.Range(boundarysY[0].x, boundarysY[0].y);
        }
        else
        {
            pos.y = Random.Range(boundarysY[1].x, boundarysY[1].y);
        }


        return pos;
    }

    private void ShootBubble()
    {
        Debug.Log("�Թ� ���");
        for (int i = 0; i < maxBubble; i++)
        {
            bubbles[i] = Instantiate(bubble, transform.position, Quaternion.Euler(0, 0, 180));
            bubbles[i].GetComponent<PoisonBubble>().moveDir = (Quaternion.Euler(0, 0, bubbleRot[i]) * transform.position).normalized;
        }
        startBubbleMove();
    }


}
