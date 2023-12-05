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
        // ���&&����Ʈ ������

        // ������Ʈid => ������:1000 ĥ��: 2000 �½� ��: 3000 ������ å��: 4000 ���� ����: 5000
        // �����Թ� å�� �� ����: 6000 �ⱸ: 7000 ���� ħ��: 8000 ������ ����: 9000
        // �÷��̾� å��: 10000 �����Թ� å��: 11000 �����Թ� ħ��: 12000
        // /�ڿ� ���� : �ʻ�ȭ ���� ��ȣ

        // �⺻ ���
        dialogData.Add(1000, new string[] {
            "�̵�: WASD �Ǵ� ����Ű\n�޸���: shift\n������ ��ȣ�ۿ�: e �Ǵ� ���콺\n�κ��丮 ����: ���� 1 ~ 5/6"
        });
        dialogData.Add(2000, new string[] {
            "�ƹ��� �����ص� ���� �ϳ��δ� ������\n���� ���� �ϳ� ���޶�� ���غ��߰ڱ�/2"
        });
        dialogData.Add(3000, new string[] {
            "��ĥ û�� ���ߴٰ� ������ ��� �����̳�..\n������ �Ȱ�ż� �����̴�/4"
        });
        dialogData.Add(4000, new string[] {
            "å�� ���� ���� ����../1"
        });
        dialogData.Add(5000, new string[] {
            "�ֱٿ�.. å�� �о�����?/4"
        });
        dialogData.Add(6000, new string[] {
            "�� �������� �̷��� ����?/3"
        });
        dialogData.Add(8000, new string[]
        {
            "�ٽ� ���� ������... ����/1"
        });
        dialogData.Add(9000, new string[]
        {
            "�������� å�� �����Դٿ� �͸� �����ó�../1"
        });
        dialogData.Add(10000, new string[]
        {
            "���� ��... ���� �� ����/5"
        });
        dialogData.Add(11000, new string[]
        {
            "������ å�� �������� �ʱ���..��/3"
        });
        dialogData.Add(12000, new string[]
        {
            "������ ħ��... �� ħ�뺸�� ���ƺ���.../4"
        });

        // ���丮 ���� ���
        dialogData.Add(10 + 1000, new string[] {
            "�̵�: WASD �Ǵ� ����Ű\n�޸���: shift\n������ ��ȣ�ۿ�: e �Ǵ� ���콺\n�κ��丮 ����: ���� 1 ~ 5/6",
            "���� ���� �ؾ��� ���� Ȯ������/0"
        });
        dialogData.Add(20 + 2000, new string[] {
            "< 1. �½Ƿ� ���� �� �ֱ� >\n< 2. �����԰��� ��� Ȯ���ϱ� >/6",
            "�ϴ� �½ǿ� ���� ���� ���� �ٱ�?/0"
        });
        dialogData.Add(21 + 3000, new string[] {
            "�� �� �ڶ�� �ֱ�! ��Ư�ض�/0",
            "���� �����Ƿ� ���� ����� Ȯ���غ���/0"
        });
        dialogData.Add(30 + 1000, new string[] {
            "���õ� �ƹ��� ������ �����ó�..\n\n�������� ��ü ���� ���ô°���?/4",
            "���� ��¥�� ���� ���̶� ����� �� �ƴϾ�?/3", "��.. ������ �� �� ������.../1",
            "��¥ ���� ã���� ������ �ϳ�...;;/5",
            "���� �����Ϸ��� ��¿ �� ����\n\n�ϴ� ������ ����� ��𼭺��� ������� Ȯ���غ��߰ھ�/2",
            "\"�߻߻߻� �߻߻߻�\"\n\n\"�� ----\"\n\n\"��� ���� �Ϸ�\"/6",
            "�����... ��õ �縷�ΰ�?\n\n���� ���� ���� �賭�ϰڱ�/4",
            "� ���� ä�� ����/2",
            "�ʿ��� ��ǰ�� üũ�غ���!/0",
            "1. �ķ� -> �½�\n2. ���� -> ������ å��\n3. ħ�� -> �� ��\n4. �̴� ����� Ű -> ������ �� å��/6",
            "�� �½Ǻ��� �鷯�߰ڴ�/0"
        });
        dialogData.Add(40 + 3000, new string[] {
            "�ķ� ������!/0",
            "������ �����Ƿ� ����/0"
        });
        dialogData.Add(41 + 4000, new string[] {
            "���⵵ ì���!/0",
            "�� �濡 ħ���� �׾��µ� ��� ������?/2"
        });
        dialogData.Add(42 + 5000, new string[] {
            "���� �־�����\n\nħ���� ������!/0",
            "���� �̴� ����� Ű�� ì��� �ǰڴ�/0",
            "�ٵ� ������ �濡 ���� �ٵ�.. ��� ������ �ǰ���?/2"
        });
        dialogData.Add(43 + 6000, new string[] {
            "������ å���� �����Ÿ��ϱ� ����� �̻��ϳ�\n\n���� �뼭�ϼ��� ������../1",
            "����°���... �� �̰Ŵ�!/0",
            "�� �׷��� �� ������.. ����?\n\n�� Ű�� �� �� �Ʒ���?/3",
            "�� ������ ������ �� �Ȱ� ���̾���������/2",
            "�ٵ� ������ ���� ��� ���� ������?\n\n���� ���ƺ��̴µ�~/0",
            "�� �غ�� �� �� �� ������.../2",
            "���� ������?/0"
        });
        dialogData.Add(50 + 7000, new string[] {
            "[ ���� �����ðڽ��ϱ�? ]\n\n[ 1. ������ 2. �� �ѷ����� ]/6"
        });

        // �ʻ�ȭ ������
        // ���ΰ�
        portraitData.Add(0, portraitArr[0]); // normal face ����Ʈ ǥ��
        portraitData.Add(1, portraitArr[1]); // closed eye �� ���� ǥ��
        portraitData.Add(2, portraitArr[2]); // stare face ���� �Ʒ��� ǥ��
        portraitData.Add(3, portraitArr[3]); // side eye °������ ǥ��
        portraitData.Add(4, portraitArr[4]); // little annoying �ణ ¥��
        portraitData.Add(5, portraitArr[5]); // very annoying �ſ� ¥��
        portraitData.Add(6, portraitArr[6]); // �������� ���� �ʻ�ȭ
    }

    // ��ȭ ��ȯ �Լ�
    public string GetDialog(int id, int dialogIndex) // ��� ���� (id = ��ȭ��� ��üid , dialogIndex = ��� �ε���)
    {
        if (!dialogData.ContainsKey(id)) // ����Ʈ ������ ��簡 ���� ��� ����ó��
        {
            if (!dialogData.ContainsKey(id - id % 10))
                return GetDialog(id - id % 100, dialogIndex);
            else
                return GetDialog(id - id % 10, dialogIndex);
        }

        if (dialogIndex == dialogData[id].Length) // ��ȭ�� ������
            return null; // ����

        else // ��簡 ����������
            return dialogData[id][dialogIndex]; // ��� ��ȯ
    }

    public Sprite GetPortrait(int portraitIndex) // �ʻ�ȭ �̹��� ���� (portraitIndex == �ʻ�ȭ ��ȣ)
    {
        return portraitData[portraitIndex]; // �ʻ�ȭ �̹��� ��ȯ
    }
}
