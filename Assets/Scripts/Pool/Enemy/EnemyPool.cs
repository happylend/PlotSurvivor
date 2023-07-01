using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
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

    }

    public void Spawn(int num)
    {
        var pool = enemyPool[num];
        pool.Get();

    }
}
