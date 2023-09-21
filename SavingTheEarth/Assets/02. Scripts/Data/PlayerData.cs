using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData instance = null; // �̱��� ����

    // �÷��̾� ���� ����
    public string playerName; // �÷��̾� �̸�
    
    // ������ ���� ����
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

    
}
