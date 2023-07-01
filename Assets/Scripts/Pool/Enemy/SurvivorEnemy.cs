using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Pool;

public class SurvivorEnemy : BasePool<EnemyStats>
{
    
    [SerializeField]
    private GameObject Player;

    [SerializeField]
    private List<Transform> SpawnPoints = new List<Transform>();

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

        foreach(Transform child in GameObject.FindGameObjectWithTag("SpawnPoints").transform)
        {
            SpawnPoints.Add(child);
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
        enemy.transform.position = Player.transform.position + SpawnPoints[Random.Range(0, SpawnPoints.Count)].position; 
    }
}
