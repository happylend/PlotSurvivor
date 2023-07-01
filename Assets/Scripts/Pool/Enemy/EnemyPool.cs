using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    //单次刷怪数量
    [SerializeField] int enemyNum = 10;
    //刷怪间隔
    [SerializeField] float enemyTime = 1;
    [SerializeField]
    float reflashTime = 0f;

    [SerializeField]
    private GameObject Player;

    [SerializeField]
    EnemyStats[] enemyPrefabs;

    [SerializeField]
    List<SurvivorEnemy> enemyPool = new List<SurvivorEnemy>();

    private void Start()
    {
        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }

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

    // Update is called once per frame
    void Update()
    {
        reflashTime += Time.deltaTime;
        if (reflashTime > enemyTime)
        {
            reflashTime = 0f;
            Spawn();

        }

    }

    void Spawn()
    {
        for (int i = 0; i < enemyNum; i++)
        {
            var pool = enemyPool[0];
            pool.Get();
        }
    }
}
