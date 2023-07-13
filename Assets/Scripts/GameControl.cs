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


    //���������¼�
    public void WhenEnemyDie()
    {
        enemySpawner.OnEnemyKill();
        weaponController.LevelUp();
    }

    //��ɫ�����¼�
    public void PlayerLevelUP()
    {

    }

    //���������¼�
    public void WeaponLevelUP()
    {

    }

    //���������¼�
    public void WeaponEvolution()
    {

    }
}
