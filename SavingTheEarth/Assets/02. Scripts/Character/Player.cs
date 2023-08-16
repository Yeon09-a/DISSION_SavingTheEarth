using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum CurMap
{ // �� ������
    BaseMap
}
public class Player : Character
{
    public delegate void BaseMapDel(); // �⺻�� ���� ��������Ʈ
    public event BaseMapDel activateFace; // BaseMapFace Ȱ��ȭ �̺�Ʈ
    public event BaseMapDel deactivateFace; // BaseMapFace ��Ȱ��ȭ �̺�Ʈ

    // override�� ��ӹ��� Ŭ������ �޼ҵ� �߿��� virtual�� ����� �κ��� ������
    protected override void Update()
    {
        GetInput();
        // base�� ��ӹ��� Ŭ������ ����� ����Ŵ
        base.Move();
    }

    // Ű���� �Է°��� ���� (���� �� ���� ����)
    public void GetInput()
    {
        Vector2 moveVector;

        // Input.GetAxisRaw(): ����, ���� ��ư �Է½ÿ� -1f, 1f ��ȯ, �������� ���� 0f ��ȯ
        moveVector.x = Input.GetAxisRaw("Horizontal"); // AŰ or ���� ȭ��ǥ -1f, DŰ or ������ ȭ��ǥ 1f �� ��ȯ
        moveVector.y = Input.GetAxisRaw("Vertical"); // SŰ or �Ʒ��� ȭ��ǥ -1f, WŰ or ���� ȭ��ǥ 1f �� ��ȯ

        direction = moveVector;
    }

    // ���콺 ��ȣ�ۿ� �߰��ϱ�
    // �浹 ó��
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("BaseFace"))
        {
            activateFace();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("BaseFace"))
        {
            deactivateFace();
        }
    }
}