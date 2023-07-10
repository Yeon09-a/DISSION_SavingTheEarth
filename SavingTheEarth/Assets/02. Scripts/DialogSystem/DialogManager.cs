using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public Text dialogText; // ��ȭ �ؽ�Ʈ
    public GameObject dialogBox; // ��ȭâ
    public Image portrait; // ĳ���� �ʻ�ȭ

    public bool isTalk; // ��ȭ ����
    public ConversationManager convoManager; // ConversationManager : ��� ���� �Ŵ���
    public int dialogIndex;

    public void Talk() // ��ȭ ���۽� ȣ��� �Լ� => �÷��̾� ��ũ��Ʈ���� ȣ�� �ʿ�!!
    {
        //ObjectData objData = ~~~.GetComponent<ObjectData>(); // �Ŀ� ����
        //Conversation(objData.id, objData.isNPC);

        //dialogBox.SetActive(isTalk); // ��ȭâ �˾�
    }

    void Conversation(int id, bool isNPC)
    {
        // string dialogData = convoManager.GetDialog(id, dialogIndex); // ��� �����ͼ� ����

        //if(dialogData == null) // ��ȭ�� ������� ��
        //{
        //    isTalk = false;
        //    dialogIndex = 0; // ��� �ε��� �ʱ�ȭ
        //    return; // ����
        //}

        if (isNPC) // ��ȭ��밡 NPC���
        {
            // dialogText.text = dialogData.Split(':')[0]; // ��ȭâ �ؽ�Ʈ�� ���� (Split()���� ������ ����)

            // portrait.sprite = convoManager.GetPortrait(id, int.Parse(dialogData.Split(':')[1])); // �ʻ�ȭ �̹��� ��ȯ
            portrait.color = new Color(1, 1, 1, 1); // NPC�� ���� �ʻ�ȭ ����
        }
        else // ��ȭ��밡 �������̶��
        {
            // dialogText.text = dialogData;

            portrait.color = new Color(1, 1, 1, 0); // NPC�� �ƴ϶�� �ʻ�ȭ �Ⱥ��̰�
        }

        isTalk = true;
        dialogIndex++; // ���� ���� �Ѿ��
    }
}