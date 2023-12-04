using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public GameObject scanObject; // ���� �浹 ������Ʈ

    public TextMeshProUGUI dialogText; // ��ȭ �ؽ�Ʈ
    public GameObject dialogBox; // ��ȭâ
    public Image portrait; // ĳ���� �ʻ�ȭ

    public GameObject toggle; // ���

    public bool isTalk; // ��ȭ ����
    public ConversationManager convoManager; // ConversationManager : ��� ���� �Ŵ���
    public int dialogIndex;

    public QuestManager questManager; // ����Ʈ ���� �Ŵ���

    private void Start()
    {
        StartCoroutine(GameStartDialog());

        Debug.Log(questManager.CheckQuest());
    }

    IEnumerator GameStartDialog()
    {
        dialogBox.SetActive(true);
        toggle.SetActive(false); // ��� �Ⱥ��̰� ó��
        portrait.gameObject.SetActive(true);
        dialogText.text = "�����԰� ��� ������ ���� �� ���� ������ °...\n\n" +
            "���� nnnnkm ������ ����Կ� ȥ�� ��������.";

        yield return new WaitForSeconds(4f);

        portrait.gameObject.SetActive(true);
        dialogText.text = "������ ���ص� �Ϸ�� ������ ���۵Ǵ±���\n\n���õ� ���Ϸ� �����Ǳ�?";

        yield return new WaitForSeconds(3f);

        dialogText.text = "�ϴ� �����Ƿ� ���� ��������� üũ����";

        yield return new WaitForSeconds(2.5f);

        dialogBox.SetActive(false);
    }

    public void Talk(GameObject scanObj) // ��ȭ ���۽� ȣ��� �Լ�
    {
        scanObject = scanObj;

        ObjectData objData = scanObject.GetComponent<ObjectData>();
        Conversation(objData.id, objData.isNPC);

        dialogBox.SetActive(isTalk); // ��ȭâ �˾�
        toggle.SetActive(true); // ��� �ٽ� ���̰� ó��
    }

    void Conversation(int id, bool isNPC)
    {
        int questTalkIndex = questManager.GetQuestTalkIndex(); // ����Ʈ ��� �ε��� �ҷ����� (return == questId + questActionIndex)
        string dialogData = convoManager.GetDialog(id + questTalkIndex, dialogIndex); // ��� �����ͼ� ���� ( id == objData.Id )

        if (dialogData == null) // ��ȭ�� ������� ��
        {
            isTalk = false;
            dialogIndex = 0; // ��� �ε��� �ʱ�ȭ
            Debug.Log(questManager.CheckQuest(id)); // ���� ����Ʈ�� �Ѿ��

            return; // ����
        }

        if (isNPC) // ��ȭ��밡 NPC���
        {
            dialogText.text = dialogData.Split('/')[0]; // ��ȭâ �ؽ�Ʈ�� ���� (Split()���� ������ ����)

            portrait.sprite = convoManager.GetPortrait(int.Parse(dialogData.Split('/')[1])); // �ʻ�ȭ �̹��� ��ȯ 

            portrait.gameObject.SetActive(true);
        }
        else // ��ȭ��밡 �������̶�� (����)
        {
            dialogText.text = dialogData.Split('/')[0]; // ��ȭâ �ؽ�Ʈ�� ���� (Split()���� ������ ����)

            portrait.sprite = convoManager.GetPortrait(int.Parse(dialogData.Split('/')[1])); // �ʻ�ȭ �̹��� ��ȯ

            if (int.Parse(dialogData.Split('/')[1]) == 6) // �ʻ�ȭ �ʿ� ���� ��ȭ�� ���
                portrait.gameObject.SetActive(false); // �ʻ�ȭ �Ⱥ��̰� (�����ϰ� ó��)
            else
                portrait.gameObject.SetActive(true);
        }

        isTalk = true;
        dialogIndex++; // ���� ���� �Ѿ��
    }

    // ��� ��ư �Լ�
    public void ToggleClick()
    {
        ObjectData objData = scanObject.GetComponent<ObjectData>();
        Conversation(objData.id, objData.isNPC);

        dialogBox.SetActive(isTalk); // ��ȭâ �˾�
    }
}
