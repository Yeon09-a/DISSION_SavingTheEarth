using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject inventoryPanel; // �κ��丮 â
    public GameObject itemList; // ������ â
    public Button inventoryBtn; // �κ��丮(����) ��ư
    public Toggle itemTgl; // ����ǰ ���
    public GameObject itemPanel; // ����ǰ �ǳ�
    public GameObject pItemPanel; // �߿� ��ǰ �ǳ�

    private bool isInvenOpen = false; // �κ��丮 ����

    
    // Start is called before the first frame update
    void Start()
    {
        // UI ������ ����
        inventoryBtn.onClick.AddListener(SetInventory);
        itemTgl.onValueChanged.AddListener(OnItemPanel);
        inventoryBtn.GetComponent<RectTransform>().anchoredPosition = new Vector2(532, 40);
        itemList.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 40);
    }

    private void SetInventory() // �κ��丮 ����
    {
        if (isInvenOpen) // �κ��丮�� �������� ��
        {
            inventoryPanel.SetActive(false);
            isInvenOpen = false;
        }
        else // �κ��丮�� �������� ��
        {
            inventoryPanel.SetActive(true);
            isInvenOpen = true;
        }
    }

    private void OnItemPanel(bool itemTglOn) // ����ǰ, �߿买ǰ �ǳ� �ٲٱ�
    {
        if (itemTglOn) // ����ǰ ��� üũ ��
        {
            itemPanel.SetActive(true);
            pItemPanel.SetActive(false);
        }
        else // ����ǰ ��� ��üũ ��
        {
            itemPanel.SetActive(false);
            pItemPanel.SetActive(true);
        }
    }

}
