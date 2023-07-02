using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    //敌人类型
    [SerializeField]
    EnemyStats[] enemyPrefabs;

    //根据敌人类型创建对象池列表
    List<SurvivorEnemy> enemyPool = new List<SurvivorEnemy>();

    private void Start()
    {

        foreach (var prefab in enemyPrefabs)
        {
            var poolHolder = new GameObject($"Pool: {prefab.name}");

            poolHolder.transform.parent = transform;
            poolHolder.transform.position = transform.position;
            poolHolder.SetActive(false);

            var pool = poolHolder.AddComponent<SurvivorEnemy>();

            pool.SetPrefab(prefab);
            poolHolder.SetActive(true);
            enemyPool.Add(pool);
        }
    }

    //获取对象池中的单位
    public void Spawn(int num)
    {
        var pool = enemyPool[num];
        pool.Get();

    }
}
