using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newWeaponData", menuName = "Data/Weapon Data/Basic Weapon Data", order = 1)]
[System.Serializable]
public class WeaponData : ScriptableObject
{
    [Header("伤害模组")]
    public BulletData BulletType;

    [Header("单次射击子弹数量")]
    [SerializeField]
    int OneShootBulletNum;
    public int _OneShootBulletNum { get => OneShootBulletNum; private set => OneShootBulletNum = value; }

    [Header("单次射击子弹创建间隔")]
    [SerializeField]
    float OneShootBulletCoolDown;
    public float _OneShootBulletCoolDown { get => OneShootBulletCoolDown; private set => OneShootBulletCoolDown = value; }

    [Header("单次射击子弹夹角（0-360）")]
    [SerializeField]
    float OneShootBulletRange;
    public float _OneShootBulletRange { get => OneShootBulletRange;private set => OneShootBulletRange = value; }

    [Header("攻击间隔")]
    [SerializeField]
    float CoolDown;
    public float _CoolDown { get => CoolDown; private set => CoolDown = value; }

    [Header("暴击率(0-100)")]
    [SerializeField]
    int CritRate;
    public int _CritRate { get => CritRate; private set => CritRate = value; }

    [Header("武器熟练度")]
    [SerializeField]
    public List<int> LevelUpKill;

    [Header("武器插槽")]
    [SerializeField]
    public List<GameObject> WeaponEX;
}
