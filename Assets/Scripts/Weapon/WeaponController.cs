using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WeaponData;

public class WeaponController : MonoBehaviour
{
    public WeaponData weaponData;
    //public GameObject weaponModel;


    GameObject currentbuttleObj;
    int currentBulletNum;
    float currentCoolDown;
    float currentOneShootBulletCoolDown;
    GameObject currentWeaponModel;

    [HideInInspector]
    public List<WeaponLevel> currentLevelRanges = new List<WeaponLevel>();

    [HideInInspector]
    public int experience = 0;
    [HideInInspector]
    public int WeaponLevel = 1;
    [HideInInspector]
    public int experienceCap;

    #region 当前武器属性
    public GameObject CurrentbuttleObj
    {
        get { return currentbuttleObj; }
        //检测当前生命值是否发生变化
        set
        {
            if (currentbuttleObj != value)
            {
                currentbuttleObj = value;
            }
        }
    }

    public int CurrentBulletNum
    {
        get { return currentBulletNum; }
        //检测当前生命值是否发生变化
        set
        {
            if (currentBulletNum != value)
            {
                currentBulletNum = value;
            }
        }
    }

    public float CurrentCoolDown
    {
        get { return currentCoolDown; }
        //检测当前生命值是否发生变化
        set
        {
            if (currentCoolDown != value)
            {
                currentCoolDown = value;
            }
        }
    }

    public float CurrentOneShootBulletCoolDown
    {
        get { return currentOneShootBulletCoolDown; }
        //检测当前生命值是否发生变化
        set
        {
            if (currentOneShootBulletCoolDown != value)
            {
                currentOneShootBulletCoolDown = value;
            }
        }
    }

    public GameObject CurrentWeaponModel
    {
        get { return currentWeaponModel; }
        //检测当前生命值是否发生变化
        set
        {
            if (currentWeaponModel != value)
            {
                currentWeaponModel = value;
            }
        }
    }

    #endregion


    protected virtual void Awake()
    {
        weaponData = CharacterSelect.GetWeapon();

        if (weaponData != null)
        {
            CurrentbuttleObj = weaponData.BulletType.Bullet;
            CurrentBulletNum = weaponData._OneShootBulletNum;
            CurrentCoolDown = weaponData._CoolDown;
            CurrentOneShootBulletCoolDown = weaponData._OneShootBulletCoolDown;
            CurrentWeaponModel = weaponData._weaponModel;

            
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
        Instantiate(CurrentWeaponModel, this.transform);
        //初始化
        experienceCap = currentLevelRanges[0].experienceCapIncrease;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        CurrentCoolDown -= Time.deltaTime;
        if(CurrentCoolDown <= 0f )
        {
            Attack();
        }
    }

    protected virtual void Attack()
    {
        CurrentCoolDown += weaponData._CoolDown;
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
            CurrentBulletNum = weaponData._OneShootBulletNum;
            CurrentCoolDown = weaponData._CoolDown;
            CurrentOneShootBulletCoolDown = weaponData._OneShootBulletCoolDown;
            CurrentWeaponModel = weaponData._weaponModel;


            if (weaponData.levelRanges.Count > 0)
            {
                foreach (WeaponLevel data in weaponData.levelRanges)
                {
                    currentLevelRanges.Add(data);
                }
            }

        }

        Instantiate(CurrentWeaponModel, this.transform);
        //初始化
        experienceCap = currentLevelRanges[0].experienceCapIncrease;

    }
}
