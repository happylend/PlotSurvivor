using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Character Data/Basic Player Data", order = 0)]
[System.Serializable]
public class AttributeBase : ScriptableObject
{

    [Header("角色名")]
    [SerializeField]
    private string CharacterName;
    public string _CharacterName { get => CharacterName; private set => CharacterName = value; }

    [Header("最大生命值")]
    [SerializeField]
    private int MaxHealth;
    public int _MaxHealth { get => MaxHealth; private set => MaxHealth = value; }

    [Header("生命恢复速度")]
    [SerializeField]
    private float HealthRecovery;
    public float _HealthRecovery { get => HealthRecovery; set => HealthRecovery = value; }

    [Header("等级")]
    [SerializeField]
    private int Level;
    public int _Level { get => Level; private set => Level = value; }

    [Header("经验值")]
    [SerializeField]
    private int Experience;
    public int _Experience { get => Experience; private set => Experience = value; }

    [Header("移动速度")]
    [SerializeField]
    private float MoveSpeed;
    public float _MoveSpeed { get => MoveSpeed;set => MoveSpeed = value; }

    [Header("收集范围")]
    [SerializeField]
    float Magnet;
    public float _Magnet { get => Magnet; set => Magnet = value; }

}
