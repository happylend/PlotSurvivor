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
        if (enemyData != null)
        {
            currentDamage = enemyData._HitDamage;
            currentHealth = enemyData._MaxHealth;
            currentMoveSpeed = enemyData._MoveSpeed;
        }

    }

    //���ݳ�ʼ��
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

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) >= despawnDistance)
        {
            ReturnEnemy();
        }
    }

    //�ܻ�����
    public void Hit()
    {
        

    }

    //��������
    public void Kill()
    {
        //��������
        StartCoroutine(PlayDeathAnimation());

        //ȫ�ֻ�ɱ����������
        gameControl.WhenEnemyDie();

        //����������
        dropRateManager.DropItem();

        //������ɱ����

        //ֹͣNPC״̬
        CloseObj();
    }

    //��Դ�ػ���
    public void DestoryObj()
    {
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
        transform.position = player.position + es.relativesSpawnPoints[UnityEngine.Random.Range(0, es.relativesSpawnPoints.Count)].position;
    }

    //�رն����¼�
    void CloseObj()
    {
        if (enemyMovement != null)
        {
            enemyMovement.enabled = false;
        }

        this.GetComponent<BoxCollider>().enabled = false;
    }


    //���������¼�
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

    // Э�̣������������������ٶ���
    private IEnumerator PlayDeathAnimation()
    {
        // ������������
        animator.SetBool("Die", true);

        // �ȴ������������
        float deathAnimationLength = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(deathAnimationLength);

        // ���ٶ���
        DestoryObj();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerState player = collision.gameObject.GetComponent<PlayerState>();
            player.TakeDamage(currentDamage);
        }

    }
}
