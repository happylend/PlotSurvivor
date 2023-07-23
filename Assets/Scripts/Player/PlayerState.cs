using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public AttributeBase playerData;

    [HideInInspector]
    public float currentHealth;
    [HideInInspector]
    public float currentRecovery;
    [HideInInspector]
    public float currentMoveSpeed;
    [HideInInspector]
    public float currentMagnet;



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

    void Awake() 
    {
        if (playerData != null)
        {
            currentHealth = playerData._MaxHealth;
            currentRecovery = playerData._HealthRecovery;
            currentMoveSpeed = playerData._MoveSpeed;
            currentMagnet = playerData._Magnet;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //��ʼ��
        experienceCap = levelRanges[0].experienceCapIncrease;
    }

    // Update is called once per frame
    void Update()
    {
        Recover();
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

    // ������Ϊ
    public void Attack(PlayerState target)
    {
        int damage = 0;
        target.TakeDamage(damage);
    }

    // ������Ϊ
    public void TakeDamage(float damage)
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
        Debug.Log("Player is dead");
    }

    //��Ȼ�ָ�����ֵ
    void Recover()
    {
        if (currentHealth < playerData._MaxHealth)
        {
            currentHealth += currentRecovery * Time.deltaTime;
        }

        if(currentHealth > playerData._MaxHealth)
        {
            currentHealth = playerData._MaxHealth;
        }
    }
}
