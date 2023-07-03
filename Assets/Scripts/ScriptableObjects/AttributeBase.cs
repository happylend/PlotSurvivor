using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Character Data/Basic Player Data", order = 0)]
public class AttributeBase : ScriptableObject
{
    [SerializeField]
    [Header("角色名")]
    private string CharacterName;
    public string _CharacterName { get => CharacterName; private set => CharacterName = value; }

    [SerializeField]
    [Header("最大生命值")]
    private int MaxHealth;
    public int _MaxHealth { get => MaxHealth; private set => MaxHealth = value; }

    [SerializeField]
    [Header("生命恢复速度")]
    private float HealthRecovery;
    public float _HealthRecovery { get => HealthRecovery; set => HealthRecovery = value; }

    [SerializeField]
    [Header("等级")]
    private int Level;
    public int _Level { get => Level; private set => Level = value; }

    [SerializeField]
    [Header("经验值")]
    private int Experience;
    public int _Experience { get => Experience; private set => Experience = value; }

    [SerializeField]
    [Header("移动速度")]
    private float MoveSpeed;
    public float _MoveSpeed { get => MoveSpeed;set => MoveSpeed = value; }

}
