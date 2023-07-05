using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Animator animator; // ����������
    private bool isBlocked; // �Ƿ��赲

    //�����Χ���
    public float Radius = 20f;
    public Collider[] colliders;

    Rigidbody rb;
    public Vector3 moveDir;
    public Vector3 PlayerDir;

    bool NoEnemy = true;
    GameObject closestUnit = null;

    void Start()
    {
        if (animator == null) { animator = this.GetComponentInChildren<Animator>(); }
        rb=GetComponent<Rigidbody>();
    }

    void Update()
    {
        PlayerDir = transform.forward;
        InputManagement();

        LookEnemy();

    }

    void FixedUpdate()
    {
        Move();

    }

    void InputManagement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal"); // ��ȡˮƽ������
        float vertical = Input.GetAxisRaw("Vertical"); // ��ȡ��ֱ������

        moveDir = new Vector3(horizontal, 0, vertical).normalized;
    }

    void Move()
    {

        if (moveDir.x != 0f || moveDir.z != 0f) // ֻ����������ƶ�����ת��ɫ
        {
            Vector3 movement = new Vector3(moveDir.x, 0f, moveDir.z);

            // �����ƶ�������ת��ɫ
            if (NoEnemy)
            {
                Quaternion targetRotation = Quaternion.LookRotation(movement, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.15f);
            }


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
            //Move(movement);
            animator.SetBool("run", true);
        }

        else
        {
            animator.SetBool("run", false);
        }

    }

    void LookEnemy()
    {
        colliders = Physics.OverlapSphere(transform.position, Radius);
        float minDistance = Mathf.Infinity;

        if(colliders.Length > 0)
        {
            foreach (Collider collider in colliders)
            {
                if (collider.tag.Equals("Enemy"))
                {
                    float distance = Vector3.Distance(transform.position, collider.transform.position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        closestUnit = collider.gameObject;

                    }
                }
            }
            if(closestUnit != null)
            {
                NoEnemy = false;
                transform.LookAt(closestUnit.transform);

            }
            else
            { 
                NoEnemy = true;
            }

        }
        else
        {
            NoEnemy = true;
        }

    }

}
