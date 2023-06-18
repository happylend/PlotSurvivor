using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : EnemyStats
{
    Transform player;

    public float obstacleDetectionRadius = 1f;
    public LayerMask obstacleLayerMask;
    float distance;

    private bool isMoving = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (isMoving)
        {
            MoveToTarget();
        }
    }

    private void MoveToTarget()
    {
        // ����Ŀ�귽��;���
        Vector3 direction = player.position - transform.position;
        distance = direction.magnitude;

        // ����Ƿ����ϰ���
        Collider[] colliders = Physics.OverlapSphere(transform.position, obstacleDetectionRadius, obstacleLayerMask);
        if (colliders.Length > 0)
        {
            // ���ϰ�������ƿ�
            Vector3 avoidanceDirection = Vector3.zero;
            foreach (Collider collider in colliders)
            {
                Vector3 colliderDirection = collider.transform.position - transform.position;
                avoidanceDirection += Vector3.Cross(Vector3.up, colliderDirection.normalized);
            }
            direction += avoidanceDirection.normalized * obstacleDetectionRadius;
        }

        // �������С�ڵ����ƶ��ٶȣ�����Ŀ��
        if (distance <= enemyData._MoveSpeed * Time.deltaTime)
        {
            isMoving = false;
        }
        else
        {
            // ��������ƶ�
            transform.Translate(direction.normalized * enemyData._MoveSpeed * Time.deltaTime, Space.World);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isMoving = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isMoving = true;
        }
    }
    //public EnemyAttributeData enemyData;


    // Start is called before the first frame update


}
