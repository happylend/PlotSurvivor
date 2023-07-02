using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    //波次参数
    [System.Serializable]
    public class Wave
    {
        [Header("波次名")]
        public string waveName;
        [Header("该波次的敌人种类")]
        public List<EnemyGroup> enemygroup;
        [Header("当前波次的敌人的总数")]
        public int waveQuota;    //每波创建敌人的总数
        [Header("创建敌人间隔")]
        public float spawnInterval; //创建敌人的间隔
        [Header("已创建敌人数量")]
        public int spawnCount;      //当前波次中场景中已有的敌人数量
    }

    //每波中敌人参数
    [System.Serializable]
    public class EnemyGroup
    {
        [Header("敌人名")]
        public string enemyName;
        [Header("敌人数量")]
        public int enemyCount;
        [Header("已创建的敌人数量")]
        public int spawnCount;
        [Header("敌人类型（根据EnemyPool调用）")]
        public int enemyPrefabElementNum;

    }

    public List<Wave> waves;    //当前游戏中的所有波次
    public int currentWaveCount;    //当前波数

    float spawnTimer = 0;

    public int enemiesAlive;
    public int maxEnemiesAllowed;
    public bool maxEnemiesReached = false;
    public float waveInterval;

    public int enemiesKill;

    public List<Transform> relativesSpawnPoints;

    private Transform Player;

    private EnemyPool enemyPool;

    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<PlayerControl>().transform;
        enemyPool = this.GetComponent<EnemyPool>();

        CalculateWaveQuota();
        //SpawnEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentWaveCount < waves.Count && waves[currentWaveCount].spawnCount == 0)
        {
            StartCoroutine(BeginNextWave());
        }

        spawnTimer += Time.deltaTime;
        if (spawnTimer >= waves[currentWaveCount].spawnInterval)
        {
            spawnTimer = 0f;
            SpawnEnemies();
        }
    }

    IEnumerator BeginNextWave()
    {
        yield return new WaitForSeconds(waveInterval);

        //如果有更多的波数在此次之后，开始下一个波数
        if (currentWaveCount < waves.Count - 1)
        {
            currentWaveCount++;
            CalculateWaveQuota();
        }
    }

    void CalculateWaveQuota()
    {
        int currentWaveQuota = 0;
        foreach(var enemyGroup in waves[currentWaveCount].enemygroup)
        {
            currentWaveQuota += enemyGroup.enemyCount;
        }

        waves[currentWaveCount].waveQuota = currentWaveQuota;
        Debug.Log(currentWaveQuota);
    }

    void SpawnEnemies()
    {
        if (waves[currentWaveCount].spawnCount < waves[currentWaveCount].waveQuota && !maxEnemiesReached)
        {
            foreach(var enemyGroup in waves[currentWaveCount].enemygroup)
            {
                if (enemyGroup.spawnCount < enemyGroup.enemyCount)
                {
                    if (enemiesAlive >= maxEnemiesAllowed)
                    {
                        maxEnemiesReached = true;
                        return;
                    }

                    //创建敌人
                    enemyPool.Spawn(enemyGroup.enemyPrefabElementNum);

                    enemyGroup.spawnCount++;
                    waves[currentWaveCount].spawnCount++;
                    enemiesAlive++;
                }
            }
        }

        if (enemiesAlive < maxEnemiesAllowed)
        {
            maxEnemiesReached = false;
        }
    }

    public void OnEnemyKill()
    {
        enemiesAlive--;
        enemiesKill++;
    }



}
