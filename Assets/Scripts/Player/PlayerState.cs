using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public AttributeBase playerData;

    [SerializeField]
    public float currentHealth;
    [SerializeField]
    public float currentRecovery;
    [SerializeField]
    public float currentMoveSpeed;

    Rigidbody rb;


    [Header("��ǰ����")]
    public int experience = 0;
    [Header("��ǰ�ȼ�")]
    public int level = 1;
    [Header("�������辭��ֵ")]
    public int experienceCap;


    [System.Serializable]
    public class LevelRange
    {
        public int startLevel;
        public int endLevel;
        [Header("���辭��ֵ����ֵ")]
        public int experienceCapIncrease;
    }

    public List<LevelRange> levelRanges;

    [Header("I-Frames")]
    public float invincibilityDuration;
    float invincibilityTimer;
    bool isInvincible;

    void Awake() 
    {
        if (playerData != null)
        {
            currentHealth = playerData._MaxHealth;
            currentRecovery = playerData._HealthRecovery;
            currentMoveSpeed = playerData._MoveSpeed;
        }

        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //��ʼ��
        experienceCap = levelRanges[0].experienceCapIncrease;
    }

    public void IncreaseExperience(int amount)
    {
        experience += amount;

        LevelUpChecker();
    }

    void LevelUpChecker()
    {
        if(experience >=experienceCap)
        {
            level++;
            experience -= experienceCap;
            Debug.Log("�����ˣ�");

            int experienceCapIncrease = 0;
            foreach(LevelRange range in levelRanges)
            {
                if (level >= range.startLevel && level <= range.endLevel)
                {
                    experienceCapIncrease = range.experienceCapIncrease;
                    break;
                }
            }
            experienceCap += experienceCapIncrease;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //�����С����ʱ����
        if (invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;
        }
        else if(isInvincible)
        {
            isInvincible = false;

        }
    }

    // �ƶ���Ϊ
    public void Move(Vector3 direction)
    {
        transform.position += direction.normalized * currentMoveSpeed * Time.deltaTime;
    }

    // ������Ϊ
    public void Attack(PlayerState target)
    {
        int damage = 0;
        target.TakeDamage(damage);
    }

    // ������Ϊ
    public void TakeDamage(float damage)
    {
        if (!isInvincible)
        {
            currentHealth -= damage;

            invincibilityTimer = invincibilityDuration;
            isInvincible = true;

            if (currentHealth <= 0)
            {
                Die();
            }
        }

    }

    // ������Ϊ
    public void Die()
    {
        // TODO: ��ɫ������Ĵ���
        Debug.Log("Player is dead");
    }

}
