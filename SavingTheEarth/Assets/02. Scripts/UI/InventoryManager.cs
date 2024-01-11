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
    public GameObject[] itemListSlots; // ȭ�� �ϴ��� ������(������ â) ���� �迭
    public GameObject[] itemSlots; // �κ��丮�� ����ǰ ���� �迭
    public GameObject[] pItemSlots; // �κ��丮�� �߿� ��ǰ ���� �迭
    public GameObject[] bItemSlots; // ���� ���� �迭

    public static bool[] checkItemList; // ������(������ â) ������ ����ִ��� üũ�ϱ� ���� �迭
    public static bool[] checkItem; // �κ��丮�� ����ǰ ������ ����ִ��� üũ�ϱ� ���� �迭
    public static bool[] checkPItem; // �κ��丮�� �߿� ��ǰ ������ ����ִ��� üũ�ϱ� ���� �迭
    public static bool[] checkBItem; // ���� ������ ����ִ��� üũ�ϱ� ���� �迭

    public GameObject itemIcon; // ������ ������ ������

    public ItemDic items; // ��� ������ �����͸� ������ �ִ� ScriptableObject(������ id�� ���� ������ ������ �迭�� ����)

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
        foreach (KeyValuePair<int, HaveItemInfo> item in DataManager.instance.nowPlayerData.haveItems) // ������ ������ ��ųʸ� ��������
        {
            if (item.Value.place == 0) // ������ �������� ��ġ�� �������� ���
            {
                // ���Կ� ���� ������ ������ ����
                LoadItemIcon(itemListSlots, checkItemList, item.Key, item.Value.slotNum, item.Value.count);
            }
            else if (item.Value.place == 1) // ������ �������� ��ġ�� �κ��丮 ����ǰ�� ���
            {
                LoadItemIcon(itemSlots, checkItem, item.Key, item.Value.slotNum, item.Value.count);
            }
            else if (item.Value.place == 2) // ������ �������� ��ġ�� �κ��丮 �߿买ǰ�� ���
            {
                LoadItemIcon(pItemSlots, checkPItem, item.Key, item.Value.slotNum, item.Value.count);
            }
            else // ������ �������� ��ġ�� ������ ���
            {
                LoadItemIcon(bItemSlots, checkBItem, item.Key, item.Value.slotNum, item.Value.count);
            }
        }
    }

    private void LoadItemIcon(GameObject[] slots, bool[] checkSlots, int id, int index, int count) // ������ ������ �ҷ�����
    {
        GameObject icon = Instantiate(itemIcon, slots[index].transform.position, Quaternion.identity); // ������ ����
        // icon �Ӽ� ����(itemicon���� �̵�)
        icon.GetComponent<ItemIcon>().itemInfo = items.items[id]; // ������ ���� ����
        icon.GetComponent<ItemIcon>().SetItemImage(); // ������ �̹��� ����
        icon.transform.SetParent(slots[index].transform.GetChild(0)); // �������� ���Կ� ����(������ �������� �θ� ������Ʈ�� �������� ������)
        icon.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1); // ������ ������ ũ�� ����
        checkSlots[index] = true; // �ش� ���Կ� �������� �ִٰ� ǥ��

        icon.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = count.ToString(); // ������ �����ܿ� ������ ���� ǥ��
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

    private int CheckItemSlot(bool[] checkSlots) // �� ���� ã��
    {
        for (int i = 0; i < checkSlots.Length; i++)
        {
            if (checkSlots[i] == false) // �� ���� �߰� ��
            {
                return i;
            }
        }

        return -1;
    }


    private bool PutNewItem(int id, int count, int wantPlace) // ���� ���� ������ ȹ��
    {
        if(wantPlace == 0)
        {
            int slotNum = CheckItemSlot(checkItemList);
            if(slotNum != -1)
            {
                CreateItemIcon(itemListSlots, checkItemList, id, slotNum, 0, count);
                return true;
            }
        }

        int type = items.items[id].type;
        
        if(wantPlace == 1 || type == 1)
        {
            int slotNum = CheckItemSlot(checkItem);
            if (slotNum != -1) // ����ǰ â�� �� ������ �ִ� ���
            {
                CreateItemIcon(itemSlots, checkItem, id, slotNum, 1, count);
                return true;
            }
            else // �� ������ ���� ���
            {
                return false;
            }
        }
        else if(wantPlace == 2 || type == 2)
        {
            int slotNum = CheckItemSlot(checkPItem);
            if (slotNum != -1) // �߿� ��ǰ â�� �� ������ �ִ� ���
            {
                CreateItemIcon(pItemSlots, checkPItem, id, slotNum, 2, count);
                return true;
            }
            else // �� ������ ���� ���
            {
                return false;
            }
        } 
        else if(wantPlace == 3 || type == 3)
        {
            int slotNum = CheckItemSlot(checkBItem);
            if (slotNum != -1) // �߿� ��ǰ â�� �� ������ �ִ� ���
            {
                CreateItemIcon(bItemSlots, checkBItem, id, slotNum, 3, count);
                return true;
            }
            else // �� ������ ���� ���
            {
                return false;
            }
        } else
        {
            return false;
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
        int place = CheckHaveItem(id); // ���� �������� ������ ������ �ִ��� Ȯ���Ѵ�.
        if (place == -1) // ���� ���� ���
        {
            return PutNewItem(id, count, wantPlace); // ���� �κ��丮�� �������� �߰��Ѵ�.
        }
        else if (place == 0) // �ش� �������� ������â(������)�� �ִ� ���
        {
            PutHaveItem(id, itemListSlots, count); // ������ ������ �ִ� �������� ������ �߰��Ѵ�.
            return true;
        }
        else if (place == 1) // ����ǰ â�� �ִ� ���
        {
            PutHaveItem(id, itemSlots, count); // ������ ������ �ִ� �������� ������ �߰��Ѵ�.
            return true;
        }
        else if (place == 2) // �߿买ǰ â�� �ִ� ���
        {
            PutHaveItem(id, pItemSlots, count); // ������ ������ �ִ� �������� ������ �߰��Ѵ�.
            return true;
        }
        else
        {
            return false;
        }
    }

    public void UseItem(int id, int count) // ������ ���
    {
        HaveItemInfo haveItemInfo = DataManager.instance.nowPlayerData.haveItems[id]; // ����� �������� ���� ���� ��������
        int slotNum = haveItemInfo.slotNum; // �������� ���� ��ȣ ��������
        int ItemType = haveItemInfo.place; // �������� �з� ��ġ ��������
        int iCount = haveItemInfo.count;

        if (iCount == count) // �������� ������� �� ������ 0���� ���� ���
        {
            if (ItemType == 0) // �ش� �������� �����Կ� �ִ� ���
            {
                DelItem(id, slotNum, itemListSlots, checkItemList); // �����Կ��� ������ ������ ��
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
        else if (iCount > count) // ������ ��� �� ������ ������ ���
        {
            haveItemInfo.count -= count;

            if (ItemType == 0) // �ش� �������� �����Կ� �ִ� ���
            {
                itemListSlots[slotNum].transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = haveItemInfo.count.ToString(); // ������ ���� UI ������Ʈ
            }
            else if (ItemType == 1) // ����ǰ â�� �ִ� ���
            {
                itemSlots[slotNum].transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = haveItemInfo.count.ToString();
            }
            else if (ItemType == 2) // �߿买ǰ â�� �ִ� ���
            {
                pItemSlots[slotNum].transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = haveItemInfo.count.ToString();
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
