using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;

public class TitleManager : MonoBehaviour
{
    public GameObject startPanel; // ���� ���� ��ư ���� �ǳ�
    public Button newGameBtn; // �� ���� ��ư
    public Button loadGameBtn; // �̾��ϱ� ��ư
    public Button exitGameBtn; // ������ ��ư
    public GameObject newGamePanel; // �� ���� �ǳ�
    public GameObject saveFilePanel; // ���̺� ���� �ǳ�
    public GameObject dialogBox; // ��ȭ����
    public GameObject selectPanel; // ������ �ǳ�
    public GameObject bg; // ��ȭâ ���

    // �� ���� �ǳ� ����
    private string playerName; // �÷��̾� �̸�
    public TMP_InputField playerNameInput; // �÷��̾� �̸� 
    public Button okayBtn; // Ȯ�� ��ư
    public Button newBackBtn; // �� ���� �ǳ� �ڷΰ���
    private bool isOpen = false;

    // �̾��ϱ� �ǳ� ����
    public Button loadBackBtn; // ���̺� �ǳ� �ڷΰ���
    public Button saveInfoOkayBtn; // SaveInfoPanel�� Ȯ�� ��ư
    public GameObject saveInfoPanel; // ���̺� ���� Ȯ�� �ǳ�
    public GameObject[] savePanels; // ���̺� ���� �迭

    public delegate void SelectDel();

    private string path;


    private void Update()
    {
        // �ӽ� �̵�
        if (Input.GetKeyDown(KeyCode.F5))
        {
            DataManager.instance.PlayNewData();
            DataManager.instance.nowPlayerData.playerName = "rescue";
            GameManager.instance.curMap = MapName.BaseMap;
            GameManager.instance.preMap = MapName.Title;

            SceneLoadingManager.LoadScene("BaseMap");
        } else if(Input.GetKeyDown(KeyCode.F6))
        {
            DataManager.instance.PlayNewData();
            DataManager.instance.nowPlayerData.playerName = "rescue";
            GameManager.instance.curMap = MapName.SeaMap;
            GameManager.instance.preMap = MapName.BaseMap;

            SceneLoadingManager.LoadScene("SeaMap");
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isOpen)
            {
                CloseDialog();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        newGameBtn.onClick.AddListener(OpenNewGamePanel);
        loadGameBtn.onClick.AddListener(OpenSaveFilePanel);
        exitGameBtn.onClick.AddListener(GameExit);
        okayBtn.onClick.AddListener(ClickOkayBtn);
        newBackBtn.onClick.AddListener(ClickNewBackBtn);
        loadBackBtn.onClick.AddListener(ClickLoadBackBtn);
        saveInfoOkayBtn.onClick.AddListener(SaveCheckOkayBtn);
        dialogBox.GetComponent<Button>().onClick.AddListener(CloseDialog);

        path = Application.dataPath + "/09. Data/";
    }
    
    public void OpenNewGamePanel() // �� ���� �ǳ� Ȱ��ȭ
    {
        startPanel.SetActive(false); // ��ư ���� ��Ȱ��ȭ

        newGamePanel.SetActive(true);
    }

    public void OpenSaveFilePanel() // ���̺� ���� �ǳ� Ȱ��ȭ
    {
        startPanel.SetActive(false); // ��ư ���� ��Ȱ��ȭ

        saveFilePanel.SetActive(true);
        LoadSaveSlotData();
    }

    public void GameExit() // ���� ����
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else 
        Application.Quit();
#endif
    }

    #region �� ���� ���� �ڵ�
    private void ClickOkayBtn() // Ȯ�� ��ư Ŭ��
    {
        playerName = playerNameInput.text;
        TitleDialog titleDialog = dialogBox.GetComponentInParent<TitleDialog>();
        if (string.IsNullOrEmpty(playerName) || string.IsNullOrWhiteSpace(playerName))
        {
            isOpen = true;
            bg.SetActive(true);
            dialogBox.SetActive(true);
            titleDialog.SetDialogName("");
            titleDialog.SetDialogContent(0, "�̸��� �Է����ֽʽÿ�.");


        } else
        {
            isOpen = true;
            bg.SetActive(true);
            dialogBox.SetActive(true);
            titleDialog.SetDialogName("");
            titleDialog.SetDialogContent(1, playerName + " ��/���� �Ͻðڽ��ϱ�?");

            // ������ �Լ� titledialog�� �����
            string[] selectText = { "��", "�ƴϿ�" };
            titleDialog.MakeSelect(selectText, this);
        }
    }

    private void ClickNewBackBtn() // �ڷΰ��� ��ư Ŭ��
    {
        startPanel.SetActive(true); // ��ư ���� Ȱ��ȭ
        playerNameInput.text = "";
        newGamePanel.SetActive(false);
    }

    private void CloseDialog()
    {
        isOpen = false;
        bg.SetActive(false);
        dialogBox.SetActive(false);
    }
    #endregion

    #region �ҷ����� ���� ��ư
    private void ClickLoadBackBtn() // �ڷΰ��� ��ư Ŭ��
    {
        startPanel.SetActive(true); // ��ư ���� Ȱ��ȭ

        saveFilePanel.SetActive(false);
    }

    private void SaveCheckOkayBtn() // SaveInfoPanel�� Ȯ�� ��ư
    {
        saveInfoPanel.SetActive(false);
    }

    private void LoadSaveSlotData()
    {
        path = Application.dataPath + "/09. Data/";

        for (int i = 0; i < 10; i++)
        {
            if (File.Exists(path + "PlayerSaveData" + i.ToString()))
            {
                DataManager.instance.LoadData(i);
                int playTime = DataManager.instance.nowPlayerData.playTime;
                int sec = playTime % 60;
                int min = (playTime / 60) % 60;
                int hour = (playTime / 60) / 60;

                savePanels[i].GetComponent<SavePanel>().isFile = true;
                savePanels[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = hour.ToString("D2") + ":" + min.ToString("D2") + ":" + sec.ToString("D2");
                savePanels[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = DataManager.instance.nowPlayerData.placeName;
                savePanels[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = DataManager.instance.nowPlayerData.playerName;

            } else
            {
                savePanels[i].GetComponent<SavePanel>().isFile = false;
                savePanels[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
                savePanels[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
                savePanels[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "";
            }
        }
    }
    #endregion

    public void SetSelectResult(string sTag, SelectDel deleteSelect) // ��ư ���� �Լ�
    {
        Debug.Log(sTag);
        
        if (sTag.Equals("Select0"))
        {
            DataManager.instance.PlayNewData();
            DataManager.instance.nowPlayerData.playerName = playerName;
            GameManager.instance.curMap = MapName.BaseMap;
            GameManager.instance.preMap = MapName.Title;

            SceneLoadingManager.LoadScene("BaseMap");
        }
        else
        {
            isOpen = false;
            deleteSelect();
            bg.SetActive(false);
            dialogBox.SetActive(false);
        }
    }
}
