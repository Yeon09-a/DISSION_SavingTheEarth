using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData : MonoBehaviour // �÷��̾� ������ Ŭ����
{
    [NonSerialized]
    public static PlayerData instance = null; // �̱��� ����

    // �÷��̾� ���� ����
    public string playerName; // �÷��̾� �̸�

    // ������ ���� ����
    [NonSerialized]
    public Dictionary<int, HaveItemInfo> haveItems; // ���� ������ �ִ� ������ ��ųʸ�<������ id, ������ ����>

    private void Awake()
    {
        // �̱���
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public PlayerData(Dictionary<int, HaveItemInfo> haveItems, string playerName) // �Ŀ� �÷��̾� ���� �߰��� �Ű����� �߰��� ����
    {
        this.haveItems = haveItems;
        this.playerName = playerName;
    }
}
