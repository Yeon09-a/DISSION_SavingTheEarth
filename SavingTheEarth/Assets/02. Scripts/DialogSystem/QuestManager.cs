using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionIndex;
    public GameObject[] questObject;

    public InventoryManager inventoryManager;

    Dictionary<int, QuestData> questList; // ����Ʈ ��� ����

    // Start is called before the first frame update
    void Start()
    {
        questList = new Dictionary<int, QuestData>(); // �ʱ�ȭ
        inventoryManager = GetComponent<InventoryManager>(); // �ʱ�ȭ

        GenerateData();
    }

    void GenerateData() // ����Ʈ ������ ����
    {
        questList.Add(10, new QuestData("�����Ƿ� ���� ����Ű ����", new int[] { 1000 }));
        questList.Add(20, new QuestData("ĥ�ǿ��� �� �� üũ �� �½ǿ��� �� �ֱ�", new int[] { 2000, 3000 }));
        questList.Add(30, new QuestData("������ ��濡 �ǹ� ����&���� ����ϱ�", new int[] { 1000 }));
        questList.Add(40, new QuestData("�ʿ��� ��ǰ ì���", new int[] { 3000, 13000, 4000, 5000, 6000 }));
        questList.Add(50, new QuestData("���� ���", new int[] { 7000 }));
        questList.Add(60, new QuestData("BaseMap ����Ʈ Ŭ����", new int[] { 0 }));
    }

    public int GetQuestTalkIndex()
    {
        return questId + questActionIndex;
    }

    public string CheckQuest(int id)
    {
        if (id == questList[questId].objId[questActionIndex])
            questActionIndex++; // ����Ʈ �׼� �ε��� ����

        ControlObject(); // ����Ʈ ������Ʈ �Լ�

        if (questActionIndex == questList[questId].objId.Length) // ��� ����Ʈ �ϼ� ��
            NextQuest(); // ���� ����Ʈ�� �Ѿ��

        return questList[questId].questName;
    }
    public string CheckQuest()
    {
        return questList[questId].questName;
    }

    void NextQuest()
    {
        questId += 10;
        questActionIndex = 0;
    }

    void ControlObject()
    {
        switch (questId)
        {
            case 10:
                if (questActionIndex == 1)
                    questObject[0].SetActive(true); // ĥ�� ���� ����ǥ ����
                break;
            case 20:
                if (questActionIndex == 1)
                    questObject[0].SetActive(false); // ĥ�� ���� ����ǥ ���ֱ�
                break;
            case 30:
                if (questActionIndex == 1)
                    questObject[1].SetActive(true);
                break;
            case 40:
                if (questActionIndex == 1)
                {
                    questObject[1].SetActive(false);
                    questObject[2].SetActive(true);
                }
                if (questActionIndex == 2)
                {
                    questObject[2].SetActive(false);
                    questObject[3].SetActive(true);
                }
                if (questActionIndex == 3)
                {
                    questObject[3].SetActive(false);
                    questObject[4].SetActive(true);
                    inventoryManager.PutItem(1, 1);// putItem() ȣ��
                }
                if (questActionIndex == 4)
                {
                    questObject[4].SetActive(false);
                    questObject[5].SetActive(true);
                    inventoryManager.PutItem(2, 1);// putItem() ȣ��
                }
                if (questActionIndex == 5)
                {
                    questObject[5].SetActive(false);
                }
                break;
        }
    }
}

