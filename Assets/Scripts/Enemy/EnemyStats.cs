using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemyAttributeData enemyData;
    private GameControl gameControl;
    private DropRateManager dropRateManager;
    private EnemyMovement enemyMovement;


    //当前状态
    [HideInInspector]
    public float currentMoveSpeed;
    [HideInInspector]
    public float currentHealth;
    [HideInInspector]
    public float currentDamage;

    [HideInInspector]
    Transform player;

    [HideInInspector]
    private float despawnDistance = 40f;


    Animator animator;

    public float invincibilityDuration;
    float invincibilityTimer;
    public bool isInvincible;

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

    //数据初始化
    void OnEnable()
    {
        OpenObj();
    }

    void Start()
    {
        player = FindObjectOfType<PlayerState>().transform;

        if (gameControl == null) { gameControl = FindObjectOfType<GameControl>(); }

        if (dropRateManager == null) { dropRateManager = this.GetComponent<DropRateManager>(); }
        if (enemyMovement == null) { enemyMovement = this.GetComponent<EnemyMovement>(); }

        if (animator == null) { animator = this.GetComponentInChildren<Animator>(); }



    }


    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) >= despawnDistance)
        {
            ReturnEnemy();
        }

        //造成伤害的最小时间间隔
        if (invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;
        }
        else if (isInvincible)
        {
            isInvincible = false;

        }
    }

    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;

        if (currentHealth <= 0)
        {
            Kill();
        }
        else
        {
            Hit();
        }
    }

    //受击触发
    public void Hit()
    {
        

    }

    //死亡触发
    public void Kill()
    {
        //监听动画
        StartCoroutine(PlayDeathAnimation());

        //全局击杀计数器调用
        gameControl.WhenEnemyDie();

        //掉落器调用
        dropRateManager.DropItem();

        //武器击杀调用

        //停止NPC状态
        CloseObj();
    }

    //资源池回收
    public void DestoryObj()
    {
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
        transform.position = player.position + es.relativesSpawnPoints[UnityEngine.Random.Range(0, es.relativesSpawnPoints.Count)].position;
    }

    //关闭对象事件
    void CloseObj()
    {
        if (enemyMovement != null)
        {
            enemyMovement.enabled = false;
        }

        this.GetComponent<BoxCollider>().enabled = false;
    }


    //开启对象事件
    void OpenObj()
    {
        if (enemyData != null)
        {
            currentDamage = enemyData._HitDamage;
            currentHealth = enemyData._MaxHealth;
            currentMoveSpeed = enemyData._MoveSpeed;
        }

        if (enemyMovement != null)
        {
            enemyMovement.enabled = true;
        }

        this.GetComponent<BoxCollider>().enabled = true;
    }

    // 协程：播放死亡动画并销毁对象
    private IEnumerator PlayDeathAnimation()
    {
        // 播放死亡动画
        animator.SetBool("Die", true);

        // 等待动画播放完成
        float deathAnimationLength = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(deathAnimationLength);

        // 销毁对象
        DestoryObj();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!isInvincible)
            {
                invincibilityTimer = invincibilityDuration;
                isInvincible = true;

                Debug.Log(collision.transform.name + " Hit");
                PlayerState player = collision.gameObject.GetComponent<PlayerState>();
                player.TakeDamage(currentDamage);
            }
        }
    }
}
