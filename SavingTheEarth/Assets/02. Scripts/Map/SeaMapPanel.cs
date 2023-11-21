using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaMapPanel : MonoBehaviour
{
    public delegate void SeaMapDel(); // �⺻�� ���� ��������Ʈ
    public event SeaMapDel activateFace; // SeaMapFace Ȱ��ȭ �̺�Ʈ
    public event SeaMapDel deactivateFace; // SeaMapFace ��Ȱ��ȭ �̺�Ʈ

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
