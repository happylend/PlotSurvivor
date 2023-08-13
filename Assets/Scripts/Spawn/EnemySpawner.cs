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
        public int waveQuota;   
        [Header("创建敌人间隔")]
        public float spawnInterval; 
        [Header("当前敌人数量")]
        public int spawnCount;     
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


    [Header("当前波次")]
    public int currentWaveCount = 0;

    float spawnTimer = 0;
    [Header("当前存活敌人数")]
    public int enemiesAlive;
    [Header("最大存活敌人数")]
    public int maxEnemiesAllowed;
    [Header("是否到达最大存活敌人数")]
    public bool maxEnemiesReached = false;
    [Header("每一波开始后，下一波开始倒计时")]
    public float waveInterval;
    bool isWaveActive = false;

    [Header("刷怪点")]
    public List<Transform> relativesSpawnPoints;

    private EnemyPool enemyPool;

    void Awake()
    {
        currentWaveCount = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyPool = this.GetComponent<EnemyPool>();

        CalculateWaveQuota();


    }

    void OnEnable()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (currentWaveCount < waves.Count && waves[currentWaveCount].spawnCount == 0 && !isWaveActive) 
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
        isWaveActive = true;

        yield return new WaitForSeconds(waveInterval);

        //如果有更多的波数在此次之后，开始下一个波数
        if (currentWaveCount < waves.Count - 1)
        {
            isWaveActive = false;

            Debug.Log("下一波");
            currentWaveCount++;
            CalculateWaveQuota();
        }
    }

    void CalculateWaveQuota()
    {
        Debug.Log("刷怪");
        int currentWaveQuota = 0;
        foreach(var enemyGroup in waves[currentWaveCount].enemygroup)
        {
            currentWaveQuota += enemyGroup.enemyCount;
        }

        waves[currentWaveCount].waveQuota = currentWaveQuota;
    }

    void SpawnEnemies()
    {
        if (waves[currentWaveCount].spawnCount < waves[currentWaveCount].waveQuota && !maxEnemiesReached)
        {
            foreach(var enemyGroup in waves[currentWaveCount].enemygroup)
            {
                if (enemyGroup.spawnCount < enemyGroup.enemyCount)
                {
                    //创建敌人
                    enemyPool.Spawn(enemyGroup.enemyPrefabElementNum);

                    enemyGroup.spawnCount++;
                    waves[currentWaveCount].spawnCount++;
                    enemiesAlive++;

                    if (enemiesAlive >= maxEnemiesAllowed)
                    {
                        maxEnemiesReached = true;
                        return;
                    }
                }
            }
        }


    }

    public void OnEnemyKill()
    {
        enemiesAlive--;

        if (enemiesAlive < maxEnemiesAllowed)
        {
            maxEnemiesReached = false;
        }
    }



}
