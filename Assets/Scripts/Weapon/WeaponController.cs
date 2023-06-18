using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public WeaponData weaponData;


    private int currentBulletNum;
    private float currentCoolDown;
    private float currentOneShootBulletCoolDown;

    protected PlayerControl playerControl;


    protected virtual void Awake()
    {
        playerControl=FindObjectOfType<PlayerControl>();

        currentBulletNum = weaponData._OneShootBulletNum;
        currentCoolDown = weaponData._CoolDown;
        currentOneShootBulletCoolDown = weaponData._OneShootBulletCoolDown;


    }
    // Start is called before the first frame update
    protected virtual void Start()
    {
        
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
}
