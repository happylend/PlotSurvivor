using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeBase : MonoBehaviour
{
    [SerializeField]
    private string CharacterName;
    public string _CharacterName { get => CharacterName; private set => CharacterName = value; }

    [SerializeField]
    private int MaxHealth;
    public int _MaxHealth { get => MaxHealth; private set => MaxHealth = value; }

    [SerializeField]
    private int CurrentHealth;
    public int _CurrentHealth { get => CurrentHealth; private set => CurrentHealth = value; }

    [SerializeField]
    private int Level;
    public int _Level { get => Level; private set => Level = value; }

    [SerializeField]
    private int Experience;
    public int _Experience { get => Experience; private set => Experience = value; }

    [SerializeField]
    private float MoveSpeed;
    public float _MoveSpeed { get => MoveSpeed;set => MoveSpeed = value; }


    public static AttributeBase Instance { get; set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }


    // �ƶ���Ϊ
    public void Move(Vector3 direction)
    {
        transform.position += direction.normalized * _MoveSpeed * Time.deltaTime;
    }

    // ������Ϊ
    public void Attack(AttributeBase target)
    {
        int damage = 0;
        target.TakeDamage(damage);
    }

    // ������Ϊ
    public void TakeDamage(int damage)
    {
        _CurrentHealth -= damage;
        if (_CurrentHealth <= 0)
        {
            Die();
        }
    }

    // ������Ϊ
    public void Die()
    {
        // TODO: ��ɫ������Ĵ���
        gameObject.SetActive(false);
    }

    // ������Ϊ
    public void LevelUp()
    {
        Level++;
        MaxHealth += 10;
        CurrentHealth = MaxHealth;
        //attackPower += 5;
        // TODO: �������Ե�����
    }
}
