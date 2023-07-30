using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    AttributeBase playerData;

    float currentHealth;
    float currentRecovery;
    float currentMoveSpeed;
    float currentMagnet;

    #region ��ǰ��ɫ����
    public float CurrentHealth
    {
        get { return currentHealth; }
        //��⵱ǰ����ֵ�Ƿ����仯
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
        //��⵱ǰ����ֵ�Ƿ����仯
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
        //��⵱ǰ����ֵ�Ƿ����仯
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
        //��⵱ǰ����ֵ�Ƿ����仯
        set
        {
            if (currentMagnet != value)
            {
                currentMagnet = value;
            }
        }
    }
    #endregion


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

    InventoryManager inventory;
    public int weaponIndex;
    public int passiveItemIndex;


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
        //��ʼ��
        experienceCap = levelRanges[0].experienceCapIncrease;
        CharacterSelect.instance.DestroySingleton();
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
       
            CurrentHealth -= damage;

            if (CurrentHealth <= 0)
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
