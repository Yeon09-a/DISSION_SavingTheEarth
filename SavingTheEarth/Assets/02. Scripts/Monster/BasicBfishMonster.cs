using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBfishMonster : MonoBehaviour
{
    int HP = 1;

    Animator animator;
    Rigidbody2D rig;
    SpriteRenderer sr;

    float move = -1;

    enum State
    {
        Idle,
        MoveLeft,
        MoveRight,
        Chase
    }

    State state;
    Player player;

    bool cancelWait;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        rig = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        cancelWait = false;
        animator = GetComponent<Animator>();

        while (HP > 0)
        {
            yield return StartCoroutine(state.ToString());
        }
    }

    void StateChange(State next)
    {
        state = next;
        cancelWait = true;

        /*if (state == State.Chase)
        {
            animator.SetBool("IsCatch", true);
        }
        else
        {
            animator.SetBool("IsCatch", false);
        }*/
    }

    void SetNextRoaming()
    {
        // ������ state �� Idle, MoveLeft, MoveRight �̸� ���� �ι� ����
        if (state == State.Idle || state == State.MoveLeft || state == State.MoveRight)
        {
            state = (State)Random.Range(0, 3);
        }
    }

    IEnumerator CancelableWait(float t)
    {
        var d = Time.deltaTime;
        cancelWait = false;

        // ������ �ð� t ��ŭ ���
        // cancelWait == true �� �ߴ�
        while (d < t && cancelWait == false)
        {
            d += Time.deltaTime;
            yield return null;
        }

        if (cancelWait == true) print("Cancled");
    }

    IEnumerator Idle()
    {
        // �������� ����
        move = 0;
        // 1~2 �� ���
        yield return StartCoroutine(CancelableWait((Random.Range(1f, 3f))));

        SetNextRoaming();
    }

    IEnumerator MoveLeft()
    {
        move = -1;
        sr.flipX = false;
        yield return StartCoroutine(CancelableWait((Random.Range(3f, 6f))));

        SetNextRoaming();
    }

    IEnumerator MoveRight()
    {
        move = 1;
        sr.flipX = true;
        yield return StartCoroutine(CancelableWait((Random.Range(3f, 6f))));

        SetNextRoaming();
    }

    IEnumerator Chase()
    {
        if (player != null)
        {
            Vector3 vec;

            do
            {
                yield return new WaitForSeconds(0.5f);

                // Player���� ���� ����
                vec = player.transform.position - transform.position;

                // Player �� �����ʿ� ������
                if (vec.x > 0)
                {
                    sr.flipX = true;
                    move = 2f;
                }
                // Player �� ���ʿ� ������
                else
                {
                    sr.flipX = false;
                    move = -2f;
                }
            }
            // �Ÿ��� 6 �̳��� ���� ���� �ٴϱ�
            while (vec.magnitude < 6f);

            print("End of chase");

            // Player �� �־����� State �� Idle �� ����
            StateChange(State.Idle);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // move ���� ���� x ������ �̵�
        rig.velocity = new Vector2(move, rig.velocity.y);
    }

    // Sensor ��ü�� CircleCollider2D�� �浹�� �߻��ϴ� �̺�Ʈ
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �浹�� ��ü�� �̸��� Player �� �ƴϸ� return
        if (collision.gameObject.name != "Player") return;
        player = collision.gameObject.GetComponent<Player>();
        if (state != State.Chase) StateChange(State.Chase);
    }
}