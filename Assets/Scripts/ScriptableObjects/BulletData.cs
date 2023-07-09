using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newWeaponData", menuName = "Data/Weapon Data/Basic Bullet Data", order = 0)]
[System.Serializable]
public class BulletData : ScriptableObject
{
    [Header("�ӵ�ģ��")]
    public GameObject Bullet;

    [Header("�ӵ��˺�")]
    [SerializeField]
    float HitDamage;
    public float _HitDamage { get => HitDamage; set => HitDamage = value; }

    [Header("��͸����")]
    [SerializeField]
    int NumberOfHits;
    public int _NumberOfHits { get => NumberOfHits; set => NumberOfHits = value; }

    [Header("��������")]
    [SerializeField]
    float CritDamage;
    public float _CritDamage { get => CritDamage; set => CritDamage = value; }

    [Header("�ӵ���С����")]
    [SerializeField]
    float BulletSize;
    public float _BulletSize { get => BulletSize; set => BulletSize = value; }

    [Header("�ӵ��ٶ�")]
    [SerializeField]
    float BulletSpeed;
    public float _BulletSpeed { get => BulletSpeed; set => BulletSpeed = value; }

    [Header("���")]
    [SerializeField]
    float BulletRange;
    public float _BulletRange { get => BulletRange; set => BulletRange = value; }

    [Header("���ӷ����˺�")]
    [SerializeField]
    int BreakArmor;
    public int _BreakArmor { get => BreakArmor; set => BreakArmor = value; }
}
