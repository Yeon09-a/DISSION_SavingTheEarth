using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenMoving : MonoBehaviour
{
    [Tooltip("NPCMove�� üũ�ϸ� NPC�� ������")]
    public bool NPCMove;
    public string[] direction; // npc�� ������ ���� ����
    [Range(1, 5)]
    [Tooltip("1 = very slow, 2 = slow, 3 = normal, 4 = fast, 5 = continual")]
    public int frequency; // npc ������ �ӵ� ����

    protected Animator anim;

    protected Vector3 vector; // ����
    public float speed; // �ӵ�

    void Start()
    {
        anim = GetComponent<Animator>();

        StartCoroutine(MoveCoroutine());
    }
    void Update()
    {

    }

    public void Moving(string dir)
    {
        StartCoroutine(MovingCoroutine(dir));
    }

    public IEnumerator MovingCoroutine(string dir)
    {
        vector.Set(0, 0, 0);

        switch (dir)
        {
            case "UP":
                vector.y = 1;
                break;
            case "DOWN":
                vector.y = -1;
                break;
            case "LEFT":
                vector.x = -1;
                break;
            case "RIGHT":
                vector.x = 1;
                break;
        }
        // �ִϸ��̼�
        anim.SetFloat("DirX", vector.x);
        anim.SetFloat("DirY", vector.y);
        anim.SetBool("Walking", true);

        // �̵�
        transform.Translate(vector.x * speed, vector.y * speed, 0);
        yield return new WaitForSeconds(0.01f);

        anim.SetBool("Walking", false);
    }

    public void SetMove()
    {

    }
    public void SetNotMove()
    {

    }

    IEnumerator MoveCoroutine()
    {
        if (direction.Length != 0)
        {
            for (int i = 0; i < direction.Length; i++)
            {
                switch (frequency) // �̵� ��⸦ �ɾ ������ �ӵ� ����
                {
                    case 1:
                        yield return new WaitForSeconds(4f);
                        break;
                    case 2:
                        yield return new WaitForSeconds(3f);
                        break;
                    case 3:
                        yield return new WaitForSeconds(2f);
                        break;
                    case 4:
                        yield return new WaitForSeconds(1f);
                        break;
                    case 5:
                        break;
                }

                // �������� �̵�
                Moving(direction[i]);

                if (i == direction.Length - 1) // i �ʱ�ȭ�� ���� ���� �ݺ�
                    i = -1;
            }
        }
    }
}
