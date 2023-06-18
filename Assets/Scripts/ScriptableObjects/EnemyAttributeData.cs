using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyData", menuName = "Data/Character Data/Basic Enemy Data", order = 0)]
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
    int Damage;
    public int _Damage { get => Damage; private set => Damage = value; }
}
