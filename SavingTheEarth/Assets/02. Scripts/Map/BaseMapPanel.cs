using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMapPanel : MonoBehaviour
{
    public delegate void BaseMapDel(); // �⺻�� ���� ��������Ʈ
    public event BaseMapDel activateFace; // BaseMapFace Ȱ��ȭ �̺�Ʈ
    public event BaseMapDel deactivateFace; // BaseMapFace ��Ȱ��ȭ �̺�Ʈ

    // �浹 ó��
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("PlayerGroundCollider"))
        {
            activateFace();

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("PlayerGroundCollider"))
        {
            deactivateFace();
        }
    }
}
