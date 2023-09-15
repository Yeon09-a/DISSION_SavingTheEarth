using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerDir
{ // ĳ���� ���� ����
    Up,
    Down,
    Right,
    Left
};

enum CurMap
{ // �� ������
    BaseMap
}
public class Player : Character
{
    public delegate void BaseMapDel(); // �⺻�� ���� ��������Ʈ
    public event BaseMapDel activateFace; // BaseMapFace Ȱ��ȭ �̺�Ʈ
    public event BaseMapDel deactivateFace; // BaseMapFace ��Ȱ��ȭ �̺�Ʈ

    public PlayerDir playerDir = PlayerDir.Down; // �÷��̾� ���� ����

    private RaycastHit2D hit; // ����ĳ��Ʈ �ᱣ���� �����ϱ� ���� ����ü ����
    private Vector3 interPos; // ��ȣ�ۿ��� ����(���� �߻��� ����)
    private float rayLength; // ���� ����

    // override�� ��ӹ��� Ŭ������ �޼ҵ� �߿��� virtual�� ����� �κ��� ������
    protected override void Update()
    {
        GetInput();
        // base�� ��ӹ��� Ŭ������ ����� ����Ŵ
        base.Move();

        // Ű���� ��ȣ�ۿ� 
        if (Input.GetKeyDown(KeyCode.E)) // ��ȣ�ۿ� Ű
        {
            if (playerDir == PlayerDir.Down) // ������ �Ʒ��� ���
            {
                interPos = Vector3.down;
                rayLength = 1.1f;
            }
            else if (playerDir == PlayerDir.Up) // ������ ���� ���
            {
                interPos = Vector3.up;
                rayLength = 1.1f;
            }
            else if (playerDir == PlayerDir.Right) // ������ �������� ���
            {
                interPos = Vector3.right;
                rayLength = 1.0f;
            }
            else if (playerDir == PlayerDir.Left) // ������ ������ ���
            {
                interPos = Vector3.left;
                rayLength = 1.0f;
            }

            hit = Physics2D.Raycast(transform.position, interPos, rayLength, 1 << 6); // ���� �߻�
            //Debug.DrawRay(transform.position, interPos * rayLength, Color.red);
            if (hit.collider != null) // �浹�� ������Ʈ�� ���� ���
            {
                // ���⿡�� ��ȣ�ۿ�
                // hit.collider�� ���̿� �浹�� ������Ʈ
            }
        }
    }

    // Ű���� �Է°��� ���� (���� �� ���� ����)
    public void GetInput()
    {
        Vector2 moveVector;

        // Input.GetAxisRaw(): ����, ���� ��ư �Է½ÿ� -1f, 1f ��ȯ, �������� ���� 0f ��ȯ
        moveVector.x = Input.GetAxisRaw("Horizontal"); // AŰ or ���� ȭ��ǥ -1f, DŰ or ������ ȭ��ǥ 1f �� ��ȯ
        moveVector.y = Input.GetAxisRaw("Vertical"); // SŰ or �Ʒ��� ȭ��ǥ -1f, WŰ or ���� ȭ��ǥ 1f �� ��ȯ

        direction = moveVector;

        // �÷��̾��� �ִϸ��̼ǿ� ���� ���� ���� ����
       if (myAnimator.GetCurrentAnimatorStateInfo(1).IsName("walk_Up")) 
        {
            playerDir = PlayerDir.Up;
        }
        else if (myAnimator.GetCurrentAnimatorStateInfo(1).IsName("walk_Down"))
        {
            playerDir = PlayerDir.Down;
        }
        else if (myAnimator.GetCurrentAnimatorStateInfo(1).IsName("walk_Right"))
        {
            playerDir = PlayerDir.Right;
        }
        else if (myAnimator.GetCurrentAnimatorStateInfo(1).IsName("walk_Left"))
        {
            playerDir = PlayerDir.Left;
        }
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