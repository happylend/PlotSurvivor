using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    //���β���
    [System.Serializable]
    public class Wave
    {
        [Header("������")]
        public string waveName;
        [Header("�ò��εĵ�������")]
        public List<EnemyGroup> enemygroup;
        [Header("��ǰ���εĵ��˵�����")]
        public int waveQuota;   
        [Header("�������˼��")]
        public float spawnInterval; 
        [Header("��ǰ��������")]
        public int spawnCount;     
    }

    //ÿ���е��˲���
    [System.Serializable]
    public class EnemyGroup
    {
        [Header("������")]
        public string enemyName;
        [Header("��������")]
        public int enemyCount;
        [Header("�Ѵ����ĵ�������")]
        public int spawnCount;
        [Header("�������ͣ�����EnemyPool���ã�")]
        public int enemyPrefabElementNum;

    }

    public List<Wave> waves;    //��ǰ��Ϸ�е����в���


    [Header("��ǰ����")]
    public int currentWaveCount = 0;

    float spawnTimer = 0;
    [Header("��ǰ��������")]
    public int enemiesAlive;
    [Header("����������")]
    public int maxEnemiesAllowed;
    [Header("�Ƿ񵽴�����������")]
    public bool maxEnemiesReached = false;
    [Header("ÿһ����ʼ����һ����ʼ����ʱ")]
    public float waveInterval;
    bool isWaveActive = false;

    [Header("ˢ�ֵ�")]
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

        //����и���Ĳ����ڴ˴�֮�󣬿�ʼ��һ������
        if (currentWaveCount < waves.Count - 1)
        {
            isWaveActive = false;

            Debug.Log("��һ��");
            currentWaveCount++;
            CalculateWaveQuota();
        }
    }

    void CalculateWaveQuota()
    {
        Debug.Log("ˢ��");
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
                    //��������
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
