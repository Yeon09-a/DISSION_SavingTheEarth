using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HaveItemInfo
{
    public int place; // ������ ��ġ
    public int count; // ������ ����
    public int slotNum; // �������� ���ִ� ���� �ε���

    public HaveItemInfo(int place, int count, int slotNum) // ������
    {
        this.place = place; // �з�(0(������ â), 1(����ǰ), 2(�߿买ǰ))
        this.count = count;
        this.slotNum = slotNum;
    }
}

public class InventoryManager : MonoBehaviour
{
    public GameObject[] itemListSlots; // �ϴ� ������ â ���� �迭
    public GameObject[] itemSlots; // ����ǰ ���� �迭
    public GameObject[] pItemSlots; // �߿� ��ǰ ���� �迭

    private bool[] checkItemList; // ������ â ������ ����ִ��� üũ
    private bool[] checkItem; // ����ǰ ������ ����ִ��� üũ
    private bool[] checkPItem; // �߿� ��ǰ ������ ����ִ��� üũ

    public GameObject itemIcon; // ������ ������ ������

    public ItemDic items; // ������ ���� ����

    public SlotClick[] slots; // ���� �迭
    private SlotClick selectedSlot = null; // ���õ� ����

    void Update()
    {
        // Ű���� �Է��� ���� ���� ����
        for (int i = 0; i < slots.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                // ����� �κ�: ���� ��ȣ�� SlotClick ��ü�� ��ȯ
                SlotClick clickedSlot = slots[i];
                HandleSlotSelection(clickedSlot);

                // �������� �ִ��� Ȯ���ϰ� ó��
                CheckAndHandleItemInSlot(clickedSlot);
            }
        }
    }

    private void CheckAndHandleItemInSlot(SlotClick clickedSlot)
    {
        if (clickedSlot.HasItem())
        {
            Item itemInSlot = clickedSlot.GetItem();
            Debug.Log("Selected slot contains item: " + itemInSlot.itName);

            // ���⿡�� ���õ� ���Կ� �������� ����ִ� ����� ������ �߰��ϼ���.
        }
        else
        {
            Debug.Log("Selected slot is empty.");
        }
    }

    public void HandleSlotSelection(SlotClick clickedSlot)
    {
        bool isSelected = clickedSlot.IsSelected();

        if (isSelected)
        {
            // �̹� ���õ� ������ �ٽ� ������ ���
            clickedSlot.Deselect();
            selectedSlot = null; // ���õ� ���� ������Ʈ
        }
        else
        {
            // ���ο� ������ ������ ���
            if (selectedSlot != null)
            {
                // ������ ������ ������ ������ �ش� ������ ���� ����
                selectedSlot.Deselect();
            }

            clickedSlot.ToggleSelection();
            selectedSlot = clickedSlot; // ���õ� ���� ������Ʈ

            // EventSystem�� ����Ͽ� ���� ���õ� ���� ������Ʈ ����
            EventSystem.current.SetSelectedGameObject(clickedSlot.gameObject);
        }
    }

    public void SelectSlot(SlotClick clickedSlot)
    {
        if (selectedSlot != null)
        {
            selectedSlot.ToggleSelection(); // ������ ������ ���� ���� ����
        }

        selectedSlot = clickedSlot; // ���ο� ���� ����
        selectedSlot.ToggleSelection();
    }

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
        //UpdateInventory();
    }

    public void Use(Item item)
    {
        // ������ �Ӽ��� ������� ������ ��� ȿ���� ���� ������ ����
        Debug.Log("������ ��� ��: " + item.itName);

        // �ʿ��� ��� ������ �Ӽ��� ������� �߰� ������ ����
    }



    public void UpdateInventory()
    {
        // slots �迭 �ʱ�ȭ
        slots = GetComponentsInChildren<SlotClick>();

        // ���õ� ���� �ʱ�ȭ
        selectedSlot = null;

        // ������ ������ �ҷ�����
        foreach (KeyValuePair<int, HaveItemInfo> item in DataManager.instance.nowPlayerData.haveItems)
        {
            if (item.Value.place == 0) // ������ â
            {
                LoadItemIcon(itemListSlots, checkItemList, item.Key, item.Value.slotNum, item.Value.count);
            }
            else if (item.Value.place == 1) // ����ǰ
            {
                LoadItemIcon(itemSlots, checkItem, item.Key, item.Value.slotNum, item.Value.count);
            }
            else // �߿买ǰ
            {
                LoadItemIcon(pItemSlots, checkPItem, item.Key, item.Value.slotNum, item.Value.count);
            }
        }
    }

    private void LoadItemIcon(GameObject[] slots, bool[] checkSlots, int id, int index, int count) // ������ ������ �ҷ�����
    {
        GameObject icon = Instantiate(itemIcon, slots[index].transform.position, Quaternion.identity);
        // icon �Ӽ� ����(itemicon���� �̵�)
        icon.GetComponent<ItemIcon>().itemInfo = items.items[id];
        icon.transform.SetParent(slots[index].transform.GetChild(0));
        checkSlots[index] = true;

        icon.GetComponentInChildren<TextMeshProUGUI>().text = count.ToString();
    }

    private int CheckHaveItem(int id) // �������� ���� ������ �ִ��� Ȯ��
    {
        if (DataManager.instance.nowPlayerData.haveItems.ContainsKey(id)) // �������� ������ �ִ� ���
        {
            return DataManager.instance.nowPlayerData.haveItems[id].place;
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
    
    public void CreateItemIcon(GameObject[] slots, bool[] checkSlots, int id, int index, int place) // ������ ������ ����
    {
        GameObject icon = Instantiate(itemIcon, slots[index].transform.position, Quaternion.identity);
        // icon �Ӽ� ����(itemicon���� �̵�)
        icon.GetComponent<ItemIcon>().itemInfo = items.items[id];
        icon.transform.SetParent(slots[index].transform.GetChild(0));
        checkSlots[index] = true ;
        DataManager.instance.nowPlayerData.haveItems.Add(id, new HaveItemInfo(place, 1, index));
    }

    public void PutHaveItem(int id, GameObject[] slots) // ������ ������ �ִ� ������ �߰�
    {
        HaveItemInfo haveItemInfo = DataManager.instance.nowPlayerData.haveItems[id];
        int slotNum = haveItemInfo.slotNum;
        haveItemInfo.count += 1;
        slots[slotNum].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = haveItemInfo.count.ToString();
    }

    public bool PutItem(int id) // ������ ȹ��
    {
        int place = CheckHaveItem(id);
        if(place == -1) // ���� ���� ���
        {
            return PutNewItem(id);
        } 
        else if(place == 0) // �ش� �������� ������ â�� �ִ� ���
        {
            PutHaveItem(id, itemListSlots);
            return true;
        } 
        else if(place == 1) // ����ǰ â�� �ִ� ���
        {
            PutHaveItem(id, itemSlots);
            return true;
        } 
        else if(place == 2) // �߿买ǰ â�� �ִ� ���
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
