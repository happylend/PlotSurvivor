using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveItem : MonoBehaviour
{
    protected PlayerState player;
    protected BulletBehaviour bullet;
    protected WeaponController weapon;
    public PassiveItemData passiveItemData;

    protected virtual void ApplyModifler()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerState>();
        bullet = FindObjectOfType<BulletBehaviour>();
        weapon = FindObjectOfType<WeaponController>();

        ApplyModifler();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
