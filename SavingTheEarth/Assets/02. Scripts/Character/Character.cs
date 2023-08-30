using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// abstract�� ���� �߻� Ŭ���� ���
public abstract class Character : MonoBehaviour
{
    // �ν�����â�� ������
    [SerializeField]
<<<<<<< Updated upstream
    private float speed;
    protected Vector2 direction;
    private Animator animator;
=======
    protected float speed;
    protected float hp; // ���� ������ ���� �߰�
    protected Vector2 direction;
    protected Animator myAnimator;
    private Rigidbody2D myRigidbody;
>>>>>>> Stashed changes

    void Start()
    {
        myAnimator = GetComponent<Animator>();
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
            myAnimator.SetLayerWeight(1, 0);
        }
    }

    //�Ķ���� ���� ���� �ִϸ��̼� ��ȯ
    public void AnimateMovement(Vector2 direction)
    {
        myAnimator.SetLayerWeight(1, 1);

        myAnimator.SetFloat("x", direction.x);
        myAnimator.SetFloat("y", direction.y);
    }
<<<<<<< Updated upstream
=======



    // ���ǵ� setter
    public void SetSpeed(float sp)
    {
        speed = sp;
    }

    // ���ǵ� getter
    public float GetSpeed()
    {
        return speed;
    }

    // �ִϸ����� setter
    public void SetAnimator(bool b)
    {
        myAnimator.enabled = b;
        myAnimator.Play("idle_Up", 0);
    }
>>>>>>> Stashed changes
}