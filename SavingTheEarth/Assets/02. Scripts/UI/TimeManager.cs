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
    }
    void Start()
    {
        curTime = DataManager.instance.nowPlayerData.gameTime;
        ampm = DataManager.instance.nowPlayerData.ampm;

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
            if (ampm.Equals("����  "))
            {
                ampm = "����  ";
            } else if (ampm.Equals("����  "))
            {
                ampm = "����  ";
            }
        }
    }
    IEnumerator setTime()
    {
        while (true)
        {
            calTime();
            timeText.text = ampm + uiHours.ToString() + " : " + uiMinutes.ToString("D2");
            yield return new WaitForSeconds(standTime);
            curTime += gameStandTime;
        }
    }
}
