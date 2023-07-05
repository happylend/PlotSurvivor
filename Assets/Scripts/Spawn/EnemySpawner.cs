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
        public int waveQuota;    //ÿ���������˵�����
        [Header("�������˼��")]
        public float spawnInterval; //�������˵ļ��
        [Header("�Ѵ�����������")]
        public int spawnCount;      //��ǰ�����г��������еĵ�������
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
    public int currentWaveCount;    //��ǰ����

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
        Player = FindObjectOfType<PlayerState>().transform;
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

        //����и���Ĳ����ڴ˴�֮�󣬿�ʼ��һ������
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

                    //��������
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
