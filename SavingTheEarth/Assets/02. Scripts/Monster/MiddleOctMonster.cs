using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MiddleOctMonster : MonoBehaviour
{
    public Monster octMon;
    
    public GameObject[] beams;
    public GameObject[] bubbles;

    /*private Vector2[] boundarysX = { new Vector2(-9.0f, -4.5f), new Vector2(-4.5f, 0.0f), new Vector2(0.0f, 4.5f), new Vector2(4.5f, 9.0f) };
    private Vector2[] boundarysY = { new Vector2(5.0f, 0.0f), new Vector2(-5.0f, 0.0f) };*/


    // ��� �߻� ��ġ ���ϱ� 
    private int[] bubbleRot = {-50, -35, -18, 0, 18, 35, 50}; // inspector�� ����

    public static bool isAttaking; // ���� ������ Ȯ��

    // �� ����
    private int maxBeam = 8; // �ӽ�
    private int maxBubble = 7; // �ӽ�

    public Action startBeamAnimation;
    public Action startBubbleMove;

    // ���� ��ȣ
    private int attackNum; // 1, 2, 3

    // Start is called before the first frame update
    void Start()
    {

        
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
            beams[i].SetActive(true); // ��ġ�� �Ŀ� �ʿ� ���缭 ����
        }

        startBeamAnimation();
    }

    private void ShootBubble()
    {
        Debug.Log("�Թ� ���");
        for (int i = 0; i < maxBubble; i++)
        {
            bubbles[i].SetActive(true);
        }
        startBubbleMove();
    }

    public void StartAttack()
    {
        StartCoroutine(Attack());
    }


}
