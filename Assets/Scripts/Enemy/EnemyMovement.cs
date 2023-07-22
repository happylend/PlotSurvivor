using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : EnemyStats
{
    Transform Player;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<PlayerState>().transform;
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {

    }


    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Player);
        // 检测是否会碰到障碍物
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward , out hit, 0.05f))
        {
            if (!hit.collider.isTrigger) // 如果碰撞体不是触发器，表示被阻挡了
            {
                return; // 不进行移动和旋转
            }
        }
        else // 没有碰撞，表示没有被阻挡
        {
            //rb.velocity = Player.position * currentMoveSpeed;
            transform.position = Vector3.MoveTowards(transform.position, Player.position, currentMoveSpeed * Time.deltaTime);
        }

    }
}
