using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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

    // �̾��ϱ� �ǳ� ����
    public Button loadBackBtn; // ���̺� �ǳ� �ڷΰ���

    // ���̺� ������ ���� ��ũ��Ʈ �����

    public delegate void SelectDel();


    private void Update()
    {
        // �ӽ� �̵�
        if (Input.GetKeyDown(KeyCode.F5))
        {
            GameManager.instance.curMap = MapName.BaseMap;
            GameManager.instance.preMap = MapName.Title;

            SceneManager.LoadScene("BaseMap");
            SceneManager.LoadScene("Player", LoadSceneMode.Additive);
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
            bg.SetActive(true);
            dialogBox.SetActive(true);
            titleDialog.SetDialogName("");
            titleDialog.SetDialogContent(0, "�̸��� �Է����ֽʽÿ�.");

        } else
        {
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
    #endregion

    #region �ҷ����� ���� ��ư
    private void ClickLoadBackBtn() // �ڷΰ��� ��ư Ŭ��
    {
        startPanel.SetActive(true); // ��ư ���� Ȱ��ȭ

        saveFilePanel.SetActive(false);
    }
    #endregion

    public void SetSelectResult(string sTag, SelectDel deleteSelect) // ��ư ���� �Լ�
    {
        Debug.Log(sTag);
        
        if (sTag.Equals("Select0"))
        {
            PlayerData.instance.playerName = playerName;
            GameManager.instance.curMap = MapName.BaseMap;
            GameManager.instance.preMap = MapName.Title;

            SceneManager.LoadScene("BaseMap");
            SceneManager.LoadScene("Player", LoadSceneMode.Additive);
        }
        else
        {
            deleteSelect();
            bg.SetActive(false);
            dialogBox.SetActive(false);
        }
    }
}
