using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
    AttributeBase playerData;

    float currentHealth;
    float currentRecovery;
    float currentMoveSpeed;
    float currentMagnet;

    #region 当前角色属性
    public float CurrentHealth
    {
        get { return currentHealth; }
        //检测当前生命值是否发生变化
        set 
        { 
            if (currentHealth != value) 
            { 
                currentHealth = value; 

            } 
        }
    }

    public float CurrentRecovery
    {
        get { return currentRecovery; }
        //检测当前生命值是否发生变化
        set
        {
            if (currentRecovery != value)
            {
                currentRecovery = value;
            }
        }
    }

    public float CurrentMoveSpeed
    {
        get { return currentMoveSpeed; }
        //检测当前生命值是否发生变化
        set
        {
            if (currentMoveSpeed != value)
            {
                currentMoveSpeed = value;
            }
        }
    }

    public float CurrentMagnet
    {
        get { return currentMagnet; }
        //检测当前生命值是否发生变化
        set
        {
            if (currentMagnet != value)
            {
                currentMagnet = value;
            }
        }
    }
    #endregion


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

    InventoryManager inventory;
    public int weaponIndex;
    public int passiveItemIndex;

    [Header("UI")]
    public Image healthBar;


    void Awake() 
    {
        playerData = CharacterSelect.GetData();

        if (playerData != null)
        {
            CurrentHealth = playerData._MaxHealth;
            CurrentRecovery = playerData._HealthRecovery;
            CurrentMoveSpeed = playerData._MoveSpeed;
            CurrentMagnet = playerData._Magnet;

            inventory = FindObjectOfType<InventoryManager>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //初始化
        experienceCap = levelRanges[0].experienceCapIncrease;
        CharacterSelect.instance.DestroySingleton();

        UpdateHealthBar();
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

            GameManager.instance.StartLevelUp();
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
       
            CurrentHealth -= damage;

            if (CurrentHealth <= 0)
            {
                Die();
            }

        UpdateHealthBar();

    }

    void UpdateHealthBar()
    {
        //更新生命值
        healthBar.fillAmount = currentHealth / playerData._MaxHealth;
    }

    // 死亡行为
    public void Die()
    {
        // TODO: 角色死亡后的处理
        Debug.Log("Player is dead");

        if (!GameManager.instance.isGameOver)
        {
            //GameManager.instance.AssignLevelReachedUI(level);
            //GameManager.instance.AssignChosenWeaponsAndPassiveItemsUI(inventory.weaponUISlots,inventory.passiveItemUISlots);
            GameManager.instance.GameOver();
        }
    }

    //自然恢复生命值
    void Recover()
    {
        if (CurrentHealth < playerData._MaxHealth)
        {
            CurrentHealth += currentRecovery * Time.deltaTime;
        }

        if(CurrentHealth > playerData._MaxHealth)
        {
            CurrentHealth = playerData._MaxHealth;
        }
    }

    public void SpawnPassiveItem(GameObject passiveItem)
    {
        if (passiveItemIndex >= inventory.passiveSlots.Count - 1)
        {
            Debug.LogError("full!");
                return;
        }

        GameObject spawnedPassiveItem = Instantiate(passiveItem, transform.position, Quaternion.identity);
        spawnedPassiveItem.transform.SetParent(transform);
        inventory.AddPassiveItem(passiveItemIndex, spawnedPassiveItem.GetComponent<PassiveItem>());

        passiveItemIndex++;
    }
}
