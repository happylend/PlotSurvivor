using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerControl : AttributeBase
{
    public Animator animator; // ����������
    private bool isBlocked; // �Ƿ��赲

    Rigidbody rb;
    public Vector3 moveDir;
    public Vector3 PlayerDir;

    void Start()
    {
        if (animator == null) { animator = this.GetComponentInChildren<Animator>(); }
        rb=GetComponent<Rigidbody>();
    }

    void Update()
    {
        PlayerDir = transform.forward;
        InputManagement();
    }

    void FixedUpdate()
    {
        Move();


    }

    void InputManagement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal"); // ��ȡˮƽ������
        float vertical = Input.GetAxisRaw("Vertical"); // ��ȡ��ֱ������

        moveDir=new Vector3(horizontal, 0, vertical).normalized;
    }

    void Move()
    {

        if (moveDir.x != 0f || moveDir.z != 0f) // ֻ����������ƶ�����ת��ɫ
        {
            Vector3 movement = new Vector3(moveDir.x, 0f, moveDir.z);

            // �����ƶ�������ת��ɫ
            Quaternion targetRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.15f);

            // ����Ƿ�������ϰ���
            RaycastHit hit;
            if (Physics.Raycast(transform.position, movement.normalized, out hit, movement.magnitude))
            {
                if (!hit.collider.isTrigger) // �����ײ�岻�Ǵ���������ʾ���赲��
                {
                    isBlocked = true;
                    return; // �������ƶ�����ת
                }
            }
            else // û����ײ����ʾû�б��赲
            {
                isBlocked = false;
            }

            //�����ƶ�
            Move(movement);
            animator.SetBool("run", true);
        }

        else
        {
            animator.SetBool("run", false);
        }

    }

}
