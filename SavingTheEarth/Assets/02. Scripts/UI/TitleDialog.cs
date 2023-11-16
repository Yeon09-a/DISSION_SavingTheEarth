using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TitleDialog : MonoBehaviour// Ÿ��Ʋ ��ȭ �ý���
{
    public TextMeshProUGUI dialogName; // ��ȭâ �̸�
    public TextMeshProUGUI dialogContent; // ��ȭâ ����
    public Image dialogToggle; // ��ȭâ ���
    public Transform selectPanel; // ������ �ǳ�
    public GameObject[] selectBtns; // ��������

    private int eventNum; // �̺�Ʈ ��ȣ

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetDialogName(string text) // ��ȭâ �̸� ����
    {
        dialogName.text = text;
    }

    public void SetDialogContent(int i, string text) // ��ȭâ ���� ����
    {
        eventNum = i;
        dialogContent.text = text;
    }

    public void MakeSelect(string[] selects, TitleManager titleManager) // ������ �����
    {
        selectPanel.gameObject.SetActive(true);
        for(int i = 0; i < selects.Length; i++)
        {
            selectBtns[i].SetActive(true);
            selectBtns[i].GetComponentInChildren<TextMeshProUGUI>().text = selects[i];
            string bTag = selectBtns[i].tag;
            selectBtns[i].GetComponent<Button>().onClick.AddListener(() => {titleManager.SetSelectResult(bTag, DeleteSelect);});
        }
    }

    public void DeleteSelect() // ������ ����
    {
        for(int i = 0; i < 3; i++)
        {
            selectBtns[i].GetComponent<Button>().onClick.RemoveAllListeners();
            selectBtns[i].SetActive(false);
        }
        selectPanel.gameObject.SetActive(false);
    }
}
