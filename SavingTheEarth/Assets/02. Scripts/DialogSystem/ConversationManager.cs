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
        //// �ʱ�ȭ
        //dialogData = new Dictionary<int, string[]>();
        //portraitData = new Dictionary<int, Sprite>();

        //// ������ ���� �Լ� ȣ��
        //GenerateData(); 
    }

    private void Awake()
    {
        // �ʱ�ȭ
        dialogData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();

        // ������ ���� �Լ� ȣ��
        GenerateData(); 
    }

    // ��� & �ʻ�ȭ ���� �Լ�
    void GenerateData() 
    {
        dialogData.Add(100, new string[] { "�̵�: WASD �Ǵ� ����Ű\n�޸���: shift\n������ ��ȣ�ۿ�: e �Ǵ� ���콺\n�κ��丮 ����: ���� 1 ~ 5"});
        dialogData.Add(200, new string[] { "1. �½Ƿ� ���� �� �ֱ�\n2. �����԰��� ��� Ȯ���ϱ�"}); 

        // portraitData.Add(~~~(id) + n, portraitArr[n]); // �Ŀ� ����
    }

    // ��ȭ ��ȯ �Լ�
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
