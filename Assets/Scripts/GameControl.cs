using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    EnemySpawner enemySpawner;
    PlayerState playerState;
    WeaponController weaponController;

    // Start is called before the first frame update
    void Start()
    {
        if (enemySpawner == null)  { enemySpawner = FindObjectOfType<EnemySpawner>(); }
        if (playerState == null) { playerState = FindObjectOfType<PlayerState>(); }
        if (weaponController == null) { weaponController = FindObjectOfType<WeaponController>(); }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //敌人死亡事件
    public void WhenEnemyDie()
    {
        enemySpawner.OnEnemyKill();
        weaponController.IncreaseExperience(1);
    }

    //角色升级事件
    public void PlayerLevelUP()
    {

    }

    //武器升级事件
    public void WeaponLevelUP()
    {

    }

    //武器进化事件
    public void WeaponEvolution()
    {

    }
}
