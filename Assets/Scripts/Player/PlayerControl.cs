using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Animator animator; // 动画控制器
    private bool isBlocked; // 是否被阻挡

    public float detectionRadius = 20f;

    Rigidbody rb;
    public Vector3 moveDir;
    public Vector3 PlayerDir;

    bool NoEnemy = true;

    void Start()
    {
        if (animator == null) { animator = this.GetComponentInChildren<Animator>(); }
        rb=GetComponent<Rigidbody>();
    }

    void Update()
    {
        PlayerDir = transform.forward;
        InputManagement();

        if (NoEnemy == false)
        {
            LookEnemy();
        }
    }

    void FixedUpdate()
    {
        Move();

    }

    void InputManagement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal"); // 获取水平轴输入
        float vertical = Input.GetAxisRaw("Vertical"); // 获取垂直轴输入

        moveDir = new Vector3(horizontal, 0, vertical).normalized;
    }

    void Move()
    {

        if (moveDir.x != 0f || moveDir.z != 0f) // 只有有输入才移动和旋转角色
        {
            Vector3 movement = new Vector3(moveDir.x, 0f, moveDir.z);

            // 根据移动方向旋转角色
            if (NoEnemy)
            {
                Quaternion targetRotation = Quaternion.LookRotation(movement, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.15f);
            }


            // 检测是否会碰到障碍物
            RaycastHit hit;
            if (Physics.Raycast(transform.position, movement.normalized, out hit, movement.magnitude))
            {
                if (!hit.collider.isTrigger) // 如果碰撞体不是触发器，表示被阻挡了
                {
                    isBlocked = true;
                    return; // 不进行移动和旋转
                }
            }
            else // 没有碰撞，表示没有被阻挡
            {
                isBlocked = false;
            }

            //调用移动
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
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);
        float minDistance = Mathf.Infinity;
        GameObject closestUnit = null;

        if (colliders.Length == 0)
        {
            NoEnemy = false;
        }
        else
        {
            NoEnemy = true;
        }

        foreach (Collider collider in colliders)
        {
            float distance = Vector3.Distance(transform.position, collider.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestUnit = collider.gameObject;
            }
        }

        if (closestUnit != null)
        {
            transform.LookAt(closestUnit.transform);
        }
    }

}
