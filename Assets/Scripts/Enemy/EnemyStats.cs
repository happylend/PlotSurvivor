using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemyAttributeData enemyData;

    //��ǰ״̬
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
        currentDamage = enemyData._HitDamage;
        currentHealth = enemyData._MaxHealth;
        currentMoveSpeed = enemyData._MoveSpeed;
    }
    // Start is called before the first frame update

    void Start()
    {
        player = FindObjectOfType<PlayerState>().transform;
        if (animator == null) { animator = this.GetComponentInChildren<Animator>(); }
    }

    //ÿ�μ���ʱˢ������
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
        //��������
        //Destroy(gameObject);
        EnemySpawner es = FindObjectOfType<EnemySpawner>();
        if (es != null)
        {
            es.OnEnemyKill();
        }

        animator.SetBool("Die", true);
        deactivateAction.Invoke(this);
    }

    //����ͣ��״̬
    public void SetDeactivateAction(System.Action<EnemyStats> deactivateAction)
    {
        this.deactivateAction = deactivateAction;
    }

    //ˢ�µ���λ��
    void ReturnEnemy()
    {
        EnemySpawner es = FindObjectOfType<EnemySpawner>();
        transform.position = player.position + es.relativesSpawnPoints[Random.Range(0, es.relativesSpawnPoints.Count)].position;
    }
}
