using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject inventoryPanel; // �κ��丮 â
    public GameObject itemList; // ������ â
    public GameObject playerInfoPanel; // �÷��̾� ���� â
    public GameObject miniMap; // �̴ϸ�
    public GameObject moneyPanel; // �� �ǳ�
    public GameObject timePanel; // �ð� �ǳ�


    public Button inventoryBtn; // �κ��丮(����) ��ư
    public Toggle itemTgl; // ����ǰ ���
    public GameObject itemPanel; // ����ǰ �ǳ�
    public GameObject pItemPanel; // �߿� ��ǰ �ǳ�
    public GameObject settingPanel; // ���� â
    public Button settingBtn; // ���� â ��ư
    public Button setOkayBtn; // ���� â Ȯ�� ��ư
    public GameObject fullMap; // ��ü �� â
    public Button mapExitBtn; // ��ü �� �ݱ� ��ư
    public Button inventoryExitBtn;

    public Button goTitleBtn; // Ÿ��Ʋ�� ���ư��� ��ư
    public GameObject titleCheckPanel; // Ÿ��Ʋ�� ���ư� ������ Ȯ��
    public Button titleOkayBtn; // ���ư���
    public Button titleNoBtn; // ���

    public GameObject shopPanel; // ���� �ǳ�
    public Button purchaseBtn; // �����ϱ� ��ư
    public Button shopExitBtn; // ���� ������ ��ư

    private bool isInvenOpen = false; // �κ��丮 ����

    public Player player; // �÷��̾�
    public ShopManager shopMng;

    
    // Start is called before the first frame update
    void Start()
    {
        // UI �׼� ����
        player.OpenShop += OpenShop;

        // UI ������ ����
        inventoryBtn.onClick.AddListener(SetInventory);
        itemTgl.onValueChanged.AddListener(OnItemPanel);
        settingBtn.onClick.AddListener(OpenSetting);
        setOkayBtn.onClick.AddListener(CloseSetting);
        mapExitBtn.onClick.AddListener(CloseMap);
        goTitleBtn.onClick.AddListener(OpenCheckTitle);
        titleOkayBtn.onClick.AddListener(ClickTitleOkayBtn);
        titleNoBtn.onClick.AddListener(ClickTitleNoBtn);
        inventoryExitBtn.onClick.AddListener(SetInventory);
        purchaseBtn.onClick.AddListener(PurchaseItem);
        shopExitBtn.onClick.AddListener(CloseShop);
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

    private void OpenSetting() // ���� â ����
    {
        settingPanel.SetActive(true);
        // ���� ���� ���� �߰�
    }

    private void CloseSetting() // ���� â �ݱ�
    {
        settingPanel.SetActive(false);
        // ���� ���� ���� �߰�
    }

    private void CloseMap() // ���� �ݱ�
    {
        fullMap.SetActive(false);
    }

    private void OpenCheckTitle() // Ÿ��Ʋ�� ���ư� ������ Ȯ��
    {
        settingPanel.SetActive(false);
        titleCheckPanel.SetActive(true);
    }

    private void ClickTitleOkayBtn() // Ÿ��Ʋ�� ���ư��� 
    {
        GameManager.instance.curMap = MapName.Title;
        SceneManager.LoadScene("Title");
    }

    private void ClickTitleNoBtn() // Ÿ��Ʋ�� ���ư��� �ʱ�
    {
        settingPanel.SetActive(true);
        titleCheckPanel.SetActive(false);
    }

    public void OpenShop() // ���� ����
    {
        shopPanel.SetActive(true);
    }

    public void CloseShop() // ���� �ݱ�
    {
        shopPanel.SetActive(false);
    }

    public void PurchaseItem() // �����ϱ�
    {
        shopMng.Purchase();
    }

}
