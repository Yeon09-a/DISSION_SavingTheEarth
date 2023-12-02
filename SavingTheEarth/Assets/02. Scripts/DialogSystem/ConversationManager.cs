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
        dialogData.Add(100, new string[] {  // ������
            "�̵�: WASD �Ǵ� ����Ű\n�޸���: shift\n������ ��ȣ�ۿ�: e �Ǵ� ���콺\n�κ��丮 ����: ���� 1 ~ 5/6",
            "���õ� �ƹ��� ������ �����ó�..\n�������� ��ü ���� ���ô°���?/4", 
            "���� ��¥�� ���� ���̶� ����� �� �ƴϾ�?/3", "��.. ������ �� �� ������.../1",
            "��¥ ���� ã���� ������ �ϳ�...(��¨)/5",
            "���� �����Ϸ��� ��¿ �� ����\n�ϴ� ������ ����� ��𼭺��� ������� Ȯ���غ��߰ھ�/2"
        });
        dialogData.Add(200, new string[] { // ĥ��
            "1. �½Ƿ� ���� �� �ֱ�\n2. �����԰��� ��� Ȯ���ϱ�/6",
            "�ϴ� �½ǿ� ���� ���� ���� �ٱ�?/0"
        });
        dialogData.Add(300, new string[] { // �½� ��
            "���� �����Ƿ� ���� ����� Ȯ���غ���/0"
        });

        //portraitData.Add(100 + 0, portraitArr[0]); // normal face
        //portraitData.Add(100 + 1, portraitArr[1]); // closed eye
        //portraitData.Add(100 + 2, portraitArr[2]); // stare face
        //portraitData.Add(100 + 3, portraitArr[3]); // side eye
        //portraitData.Add(100 + 4, portraitArr[4]); // little annoying
        //portraitData.Add(100 + 5, portraitArr[5]); // very annoying

        //portraitData.Add(200 + 0, portraitArr[0]); // normal face
        //portraitData.Add(200 + 1, portraitArr[1]); // closed eye
        //portraitData.Add(200 + 2, portraitArr[2]); // stare face
        //portraitData.Add(200 + 3, portraitArr[3]); // side eye
        //portraitData.Add(200 + 4, portraitArr[4]); // little annoying
        //portraitData.Add(200 + 5, portraitArr[5]); // very annoying

        portraitData.Add(0, portraitArr[0]); // normal face
        portraitData.Add(1, portraitArr[1]); // closed eye
        portraitData.Add(2, portraitArr[2]); // stare face
        portraitData.Add(3, portraitArr[3]); // side eye
        portraitData.Add(4, portraitArr[4]); // little annoying
        portraitData.Add(5, portraitArr[5]); // very annoying
        portraitData.Add(6, portraitArr[6]); // �������� ���� �ʻ�ȭ
    }

    // ��ȭ ��ȯ �Լ�
    public string GetDialog(int id, int dialogIndex) // ��� ���� (id = ��ȭ��� ��üid , dialogIndex = ��� �ε���)
    {
        if (dialogIndex == dialogData[id].Length) // ��ȭ�� ������
            return null; // ����
        else // ��簡 ����������
            return dialogData[id][dialogIndex]; // ��� ��ȯ
    }

    //public Sprite GetPortrait(int id, int portraitIndex)
    //{
    //    return portraitData[id + portraitIndex]; // �ʻ�ȭ �̹��� ��ȯ
    //}
    public Sprite GetPortrait(int portraitIndex) // �ʻ�ȭ �̹��� ���� (portraitIndex == �ʻ�ȭ ��ȣ)
    {
        return portraitData[portraitIndex]; // �ʻ�ȭ �̹��� ��ȯ
    }
}
