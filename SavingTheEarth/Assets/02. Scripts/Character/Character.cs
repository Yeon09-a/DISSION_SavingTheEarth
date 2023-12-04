using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

// abstract�� ���� �߻� Ŭ���� ���
public abstract class Character : MonoBehaviour
{
    // �ν�����â�� ������
    [SerializeField]
    private float speed;
    public float hp;
    protected Vector2 direction;
    protected Animator myAnimator;
    protected Rigidbody2D myRigidbody;

    public LayerMask groundMask;

    public bool IsMoving
    {
        get
        {
            return direction.x != 0 || direction.y != 0;
        }
    }

    protected virtual void Start()
    {
        
        myAnimator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // protected�� ��ӹ��� Ŭ���������� ���� ����
    // virtual�� ���ؼ� ��� ����
    protected virtual void Update()
    {
        
        HandleLayers();
    }

    private void FixedUpdate()
    {
        Move();
    }

    // ĳ���� �̵�
    public void Move()
    {
        // direction �� 0f�� �ÿ� ����
        myRigidbody.velocity = direction.normalized * speed;

        if (GameManager.instance.curMap == MapName.SeaMap)
        {
            direction.y = 0f;
        }
    }

    public void HandleLayers()
    {
        if (IsMoving)
        {
            ActivateLayer("Walk Layer");

            myAnimator.SetFloat("x", direction.x);
            myAnimator.SetFloat("y", direction.y);
        }
        else
        {
            ActivateLayer("Idle Layer");
        }
    }

    public void ActivateLayer(string layerName)
    {
        for (int i = 0; i < myAnimator.layerCount; i++)
        {
            myAnimator.SetLayerWeight(i, 0);
        }
        myAnimator.SetLayerWeight(myAnimator.GetLayerIndex(layerName), 1);
    }


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
}