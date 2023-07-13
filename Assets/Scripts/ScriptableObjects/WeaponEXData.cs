using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newWeaponEXData", menuName = "Data/EX Data/Basic WeaponEX Data", order = 1)]
[System.Serializable]
public class WeaponEXData : ScriptableObject
{
    [Header("技能稀有度")]
    [SerializeField]
    int EXLevel;
    public int _EXLevel { get => EXLevel; private set => EXLevel = value; }


}
