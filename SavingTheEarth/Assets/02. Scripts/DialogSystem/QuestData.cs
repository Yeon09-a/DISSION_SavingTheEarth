using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestData // ����Ʈ ���� ���� ����ü
{
    public string questName;
    public int[] objId;

    public QuestData(string name, int[] obj) // ������
    {
        questName = name;
        objId = obj;
    }
}
