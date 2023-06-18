using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyData", menuName = "Data/Character Data/Basic Enemy Data", order = 0)]
public class EnemyAttributeData : ScriptableObject
{
    [Header("�������ֵ")]
    [SerializeField]
    int MaxHealth;
    public int _MaxHealth { get => MaxHealth; private set => MaxHealth = value; } 

    [Header("�ƶ��ٶ�")]
    [SerializeField]
    float MoveSpeed;
    public float _MoveSpeed { get => MoveSpeed; private set => MoveSpeed = value; }

    [Header("��ײ�˺�")]
    [SerializeField]
    int Damage;
    public int _Damage { get => Damage; private set => Damage = value; }
}
