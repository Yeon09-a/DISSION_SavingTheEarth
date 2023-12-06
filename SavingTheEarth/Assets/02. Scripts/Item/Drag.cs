using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Drag : MonoBehaviour, IPointerDownHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Transform canvasTr; // �κ��丮 transform

    private CanvasGroup canvasGroup; // ĵ���� �׷�

    public Transform oriParentTr; // ���� �θ�(����) transform

    public static GameObject draggingIcon = null; // �巡�� �ϰ� �ִ� ������
    
    public static bool inDrag = false; // �巡�� �ϰ� �ִ���
    
    // Start is called before the first frame update
    void Start()
    {
        canvasTr = GameObject.FindWithTag("Canvas").transform;
        canvasGroup = GetComponent<CanvasGroup>();
        oriParentTr = this.transform.parent;
    }

    public void OnPointerDown(PointerEventData eventData) // �Ŀ� �÷��̾� ����(���콺 ���ۿ��� if������ ������ ����)
    {
        inDrag = true;

    }

    public void OnBeginDrag(PointerEventData eventData) // �巡�� ����
    {
        if (inDrag) // �巡�� �ϰ����� ��
        {
            this.transform.SetParent(canvasTr);
            draggingIcon = this.gameObject;

            canvasGroup.blocksRaycasts = false; 
        }
    }

    public void OnDrag(PointerEventData eventData) // �巡�� ��
    {
        if (inDrag) // �巡�� �ϰ����� ��
        {
            this.transform.position = Input.mousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData) // �巡�� �Ϸ�
    {
        if (inDrag) // �巡�� �ϰ����� ��
        {
            draggingIcon = null;
            canvasGroup.blocksRaycasts = true;

            if(this.transform.parent == canvasTr) // ���� �������� �θ� canvas�� ��(�������� �� ���� ���� ���� ���� ���)
            {
                this.transform.SetParent(oriParentTr);
            }

        }
    }
}
