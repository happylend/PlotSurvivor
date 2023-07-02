using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : EnemyStats
{
    Transform Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<PlayerControl>().transform;
    }

    void OnEnable()
    {

    }


    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Player);
        transform.position = Vector3.MoveTowards(transform.position, Player.position, currentMoveSpeed * Time.deltaTime);
    }
}
