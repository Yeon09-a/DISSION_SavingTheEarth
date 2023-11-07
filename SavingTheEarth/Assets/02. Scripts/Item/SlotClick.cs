using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotClick : MonoBehaviour, IPointerClickHandler
{
    public int slotNum; // ���� ��ȣ
    private bool isSelected = false; // ���� ���� ����
    private Color originalColor; // ���� ����

    public Color selectedColor; // ���õ� ����

    // ������ ������ ������ �����ϱ� ���� ����
    private static SlotClick previousSelectedSlot;

    void Start()
    {
        originalColor = GetComponent<Image>().color; // ������ ���� ���� ����
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ToggleSelection();
    }

    public void ToggleSelection()
    {
        isSelected = !isSelected;

        // ������ ������ ������ ���� ����
        if (previousSelectedSlot != null && previousSelectedSlot != this)
        {
            previousSelectedSlot.Deselect();
        }

        if (isSelected)
        {
            Color blackColor = Color.black;
            GetComponent<Image>().color = blackColor; // ���õ� ������ ���� ����
        }
        else
        {
            GetComponent<Image>().color = originalColor; // ���� ������ ������ ���� �������
        }

        // ������ ������ ���� ������Ʈ
        previousSelectedSlot = isSelected ? this : null;
    }

    public void Deselect()
    {
        isSelected = false;
        GetComponent<Image>().color = originalColor; // ���� ������ ������ ���� �������
    }

    public bool IsSelected()
    {
        return isSelected;
    }
}
