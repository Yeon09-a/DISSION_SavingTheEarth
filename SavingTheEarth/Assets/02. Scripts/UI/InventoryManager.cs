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
    public GameObject[] bItemSlots; // ���� ���� �迭

    public static bool[] checkItemList; // ������ â ������ ����ִ��� üũ
    public static bool[] checkItem; // ����ǰ ������ ����ִ��� üũ
    public static bool[] checkPItem; // �߿� ��ǰ ������ ����ִ��� üũ
    public static bool[] checkBItem; // ���� ���� üũ

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
            }
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
        checkItemList = new bool[5];
        checkItem = new bool[15];
        checkPItem = new bool[15];
        checkBItem = new bool[25];
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateInventory();
    }



    public void UpdateInventory()
    {
        // slots �迭 �ʱ�ȭ
        //slots = GetComponentsInChildren<SlotClick>();

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
            else if (item.Value.place == 2) // �߿买ǰ
            {
                LoadItemIcon(pItemSlots, checkPItem, item.Key, item.Value.slotNum, item.Value.count);
            }
            else // ����
            {
                LoadItemIcon(bItemSlots, checkBItem, item.Key, item.Value.slotNum, item.Value.count);
            }
        }
    }

    private void LoadItemIcon(GameObject[] slots, bool[] checkSlots, int id, int index, int count) // ������ ������ �ҷ�����
    {
        GameObject icon = Instantiate(itemIcon, slots[index].transform.position, Quaternion.identity);
        // icon �Ӽ� ����(itemicon���� �̵�)
        icon.GetComponent<ItemIcon>().itemInfo = items.items[id];
        icon.GetComponent<ItemIcon>().SetItemImage();
        icon.transform.SetParent(slots[index].transform.GetChild(0));
        icon.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        checkSlots[index] = true;

        icon.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = count.ToString();
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
            if (checkItemList[i] == false) // �� ���� �߰� ��
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

    private int CheckBItemSlots() // ������ �� ���� ã��
    {
        for (int i = 0; i < checkBItem.Length; i++)
        {
            if (checkBItem[i] == false) // �� ���� �߰� ��
            {
                return i;
            }
        }

        return -1;
    }

    private bool PutNewItem(int id, int count, int wantPlace) // ���� ���� ������ ȹ��
    {
        int lIndex;

        if (wantPlace == 0)
        {
            lIndex = CheckItemList();
        } else
        {
            lIndex = -1;
        }
        
        

        if (lIndex == -1) // ������ â�� �� ������ ���� ���
        {
            if (items.items[id].type == 1) // ������ ������ ����ǰ
            {
                int iIndex = CheckItemSlots();
                if (iIndex != -1) // ����ǰ â�� �� ������ �ִ� ���
                {
                    CreateItemIcon(itemSlots, checkItem, id, iIndex, 1, count);
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
                    CreateItemIcon(pItemSlots, checkPItem, id, pIndex, 2, count);
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
            CreateItemIcon(itemListSlots, checkItemList, id, lIndex, 0, count);
            return true;
        }
    }

    public void CreateItemIcon(GameObject[] slots, bool[] checkSlots, int id, int index, int place, int count) // ������ ������ ����
    {
        GameObject icon = Instantiate(itemIcon, slots[index].transform.position, Quaternion.identity);
        // icon �Ӽ� ����(itemicon���� �̵�)
        ItemIcon iIcon = icon.GetComponent<ItemIcon>();
        iIcon.itemInfo = items.items[id];
        iIcon.SetItemImage();
        icon.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = count.ToString();
        icon.transform.SetParent(slots[index].transform.GetChild(0));
        icon.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        checkSlots[index] = true;
        DataManager.instance.nowPlayerData.haveItems.Add(id, new HaveItemInfo(place, count, index));
    }

    public void PutHaveItem(int id, GameObject[] slots, int count) // ������ ������ �ִ� ������ �߰�
    {
        HaveItemInfo haveItemInfo = DataManager.instance.nowPlayerData.haveItems[id];
        int slotNum = haveItemInfo.slotNum;
        haveItemInfo.count += count;
        slots[slotNum].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = haveItemInfo.count.ToString();
    }



    public bool PutItem(int id, int count, int wantPlace) // ������ ȹ��
    {
        int place = CheckHaveItem(id);
        if (place == -1) // ���� ���� ���
        {
            return PutNewItem(id, count, wantPlace);
        }
        else if (place == 0) // �ش� �������� ������ â�� �ִ� ���
        {
            PutHaveItem(id, itemListSlots, wantPlace);
            return true;
        }
        else if (place == 1) // ����ǰ â�� �ִ� ���
        {
            PutHaveItem(id, itemSlots, count);
            return true;
        }
        else if (place == 2) // �߿买ǰ â�� �ִ� ���
        {
            PutHaveItem(id, pItemSlots, count);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void UseItem(int id, int count) // ������ ���
    {
        DataManager.instance.nowPlayerData.haveItems[id].count -= count;
        HaveItemInfo haveItemInfo = DataManager.instance.nowPlayerData.haveItems[id];
        int slotNum = haveItemInfo.slotNum;
        int ItemType = haveItemInfo.place;

        if (haveItemInfo.count == 0)
        {
            if (ItemType == 0) // �ش� �������� ������ â�� �ִ� ���
            {
                DelItem(id, slotNum, itemListSlots, checkItemList);
            }
            else if (ItemType == 1) // ����ǰ â�� �ִ� ���
            {
                DelItem(id, slotNum, itemSlots, checkItem);
            }
            else if (ItemType == 2) // �߿买ǰ â�� �ִ� ���
            {
                DelItem(id, slotNum, pItemSlots, checkPItem);
            }
        }
        else
        {
            if (ItemType == 0) // �ش� �������� ������ â�� �ִ� ���
            {
                itemListSlots[slotNum].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = haveItemInfo.count.ToString();
            }
            else if (ItemType == 1) // ����ǰ â�� �ִ� ���
            {
                itemSlots[slotNum].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = haveItemInfo.count.ToString();
            }
            else if (ItemType == 2) // �߿买ǰ â�� �ִ� ���
            {
                pItemSlots[slotNum].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = haveItemInfo.count.ToString();
            }
        }
    }

    public void DelItem(int id, int slotNum, GameObject[] slots, bool[] checkSlots) // ������ ����
    {
        Destroy(slots[slotNum].transform.GetChild(0).GetChild(0).gameObject);
        checkSlots[slotNum] = false;
        DataManager.instance.nowPlayerData.haveItems.Remove(id);
    }
}
