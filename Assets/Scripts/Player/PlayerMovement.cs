using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : PlayerState
{
    public Animator animator; // 动画控制器
    private bool isBlocked; // 是否被阻挡

    Rigidbody rb;
    public Vector3 moveDir;
    public Vector3 PlayerDir;

    // Start is called before the first frame update
    void Start()
    {
        if (animator == null) { animator = this.GetComponentInChildren<Animator>(); }
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
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
            Quaternion targetRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.15f);

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
            transform.position += movement.normalized * currentMoveSpeed * Time.deltaTime;
            animator.SetBool("run", true);
        }

        else
        {
            animator.SetBool("run", false);
        }

    }
}
