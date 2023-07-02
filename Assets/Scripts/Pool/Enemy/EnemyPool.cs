using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    //��������
    [SerializeField]
    EnemyStats[] enemyPrefabs;

    //���ݵ������ʹ���������б�
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

    //��ȡ������еĵ�λ
    public void Spawn(int num)
    {
        var pool = enemyPool[num];
        pool.Get();

    }
}
