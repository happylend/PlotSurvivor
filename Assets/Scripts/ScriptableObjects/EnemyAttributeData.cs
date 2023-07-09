using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyData", menuName = "Data/Character Data/Basic Enemy Data", order = 0)]
[System.Serializable]
public class EnemyAttributeData : ScriptableObject
{
    [Header("最大生命值")]
    [SerializeField]
    int MaxHealth;
    public int _MaxHealth { get => MaxHealth; private set => MaxHealth = value; } 

    [Header("移动速度")]
    [SerializeField]
    float MoveSpeed;
    public float _MoveSpeed { get => MoveSpeed; private set => MoveSpeed = value; }

    [Header("碰撞伤害")]
    [SerializeField]
    int HitDamage;
    public int _HitDamage { get => HitDamage; private set => HitDamage = value; }

    [Header("远程伤害")]
    [SerializeField]
    int ShootDamage;
    public int _ShootDamage { get => ShootDamage; private set => ShootDamage = value; }
}
