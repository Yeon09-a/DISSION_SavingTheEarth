using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject[] itemListSlots; // �ϴ� ������ â ���� �迭
    public GameObject[] itemSlots; // ����ǰ ���� �迭
    public GameObject[] pItemSlots; // �߿� ��ǰ ���� �迭

    private bool[] checkItemList; // ������ â ������ ����ִ��� üũ
    private bool[] checkItem; // ����ǰ ������ ����ִ��� üũ
    private bool[] checkPItem; // �߿� ��ǰ ������ ����ִ��� üũ

    public GameObject itemIcon; // ������ ������ ������

    public HaveItem haveItems; // ������ �ִ� ������ ����
    public ItemDic items; // ������ ���� ����

    private void Awake()
    {
        // ���߿� ���� ������ �ʿ�
        checkItemList = new bool[5];
        checkItem = new bool[15];
        checkPItem = new bool[15];
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private int CheckHaveItem(int id) // �������� ���� ������ �ִ��� Ȯ��
    {
        if (haveItems.haveItems.ContainsKey(id)) // �������� ������ �ִ� ���
        {
            return haveItems.haveItems[id].type;
        }
        else // ������ ���� ���� ���
        {
            return -1;
        }
    }

    private int CheckItemList() // ������ â�� �� ���� ã��
    {
        for (int i = 0; i < checkItemList.Length; i++)
        {
            if(checkItemList[i] == false) // �� ���� �߰� ��
            {
                return i;
            }
        }

        return -1;
    }

    private int CheckItemSlots() // ����ǰ â�� �� ���� ã��
    {
        for (int i = 0; i < checkItem.Length; i++) 
        {
            if (checkItem[i] == false) // �� ���� �߰� ��
            {
                return i;
            }
        }

        return -1;
    }

    private int CheckPItemSlots() // �߿买ǰ â�� �� ���� ã��
    {
        for (int i = 0; i < checkPItem.Length; i++)
        {
            if (checkPItem[i] == false) // �� ���� �߰� ��
            {
                return i;
            }
        }

        return -1;
    }

    private bool PutNewItem(int id) // ���� ���� ������ ȹ��
    {
        int lIndex = CheckItemList();

        if (lIndex == -1) // ������ â�� �� ������ ���� ���
        {
            if (items.items[id].type == 1) // ������ ������ ����ǰ
            {
                int iIndex = CheckItemSlots();
                if (iIndex != -1) // ����ǰ â�� �� ������ �ִ� ���
                {
                    CreateItemIcon(itemSlots, checkItem, id, iIndex, 1);
                    return true;
                }
                else // �� ������ ���� ���
                {
                    return false;
                }
            }
            else // ������ ������ �߿� ��ǰ
            {
                int pIndex = CheckPItemSlots();
                if (pIndex != -1) // �߿� ��ǰ â�� �� ������ �ִ� ���
                {
                    CreateItemIcon(pItemSlots, checkPItem, id, pIndex, 2);
                    return true;
                }
                else // �� ������ ���� ���
                {
                    return false;
                }
            }
        }
        else // ������ â�� �� ������ �ִ� ���
        {
            // ������ ����Ʈ�� ������ ����
            CreateItemIcon(itemListSlots, checkItemList, id, lIndex, 0);
            return true;
        }
    }

    public void CreateItemIcon(GameObject[] slots, bool[] checkSlots, int id, int index, int type) // ������ ������ ����
    {
        GameObject icon = Instantiate(itemIcon, slots[index].transform.position, Quaternion.identity);
        // icon �Ӽ� ����(itemicon���� �̵�)
        icon.GetComponent<ItemIcon>().itemInfo = items.items[id];
        icon.transform.SetParent(slots[index].transform.GetChild(0));
        checkSlots[index] = true ;
        haveItems.haveItems.Add(id, new HaveItemInfo(type, 1, index));
    }

    public void PutHaveItem(int id, GameObject[] slots) // ������ ������ �ִ� ������ �߰�
    {
        HaveItemInfo haveItemInfo = haveItems.haveItems[id];
        int slotNum = haveItemInfo.slotNum;
        haveItemInfo.count += 1;
        slots[slotNum].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = haveItemInfo.count.ToString();
    }

    private bool PutItem(int id) // ������ ȹ��
    {
        int type = CheckHaveItem(id);
        if(type == -1) // ���� ���� ���
        {
            return PutNewItem(id);
        } 
        else if(type == 0) // �ش� �������� ������ â�� �ִ� ���
        {
            PutHaveItem(id, itemListSlots);
            return true;
        } 
        else if(type == 1) // ����ǰ â�� �ִ� ���
        {
            PutHaveItem(id, itemSlots);
            return true;
        } 
        else if(type == 2) // �߿买ǰ â�� �ִ� ���
        {
            PutHaveItem(id, pItemSlots);
            return true;
        }
        else
        {
            return false;
        }
    }
}
