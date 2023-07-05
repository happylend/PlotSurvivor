using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public AttributeBase playerData;

    protected float currentHealth;
    protected float currentRecovery;
    protected float currentMoveSpeed;

    Rigidbody rb;
    public Vector3 moveDir;
    public Vector3 PlayerDir;


    void Awake()
    {
        currentHealth = playerData._MaxHealth;
        currentRecovery = playerData._HealthRecovery;
        currentMoveSpeed = playerData._MoveSpeed;

        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 移动行为
    public void Move(Vector3 direction)
    {
        transform.position += direction.normalized * currentMoveSpeed * Time.deltaTime;
    }

    // 攻击行为
    public void Attack(PlayerState target)
    {
        int damage = 0;
        target.TakeDamage(damage);
    }

    // 受伤行为
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // 死亡行为
    public void Die()
    {
        // TODO: 角色死亡后的处理
        gameObject.SetActive(false);
    }

    /*
    // 升级行为
    public void LevelUp()
    {
        Level++;
        MaxHealth += 10;
        CurrentHealth = MaxHealth;
        //attackPower += 5;
        // TODO: 其他属性的升级
    }
    */
}
