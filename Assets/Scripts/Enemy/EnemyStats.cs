using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemyAttributeData enemyData;

    //当前状态
    private float currentMoveSpeed;
    private float currentHealth;
    private float currentDamage;

    public float despawnDistance = 20f;
    Transform player;


    System.Action<EnemyStats> deactivateAction;

    private void Awake()
    {
        currentDamage = enemyData._Damage;
        currentHealth = enemyData._MaxHealth;
        currentMoveSpeed = enemyData._MoveSpeed;
    }
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerControl>().transform;
    }

    //每次激活时刷新属性
    void OnEnable()
    {
        
    }

    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;

        if (currentHealth <= 0)
        {
            Kill();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) >= despawnDistance)
        {
            ReturnEnemy();
        }
    }

    public void Kill()
    {
        //触发销毁
        Destroy(gameObject);
        //deactivateAction.Invoke(this);
    }

    //设置停用状态
    public void SetDeactivateAction(System.Action<EnemyStats> deactivateAction)
    {
        this.deactivateAction = deactivateAction;
    }

    private void OnDisable()
    {
        EnemySpawner es = FindObjectOfType<EnemySpawner>();
        es.OnEnemyKill();
    }

    //刷新敌人位置
    void ReturnEnemy()
    {
        EnemySpawner es = FindObjectOfType<EnemySpawner>();
        transform.position = player.position + es.relativesSpawnPoints[Random.Range(0, es.relativesSpawnPoints.Count)].position;
    }
}
