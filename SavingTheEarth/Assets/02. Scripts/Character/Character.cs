using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// abstract�� ���� �߻� Ŭ���� ���
public abstract class Character : MonoBehaviour
{
    // �ν�����â�� ������
    [SerializeField]
    private float speed;
    protected Vector2 direction;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // protected�� ��ӹ��� Ŭ���������� ���� ����
    // virtual�� ���ؼ� ��� ����
    protected virtual void Update()
    {
        Move();
    }

    // ĳ���� �̵�
    public void Move()
    {
        // direction �� 0f�� �ÿ� ����
        transform.Translate(direction * speed * Time.deltaTime);

        if (direction.x != 0 || direction.y != 0)
        {
            AnimateMovement(direction);
        }
        else
        {
            animator.SetLayerWeight(1, 0);
        }
    }

    //�Ķ���� ���� ���� �ִϸ��̼� ��ȯ
    public void AnimateMovement(Vector2 direction)
    {
        animator.SetLayerWeight(1, 1);

        animator.SetFloat("x", direction.x);
        animator.SetFloat("y", direction.y);
    }
}