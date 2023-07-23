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



    [Header("当前经验")]
    public int experience = 0;
    [Header("当前等级")]
    public int level = 1;
    [Header("升级所需经验值")]
    public int experienceCap;


    [System.Serializable]
    public class LevelRange
    {
        public int startLevel;
        public int endLevel;
        [Header("所需经验值增长值")]
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
        //初始化
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
            Debug.Log("升级了！");

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

    // 攻击行为
    public void Attack(PlayerState target)
    {
        int damage = 0;
        target.TakeDamage(damage);
    }

    // 受伤行为
    public void TakeDamage(float damage)
    {
       
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                Die();
            }

    }

    // 死亡行为
    public void Die()
    {
        // TODO: 角色死亡后的处理
        Debug.Log("Player is dead");
    }

    //自然恢复生命值
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
