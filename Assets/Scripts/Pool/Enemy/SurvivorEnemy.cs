using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Pool;

public class SurvivorEnemy : BasePool<EnemyStats>
{
    
    [SerializeField]
    private GameObject Player;

    private void Awake()
    {
        Initialize();
    }

    private void Start()
    {
        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            
        }
    }


    protected override EnemyStats OnCreatePoolItem()
    {
        var enemy = base.OnCreatePoolItem();
        enemy.SetDeactivateAction(delegate { Release(enemy); });
        return enemy;
    }

    //调用对象
    protected override void OnGetPoolItem(EnemyStats enemy)
    {
        base.OnGetPoolItem(enemy);
        CreateEnemy(enemy);
    }

    public void SetPrefab(EnemyStats prefab)
    {
        this.prefab = prefab;
    }

    //创建敌人
    void CreateEnemy(EnemyStats enemy)
    {
        enemy.transform.position = Player.transform.position + Random.insideUnitSphere * 200f;
        enemy.transform.position = new Vector3(enemy.transform.position.x, Player.transform.position.y, enemy.transform.position.z);
    }
}
