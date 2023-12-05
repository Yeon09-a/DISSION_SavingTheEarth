using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public Button inventoryExitBtn; // �κ��丮 �ݱ� ��ư
    public GameObject trashcan; // ��������

    public Button goTitleBtn; // Ÿ��Ʋ�� ���ư��� ��ư
    public GameObject titleCheckPanel; // Ÿ��Ʋ�� ���ư� ������ Ȯ��
    public Button titleOkayBtn; // ���ư���
    public Button titleNoBtn; // ���

    public GameObject shopPanel; // ���� �ǳ�
    public Button purchaseBtn; // �����ϱ� ��ư
    public Button shopExitBtn; // ���� ������ ��ư

    private bool isInvenOpen = false; // �κ��丮 ����

    public GameObject farmTool; // ��� ���� UI
    public Image toolImage; // ��� ���� �̹���
    public TextMeshProUGUI toolCount; // ���� ����
    public Sprite[] tools; // 0 : ���� ���� 1 : ȣ��, 2 : ���Ѹ���, 3 : �ٱ���
    public GameObject farmInfo; // ��� ����
    public TextMeshProUGUI farmInfoText; // ��� ���� �ؽ�Ʈ

    public GameObject box; // ����UI


    public Player player; // �÷��̾�
    public PlayerFarm playerFarm;
    public ShopManager shopMng;

    private float oriPosY;


    // Start is called before the first frame update
    void Start()
    {
        player.OpenShop -= OpenShop;
        player.OpenFarmTool -= OpenFarmTool;
        player.CloseFarmTool -= CloseFarmTool;
        player.ChangeFarmTool -= ChangeFarmTool;
        playerFarm.SetSeedCount -= SetToolCount;
        playerFarm.OnFarmInfo -= OnFarmInfo;
        player.OpenBox -= SetBox;

        inventoryBtn.onClick.RemoveAllListeners();
        itemTgl.onValueChanged.RemoveAllListeners();
        settingBtn.onClick.RemoveAllListeners();
        setOkayBtn.onClick.RemoveAllListeners();
        mapExitBtn.onClick.RemoveAllListeners();
        goTitleBtn.onClick.RemoveAllListeners();
        titleOkayBtn.onClick.RemoveAllListeners();
        titleNoBtn.onClick.RemoveAllListeners();
        inventoryExitBtn.onClick.RemoveAllListeners();
        purchaseBtn.onClick.RemoveAllListeners();
        shopExitBtn.onClick.RemoveAllListeners();

        // UI �׼� ����
        player.OpenShop += OpenShop;
        player.OpenFarmTool += OpenFarmTool;
        player.CloseFarmTool += CloseFarmTool;
        player.ChangeFarmTool += ChangeFarmTool;
        playerFarm.SetSeedCount += SetToolCount;
        playerFarm.OnFarmInfo += OnFarmInfo;
        player.OpenBox += SetBox;

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

        oriPosY = itemList.GetComponent<RectTransform>().localPosition.y;
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
        SceneLoadingManager.LoadScene("Title");
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

    private void OpenFarmTool() // ��� ���� UI ����
    {
        farmTool.SetActive(true);
    }

    private void CloseFarmTool() // ��� ���� UI �ݱ�
    {
        toolImage.sprite = tools[0];
        farmTool.SetActive(false);
    }

    private void ChangeFarmTool(int toolNum) // ��� UI �ٲٱ�
    {
        toolImage.sprite = tools[toolNum];
        if (toolNum != 3)
        {
            toolCount.gameObject.SetActive(false);
        }
        else
        {
            toolCount.gameObject.SetActive(true);
            toolCount.text = (DataManager.instance.nowPlayerData.haveItems.ContainsKey(6) ? DataManager.instance.nowPlayerData.haveItems[6].count : 0).ToString();
            playerFarm.seedCount = int.Parse(toolCount.text);
        }
    }

    public void SetToolCount(int count) // ��� UI ���� ǥ��
    {
        toolCount.text = count.ToString();
    }

    public void OnFarmInfo(string infoStr) // ��� Info ǥ��
    {
        farmInfo.SetActive(false);
        farmInfo.SetActive(true);
        farmInfoText.text = infoStr;
    }

    private void SetBox() // �ڽ� ����
    {
        inventoryPanel.GetComponent<RectTransform>().localPosition = new Vector3(324f, 56f, 0f);
        itemList.GetComponent<RectTransform>().localPosition = new Vector3(324f, oriPosY + 56, 0f);
        isInvenOpen = true;
        inventoryExitBtn.onClick.RemoveAllListeners();
        inventoryExitBtn.onClick.AddListener(CloseBox);
        trashcan.SetActive(false);
        inventoryPanel.SetActive(true);
        box.SetActive(true);
    }

    private void CloseBox() // �ڽ� �ݱ�
    {
        inventoryPanel.GetComponent<RectTransform>().localPosition = new Vector3(-19f, 0, 0);
        itemList.GetComponent<RectTransform>().localPosition = new Vector3(-19f, oriPosY, 0f);
        isInvenOpen = true;
        inventoryExitBtn.onClick.RemoveAllListeners();
        inventoryExitBtn.onClick.AddListener(SetInventory);
        trashcan.SetActive(false);
        inventoryPanel.SetActive(false);
        box.SetActive(false);
    }
}
