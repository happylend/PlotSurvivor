using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Character Data/Basic Player Data", order = 0)]
[System.Serializable]
public class AttributeBase : ScriptableObject
{

    [Header("��ɫ��")]
    [SerializeField]
    private string CharacterName;
    public string _CharacterName { get => CharacterName; private set => CharacterName = value; }

    [Header("�������ֵ")]
    [SerializeField]
    private int MaxHealth;
    public int _MaxHealth { get => MaxHealth; private set => MaxHealth = value; }

    [Header("�����ָ��ٶ�")]
    [SerializeField]
    private float HealthRecovery;
    public float _HealthRecovery { get => HealthRecovery; set => HealthRecovery = value; }

    [Header("�ȼ�")]
    [SerializeField]
    private int Level;
    public int _Level { get => Level; private set => Level = value; }

    [Header("����ֵ")]
    [SerializeField]
    private int Experience;
    public int _Experience { get => Experience; private set => Experience = value; }

    [Header("�ƶ��ٶ�")]
    [SerializeField]
    private float MoveSpeed;
    public float _MoveSpeed { get => MoveSpeed;set => MoveSpeed = value; }

}
