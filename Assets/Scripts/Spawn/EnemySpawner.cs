using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public class Wave
    {
        public string waveName;
        public List<EnemyGroup> enemygroup;
        public string waveQuota;    //每波创建敌人的总数
        public float spawnInterval; //创建敌人的间隔
        public int spawnCount;      //当前波次中场景中已有的敌人数量
    }

    public class EnemyGroup
    {
        public List<string> enemyName;
        public List<int> enemyCount;
        public int spawnCount;
        public GameObject enemyPrefab;

    }

    public List<Wave> waves;    //当前游戏中的所有波次

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
