using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CropDrop : MonoBehaviour, IDropHandler
{
    private Transform itemPanelTr; // ������ ���� transform
    private int cropId = 6;

    void Start()
    {
        itemPanelTr = transform.GetChild(0); // �������� �� �θ� 
    }

    public void OnDrop(PointerEventData eventData) // ����� ��
    {
        if (Drag.inDrag) // �巡�� �ϰ� ���� ��
        {
            int iconId = Drag.draggingIcon.GetComponent<ItemIcon>().itemInfo.id;

            if (itemPanelTr.childCount == 0 && (iconId == cropId)) // �������� �� �θ� ������� ���(�� ������ ���) && ������ Ÿ���� ������ â�̰ų� ������ Ÿ�԰� �������� Ÿ���� ���� ���
            {
                Drag.draggingIcon.transform.SetParent(itemPanelTr);
                Drag.draggingIcon.GetComponent<Drag>().oriParentTr = itemPanelTr;
            }
        }
    }
}
