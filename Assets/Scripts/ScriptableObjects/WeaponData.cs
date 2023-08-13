using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newWeaponData", menuName = "Data/Weapon Data/Basic Weapon Data", order = 1)]
[System.Serializable]
public class WeaponData : ScriptableObject
{
    [Header("�˺�ģ��")]
    public BulletData BulletType;

    [Header("")]
    [SerializeField]
    GameObject weaponModel;
    public GameObject _weaponModel { get => weaponModel; private set => weaponModel = value; }

    [Header("��������ӵ�����")]
    [SerializeField]
    int OneShootBulletNum;
    public int _OneShootBulletNum { get => OneShootBulletNum; private set => OneShootBulletNum = value; }

    [Header("��������ӵ��������")]
    [SerializeField]
    float OneShootBulletCoolDown;
    public float _OneShootBulletCoolDown { get => OneShootBulletCoolDown; private set => OneShootBulletCoolDown = value; }

    [Header("��������ӵ��нǣ�0-360��")]
    [SerializeField]
    float OneShootBulletRange;
    public float _OneShootBulletRange { get => OneShootBulletRange;private set => OneShootBulletRange = value; }

    [Header("�������")]
    [SerializeField]
    float CoolDown;
    public float _CoolDown { get => CoolDown; private set => CoolDown = value; }

    [Header("������(0-100)")]
    [SerializeField]
    int CritRate;
    public int _CritRate { get => CritRate; private set => CritRate = value; }

    [Header("����������")]
    [SerializeField]
    public List<WeaponLevel> levelRanges;

    [Header("�������")]
    [SerializeField]
    public List<WeaponEXData> WeaponEX;

    [Header("�����ȼ�")]
    [SerializeField]
    int level;
    public int _level { get => level; private set => level = value; }

    [Header("������������")]
    [SerializeField]
    GameObject nextLevelPrefab;
    public GameObject _nextLevelPrefab { get => nextLevelPrefab; private set => nextLevelPrefab = value; }

    [Header("��������")]
    [SerializeField]
    new string name;
    public string _Name { get => name; private set => name = value; }

    [Header("��������")]
    [SerializeField]
    string Description;
    public string _Description { get => Description; private set => Description = value; }

    [Header("����ͼ��")]
    [SerializeField]
    Sprite Icon;
    public Sprite _Icon { get => Icon; private set => Icon = value; }

    [System.Serializable]
    public class WeaponLevel
    {
        public int startLevel;
        public int endLevel;
        public int experienceCapIncrease;
    }
}
