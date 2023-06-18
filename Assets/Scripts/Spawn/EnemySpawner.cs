using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public class Wave
    {
        public string waveName;
        public List<EnemyGroup> enemygroup;
        public string waveQuota;    //ÿ���������˵�����
        public float spawnInterval; //�������˵ļ��
        public int spawnCount;      //��ǰ�����г��������еĵ�������
    }

    public class EnemyGroup
    {
        public List<string> enemyName;
        public List<int> enemyCount;
        public int spawnCount;
        public GameObject enemyPrefab;

    }

    public List<Wave> waves;    //��ǰ��Ϸ�е����в���

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
