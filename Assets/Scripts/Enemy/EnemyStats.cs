using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemyAttributeData enemyData;

    //当前状态
    protected float currentMoveSpeed;
    protected float currentHealth;
    protected float currentDamage;

    [SerializeField]
    Transform player;

    [SerializeField]
    private float despawnDistance = 40f;


    Animator animator;


    System.Action<EnemyStats> deactivateAction;

    private void Awake()
    {
        if (enemyData != null)
        {
            currentDamage = enemyData._HitDamage;
            currentHealth = enemyData._MaxHealth;
            currentMoveSpeed = enemyData._MoveSpeed;
        }

    }
    // Start is called before the first frame update

    void Start()
    {
        player = FindObjectOfType<PlayerState>().transform;
        if (animator == null) { animator = this.GetComponentInChildren<Animator>(); }
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
        //全局击杀计数器调用
        EnemySpawner es = FindObjectOfType<EnemySpawner>();
        if (es != null)
        {
            es.OnEnemyKill();
        }

        //武器击杀调用
        


        animator.SetBool("Die", true);
        deactivateAction.Invoke(this);
    }

    //设置停用状态
    public void SetDeactivateAction(System.Action<EnemyStats> deactivateAction)
    {
        this.deactivateAction = deactivateAction;
    }

    //刷新敌人位置
    void ReturnEnemy()
    {
        EnemySpawner es = FindObjectOfType<EnemySpawner>();
        transform.position = player.position + es.relativesSpawnPoints[Random.Range(0, es.relativesSpawnPoints.Count)].position;
    }
}
