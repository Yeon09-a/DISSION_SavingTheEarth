using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;



public class Drop : MonoBehaviour, IDropHandler
{
    private Transform itemPanelTr; // ������ ���� transform
    private int type;

    void Start()
    {
        itemPanelTr = transform.GetChild(0); // �������� �� �θ� 
        type = GetComponent<Slot>().type;
    }

    public void OnDrop(PointerEventData eventData) // ����� ��
    {
        if (Drag.inDrag) // �巡�� �ϰ� ���� ��
        {
            int iconType = Drag.draggingIcon.GetComponent<ItemIcon>().type;
            
            if (itemPanelTr.childCount == 0 && (type == 0 || type == iconType)) // �������� �� �θ� ������� ���(�� ������ ���) && ������ Ÿ���� ������ â�̰ų� ������ Ÿ�԰� �������� Ÿ���� ���� ���
            {
                Drag.draggingIcon.transform.SetParent(itemPanelTr);
                Drag.draggingIcon.GetComponent<Drag>().oriParentTr = itemPanelTr;
            }
        }
    }
}
