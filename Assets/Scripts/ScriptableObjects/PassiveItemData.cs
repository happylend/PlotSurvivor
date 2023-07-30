using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPassiveItemData", menuName = "Data/Passive Item/Basic Passive Item", order = 1)]
public class PassiveItemData : ScriptableObject
{
    [SerializeField]
    float multipler;
    public float _multipler { get => multipler; private set => multipler = value; }
}
