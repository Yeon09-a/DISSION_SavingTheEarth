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
    private Rigidbody2D myRigidbody;

    public LayerMask groundMask;

    public float distance;

    public Transform chkPos;
    public bool isGround;
    public float checkRadius;

    public bool IsMoving
    {
        get
        {
            return direction.x != 0 || direction.y != 0;
        }
    }

    protected virtual void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
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

        // Ư�� �������� y�� �̵� ����
        if (GameManager.instance.curMap == MapName.SeaMap)
        {

            if (direction.x == 0)
                myRigidbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePosition;
            else
                myRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;

            //�������� �߰��Լ�
            //RaycastHit2D hit = Physics2D.Raycast(chkPos.position, Vector2.down, distance, groundMask);
            //angle = Vector2.Angle(hit.normal, Vector2.up);

            //Debug.DrawLine(hit.point, hit.point + hit.normal, Color.yellow);

            direction.y = 0f;

            Rigidbody2D playerRigidbody = GetComponent<Rigidbody2D>();
            if (playerRigidbody != null)
            {
                playerRigidbody.gravityScale = 10f;
            }

            // MoveCollider ������Ʈ�� �ڽ� �ݶ��̴��� ã�� ��Ȱ��ȭ
            GameObject moveCollider = transform.Find("MoveCollider").gameObject;
            if (moveCollider != null)
            {
                BoxCollider2D boxCollider = moveCollider.GetComponent<BoxCollider2D>();
                if (boxCollider != null)
                {
                    boxCollider.enabled = false;
                }
            }
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