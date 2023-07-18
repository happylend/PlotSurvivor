using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public AttributeBase playerData;

    protected float currentHealth;
    protected float currentRecovery;
    protected float currentMoveSpeed;

    Rigidbody rb;
    public Vector3 moveDir;
    public Vector3 PlayerDir;

    [Header("Experience/Level")]
    public int experience = 0;
    public int level = 1;
    public int experienceCap;


    [System.Serializable]
    public class LevelRange
    {
        public int startLevel;
        public int endLevel;
        public int experienceCapIncrease;
    }

    public List<LevelRange> levelRanges;

    

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
        PlayerDir = transform.forward;
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
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // ������Ϊ
    public void Die()
    {
        // TODO: ��ɫ������Ĵ���
        gameObject.SetActive(false);
    }

    /*
    // ������Ϊ
    public void LevelUp()
    {
        Level++;
        MaxHealth += 10;
        CurrentHealth = MaxHealth;
        //attackPower += 5;
        // TODO: �������Ե�����
    }
    */
}
