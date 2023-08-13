using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPassiveItemData", menuName = "Data/Passive Item/Basic Passive Item", order = 1)]
public class PassiveItemData : ScriptableObject
{
    [SerializeField]
    float multipler;
    public float _multipler { get => multipler; private set => multipler = value; }

    [Header("���ߵȼ�")]
    [SerializeField]
    int level;
    public int _level { get => level; private set => level = value; }

    [Header("������������")]
    [SerializeField]
    GameObject nextLevelPrefab;
    public GameObject _nextLevelPrefab { get => nextLevelPrefab; private set => nextLevelPrefab = value; }

    [Header("����ͼ��")]
    [SerializeField]
    Sprite Icon;
    public Sprite _Icon { get => Icon; private set => Icon = value; }


    [Header("��������")]
    [SerializeField]
    new string name;
    public string _Name { get => name; private set => name = value; }

    [Header("��������")]
    [SerializeField]
    string Description;
    public string _Description { get => Description; private set => Description = value; }
}
