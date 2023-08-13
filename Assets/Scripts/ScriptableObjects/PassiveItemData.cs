using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPassiveItemData", menuName = "Data/Passive Item/Basic Passive Item", order = 1)]
public class PassiveItemData : ScriptableObject
{
    [SerializeField]
    float multipler;
    public float _multipler { get => multipler; private set => multipler = value; }

    [Header("道具等级")]
    [SerializeField]
    int level;
    public int _level { get => level; private set => level = value; }

    [Header("道具升级对象")]
    [SerializeField]
    GameObject nextLevelPrefab;
    public GameObject _nextLevelPrefab { get => nextLevelPrefab; private set => nextLevelPrefab = value; }

    [Header("道具图标")]
    [SerializeField]
    Sprite Icon;
    public Sprite _Icon { get => Icon; private set => Icon = value; }


    [Header("道具名称")]
    [SerializeField]
    new string name;
    public string _Name { get => name; private set => name = value; }

    [Header("道具描述")]
    [SerializeField]
    string Description;
    public string _Description { get => Description; private set => Description = value; }
}
