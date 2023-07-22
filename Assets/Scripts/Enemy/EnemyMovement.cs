using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : EnemyStats
{
    Transform Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<PlayerState>().transform;
    }

    void OnEnable()
    {

    }


    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Player);
        // ����Ƿ�������ϰ���
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward , out hit, 0.01f))
        {
            if (!hit.collider.isTrigger) // �����ײ�岻�Ǵ���������ʾ���赲��
            {
                return; // �������ƶ�����ת
            }
        }
        else // û����ײ����ʾû�б��赲
        {

            transform.position = Vector3.MoveTowards(transform.position, Player.position, currentMoveSpeed * Time.deltaTime);
        }

    }
}
