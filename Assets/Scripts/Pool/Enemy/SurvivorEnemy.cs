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

    //���ö���
    protected override void OnGetPoolItem(EnemyStats enemy)
    {
        base.OnGetPoolItem(enemy);
        CreateEnemy(enemy);
    }

    public void SetPrefab(EnemyStats prefab)
    {
        this.prefab = prefab;
    }

    //��������
    void CreateEnemy(EnemyStats enemy)
    {
        enemy.transform.position = Player.transform.position + Random.insideUnitSphere * 20f;
    }
}
