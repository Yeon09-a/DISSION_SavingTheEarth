using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrashDrop : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (Drag.inDrag) // �巡�� �ϰ� ���� ��
        {
            Destroy(Drag.draggingIcon);
        }
    }
}
