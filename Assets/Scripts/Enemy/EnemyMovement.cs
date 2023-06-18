using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : EnemyStats
{
    //public EnemyAttributeData enemyData;
    Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.position);
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, enemyData._MoveSpeed * Time.deltaTime);
    }
}
