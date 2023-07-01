using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public List<EnemyGroup> enemygroup;
        public int waveQuota;    //ÿ���������˵�����
        public float spawnInterval; //�������˵ļ��
        public int spawnCount;      //��ǰ�����г��������еĵ�������
    }

    [System.Serializable]
    public class EnemyGroup
    {
        public string enemyName;
        public int enemyCount;
        public int spawnCount;
        public GameObject enemyPrefab;

    }

    public List<Wave> waves;    //��ǰ��Ϸ�е����в���
    public int currentWaveCount;    //��ǰ����

    float spawnTimer = 0;

    public int enemiesAlive;
    public int maxEnemiesAllowed;
    public bool maxEnemiesReached = false;
    public float waveInterval;

    public List<Transform> relativesSpawnPoints;

    Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerControl>().transform;
        CalculateWaveQuota();
        SpawnEnemies();
    }
*
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
                    Instantiate(enemyGroup.enemyPrefab, player.position + relativesSpawnPoints[Random.Range(0, relativesSpawnPoints.Count)].position, Quaternion.identity);

                    //Vector3 spawnPosition = new Vector3(player.transform.position.x + Random.Range(-10f, 10f), 0, player.transform.position.z + Random.Range(-10f, 10f));
                    //Instantiate(enemyGroup.enemyPrefab, spawnPosition, Quaternion.identity);

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
    }
}
