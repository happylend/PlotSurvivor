using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WeaponData;

public class WeaponController : MonoBehaviour
{
    public WeaponData weaponData;
    //public GameObject weaponModel;


    private int currentBulletNum;
    private float currentCoolDown;
    private float currentOneShootBulletCoolDown;
    private GameObject currentWeaponModel;
    private List<WeaponLevel> currentLevelRanges = new List<WeaponLevel>();

    protected PlayerState playerState;

    public int experience = 0;
    public int WeaponLevel = 1;
    public int experienceCap;


    protected virtual void Awake()
    {
        playerState = FindObjectOfType<PlayerState>();

        if (weaponData != null)
        {
            currentBulletNum = weaponData._OneShootBulletNum;
            currentCoolDown = weaponData._CoolDown;
            currentOneShootBulletCoolDown = weaponData._OneShootBulletCoolDown;
            currentWeaponModel = weaponData._weaponModel;

            
            if (weaponData.levelRanges.Count > 0)
            {
                foreach(WeaponLevel data in  weaponData.levelRanges)
                {
                    currentLevelRanges.Add(data);
                }
            }
            
        }
    }
    // Start is called before the first frame update
    protected virtual void Start()
    {
        Instantiate(currentWeaponModel, this.transform);
        //初始化
        experienceCap = currentLevelRanges[0].experienceCapIncrease;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        currentCoolDown -= Time.deltaTime;
        if(currentCoolDown <= 0f )
        {
            Attack();
        }
    }

    protected virtual void Attack()
    {
        currentCoolDown = weaponData._CoolDown;
    }

    public void IncreaseExperience(int amount)
    {
        experience += amount;

        LevelUpChecker();
    }

    
    void LevelUpChecker()
    {
        if (experience >= experienceCap)
        {
            WeaponLevel++;
            experience -= experienceCap;

            int experienceCapIncrease = 0;
            foreach (WeaponLevel range in currentLevelRanges)
            {
                if (WeaponLevel >= range.startLevel && WeaponLevel <= range.endLevel)
                {
                    experienceCapIncrease = range.experienceCapIncrease;
                    break;
                }
            }
            experienceCap += experienceCapIncrease;
        }
    }
    
    //武器升级
    public void ChangeWeapon()
    {
        if (weaponData != null)
        {
            currentBulletNum = weaponData._OneShootBulletNum;
            currentCoolDown = weaponData._CoolDown;
            currentOneShootBulletCoolDown = weaponData._OneShootBulletCoolDown;
            currentWeaponModel = weaponData._weaponModel;


            if (weaponData.levelRanges.Count > 0)
            {
                foreach (WeaponLevel data in weaponData.levelRanges)
                {
                    currentLevelRanges.Add(data);
                }
            }

        }

        Instantiate(currentWeaponModel, this.transform);
        //初始化
        experienceCap = currentLevelRanges[0].experienceCapIncrease;

    }
}
