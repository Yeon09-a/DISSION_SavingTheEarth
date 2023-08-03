using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TimeManager : MonoBehaviour
{
    public TextMeshProUGUI timeText; // �ð� text

    private int curTime; // ���� �ð�(�����Ϳ��� ��������) // �ӽ�(���ӽð� ���� 9��)
    private int uiMinutes; // UI�� ǥ�õ� ��
    private int uiHours; // UI�� ǥ�õ� �ð�
    private string ampm; // ���� ���� ǥ��

    private int standTime = 300; // ���� �ð�(�ʴ��� / ���ӿ��� 30���� ���� 5��)
    private int gameStandTime = 30; // ���� �ð�(�д���)

   

    private void Awake()
    {
        curTime = 540;// ��� ������ �������� �����ϸ鼭 
        uiMinutes = 0;
        uiHours = 9;
        ampm = " a.m.";
    }
    void Start()
    {
        StartCoroutine(setTime());
    }

    void Update()
    {
        
    }

    private void calTime()
    {
        uiHours = curTime / 60;
        uiMinutes = curTime % 60;
        if(uiHours == 13 && uiMinutes == 0)
        {
            uiHours = 1;
            curTime = 60;
            if (ampm.Equals(" a.m."))
            {
                ampm = " p.m.";
            } else if (ampm.Equals(" p.m."))
            {
                ampm = " a.m.";
            }
        }
    }
    IEnumerator setTime()
    {
        while (true)
        {
            calTime();
            timeText.text = uiHours.ToString() + " : " + uiMinutes.ToString("D2") + ampm;
            yield return new WaitForSeconds(standTime);
            curTime += gameStandTime;
        }
    }
}
