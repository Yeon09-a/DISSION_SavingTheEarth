using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HaveItem", menuName = "ScriptableObject/HaveItem")]
public class HaveItem : ScriptableObject
{
    [SerializeField]
    public Dictionary<int, HaveItemInfo> haveItems; // ���� ������ �ִ� ������ ��ųʸ�<������ id, ������ ����>
}

[System.Serializable]
public class HaveItemInfo : ScriptableObject
{
    public int type; // ������ ��ġ
    public int count; // ������ ����
    public int slotNum; // �������� ���ִ� ���� �ε���

    public HaveItemInfo(int type, int count, int slotNum) // ������
    {
        this.type = type; // �з�(0(������ â), 1(����ǰ), 2(�߿买ǰ))
        this.count = count;
        this.slotNum = slotNum;
    }
}
