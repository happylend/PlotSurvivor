using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newWeaponData", menuName = "Data/Weapon Data/Basic Bullet Data", order = 0)]
[System.Serializable]
public class BulletData : ScriptableObject
{
    [Header("子弹模组")]
    public GameObject Bullet;

    [Header("子弹伤害")]
    [SerializeField]
    float HitDamage;
    public float _HitDamage { get => HitDamage; set => HitDamage = value; }

    [Header("穿透次数")]
    [SerializeField]
    int NumberOfHits;
    public int _NumberOfHits { get => NumberOfHits; set => NumberOfHits = value; }

    [Header("暴击倍数")]
    [SerializeField]
    float CritDamage;
    public float _CritDamage { get => CritDamage; set => CritDamage = value; }

    [Header("子弹大小倍数")]
    [SerializeField]
    float BulletSize;
    public float _BulletSize { get => BulletSize; set => BulletSize = value; }

    [Header("子弹速度")]
    [SerializeField]
    float BulletSpeed;
    public float _BulletSpeed { get => BulletSpeed; set => BulletSpeed = value; }

    [Header("射程")]
    [SerializeField]
    float BulletRange;
    public float _BulletRange { get => BulletRange; set => BulletRange = value; }

    [Header("无视防御伤害")]
    [SerializeField]
    int BreakArmor;
    public int _BreakArmor { get => BreakArmor; set => BreakArmor = value; }
}
