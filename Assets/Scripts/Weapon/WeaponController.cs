using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public WeaponData weaponData;
    public GameObject weaponModel;


    private int currentBulletNum;
    private float currentCoolDown;
    private float currentOneShootBulletCoolDown;
    private List<int> currentLevelUpKill = new List<int>();

    protected PlayerState playerState;

    private int WeaponLevel = 0;

    protected virtual void Awake()
    {
        playerState = FindObjectOfType<PlayerState>();

        if (weaponData != null)
        {
            currentBulletNum = weaponData._OneShootBulletNum;
            currentCoolDown = weaponData._CoolDown;
            currentOneShootBulletCoolDown = weaponData._OneShootBulletCoolDown;

            if (weaponData.LevelUpKill.Count > 0)
            {
                for (int i = 0; i < weaponData.LevelUpKill.Count; i++) 
                {
                    currentLevelUpKill.Add(weaponData.LevelUpKill[i]);
                }
            }
        }



    }
    // Start is called before the first frame update
    protected virtual void Start()
    {
        Instantiate(weaponModel, this.transform);
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

    //��������
    void LevelUp()
    {
        currentLevelUpKill[WeaponLevel]--;
        if (currentLevelUpKill[WeaponLevel] <= 0)
        {
            WeaponLevel++;
        }
    }
}
