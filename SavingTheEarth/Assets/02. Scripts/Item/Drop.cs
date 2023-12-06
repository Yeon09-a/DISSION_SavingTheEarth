using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;



public class Drop : MonoBehaviour, IDropHandler
{
    private Transform itemPanelTr; // ������ ���� transform
    private int type;
    private int num;

    void Start()
    {
        itemPanelTr = transform.GetChild(0); // �������� �� �θ� 
        type = GetComponent<Slot>().type;
        num = GetComponent<Slot>().num;

    }

    public void OnDrop(PointerEventData eventData) // ����� ��
    {
        if (Drag.inDrag) // �巡�� �ϰ� ���� ��
        {
            int id = Drag.draggingIcon.GetComponent<ItemIcon>().itemInfo.id;
            int iconType = Drag.draggingIcon.GetComponent<ItemIcon>().itemInfo.type;
            
            if (itemPanelTr.childCount == 0 && (type == 0 || type == iconType)) // �������� �� �θ� ������� ���(�� ������ ���) && ������ Ÿ���� ������ â�̰ų� ������ Ÿ�԰� �������� Ÿ���� ���� ���
            {
                DataManager.instance.nowPlayerData.haveItems[id].place = type;
                DataManager.instance.nowPlayerData.haveItems[id].slotNum = num;
                if(type == 0)
                {
                    InventoryManager.checkItemList[num] = true;
                } else if(type == 1)
                {
                    InventoryManager.checkItem[num] = true;
                } else if(type == 2)
                {
                    InventoryManager.checkPItem[num] = true;
                }
                Drag.draggingIcon.transform.SetParent(itemPanelTr);
                Drag.draggingIcon.GetComponent<Drag>().oriParentTr = itemPanelTr;
            }
        }
    }
}
