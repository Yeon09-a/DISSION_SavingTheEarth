using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    private int money; // ��
    private int count; // ������ ����
    private int price; // ������

    public GameObject purchasePanel; // ���� �ǳ�
    public TextMeshProUGUI dialogText; // ���� ��ȭ
    public Toggle purchaseItemTgl; // ������/���� ���
    public GameObject itemPanel; // ������ �ǳ�
    public GameObject weaponPanel; // ���� �ǳ�
    public RectTransform itemcontent; // ������ ��ũ�Ѻ� content
    public RectTransform weaponContent; // ���� ��ũ�Ѻ� content
    public GameObject countPanel; // ���� �ǳ�
    public Button pBtn; // ���� �߰� ��ư
    public Button mBtn; // ���� ���� ��ư
    public TextMeshProUGUI countText; // ���� ���� �ؽ�Ʈ
    public TextMeshProUGUI purchaseBtnText; // ���� ��ư �ؽ�Ʈ
    public TextMeshProUGUI moneyText; // �� �ؽ�Ʈ
    public TextMeshProUGUI priceText; // ���� �ؽ�Ʈ
    public GameObject cantPurchasePanel; // ���� �ȳ� �ǳ�
    public Button cantOkayBtn; // Ȯ�� ��ư

    public Item selectedItem; // ���õ� ������

    private string[] dialogArr = {"������ ��� �� ������ ��󺸶��!", "�������༭ ����!"};

    public InventoryManager invenMng; 


    // Start is called before the first frame update
    void Start()
    {
        //money = DataManager.instance.nowPlayerData.money;
        moneyText.text = money.ToString();
        priceText.text = "0";
        count = 1;
        dialogText.text = dialogArr[0];

        // ��ũ�Ѻ� content ���� ����
        int itemCount = itemcontent.transform.childCount;
        itemcontent.sizeDelta = new Vector2(itemcontent.sizeDelta.x, 150 * (itemCount / 2) + 10 * (itemCount / 2) + (itemCount % 2 == 1 ? 150 : -10));
        itemCount = weaponContent.transform.childCount;
        weaponContent.sizeDelta = new Vector2(itemcontent.sizeDelta.x, 150 * (itemCount / 2) + 10 * (itemCount / 2) + (itemCount % 2 == 1 ? 150 : -10));

        // UI �̺�Ʈ �߰�
        purchaseItemTgl.onValueChanged.AddListener(OnWeaponPanel);
        pBtn.onClick.AddListener(AddCount);
        mBtn.onClick.AddListener(MinusCount);
        cantOkayBtn.onClick.AddListener(ClickCantOkayBtn);
    }

    private void OnWeaponPanel(bool WeaponTglOn) // ������, ���� �ǳ� �ٲٱ�
    {
        if (WeaponTglOn) // ���� ��� üũ ��
        {
            itemPanel.SetActive(false);
            weaponPanel.SetActive(true);
        }
        else // ���� ��� ��üũ ��
        {
            itemPanel.SetActive(true);
            weaponPanel.SetActive(false);
        }
    }

    public void ChangePurchasePanel() // ���� �ǳ� ������Ʈ
    {
        purchasePanel.SetActive(true);
        count = 1;
        countText.text = count.ToString();
        purchasePanel.transform.GetChild(0).GetComponent<Image>().sprite = selectedItem.image;
        purchasePanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = selectedItem.price.ToString();
        purchasePanel.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = selectedItem.info;

        if(selectedItem.id != 1) // �������� â�� �ƴϸ�
        {
            countPanel.SetActive(true);
            purchaseBtnText.text = "�����ϱ�";
        } 
        else // �������� â�̸�
        {
            countPanel.SetActive(false);
            purchaseBtnText.text = "���׷��̵�";
        }
    }

    private void AddCount() // ���� ���� �߰�
    {
        if(count < 100)
        {
            count++;
            countText.text = count.ToString();
            price = selectedItem.price * count;
            priceText.text = price.ToString();
        }
    }

    private void MinusCount() // ���� ���� ����
    {
        if(count > 1)
        {
            count--;
            countText.text = count.ToString();
            price = selectedItem.price * count;
            priceText.text = price.ToString();
        }
    }

    public void OpenCountPanel() // ���� �ǳ� ����
    {
        countPanel.SetActive(true);
    }

    public void CloseCountPanel() // ���� �ǳ� �ݱ�
    {
        countPanel.SetActive(false);
    }

    public void Purchase() // ����
    {
        if (money >= price)
        {
            bool isNotFull = invenMng.PutItem(selectedItem.id, count, 0);
            if (isNotFull)
            {
                money -= price;
                DataManager.instance.nowPlayerData.money = money;
                moneyText.text = money.ToString();
                dialogText.text = dialogArr[1];
                //invenMng.UpdateInventory();
            }
            else
            {
                cantPurchasePanel.SetActive(true);
                cantPurchasePanel.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = "���濡 �� �ڸ��� ����/n������ �� �����ϴ�.";
            }

        } else
        {
            cantPurchasePanel.SetActive(true);
            cantPurchasePanel.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = "������ �����Ͽ�/n������ �� �����ϴ�. ";
        }

    }

    public void ClickCantOkayBtn() // �ȳ� Ȯ�� ��ư
    {
        cantPurchasePanel.SetActive(false);
    }
}
