using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationManager : MonoBehaviour
{
    Dictionary<int, string[]> dialogData; // ��ȭ ���� ����
    Dictionary<int, Sprite> portraitData; // �ʻ�ȭ ��� ����

    public Sprite[] portraitArr; // �ʻ�ȭ Sprite �������� ���� �迭

    void Start()
    {
        dialogData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();

        GenerateData();
    }

    void GenerateData() // ��� & �ʻ�ȭ ����
    {
        // dialogData.Add(~~~(id), new string[] { "~~~:portraitIndex", "~~~:portraitIndex" }); // �Ŀ� ����

        // portraitData.Add(~~~(id) + n, portraitArr[n]); // �Ŀ� ����
    }

    public string GetDialog(int id, int dialogIndex) // ��� ���� (id = ��ȭ��� ��üid , dialogIndex = ��� �ε���)
    {
        if (dialogIndex == dialogData[id].Length) // ��ȭ�� ������
            return null; // ����
        else // ��簡 ����������
            return dialogData[id][dialogIndex]; // ��� ��ȯ
    }

    public Sprite GetPortrait(int id, int portraitIndex)
    {
        return portraitData[id + portraitIndex]; // �ʻ�ȭ �̹��� ��ȯ
    }
}
