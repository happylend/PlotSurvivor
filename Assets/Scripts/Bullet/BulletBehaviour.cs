using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public BulletData bulletData;

    protected Vector3 direction;
    
    
    protected float destroyAfterSeconds;
    protected float currentDamage;
    protected float currentSpeed;
    protected float currentcooldownDuration;
    protected float currentPierce;

    void Awake()
    {
        if (bulletData != null)
        {
            destroyAfterSeconds = bulletData._BulletRange;
            currentDamage = bulletData._HitDamage;
            currentSpeed = bulletData._BulletSpeed;
            currentPierce = bulletData._NumberOfHits;
        }
        else
        {
            Debug.LogError("NoBulletData!");
        }
    }


    // Start is called before the first frame update
    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DirectionChecker(Vector3 dir)
    {
        direction = dir;

        float dirx = direction.x;
        float diry = direction.y;
        float dirz = direction.z;

        transform.forward = dir;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyStats enemy = other.GetComponent<EnemyStats>();
            enemy.TakeDamage(currentDamage);
            ReducePierce();
        }
    }

    void ReducePierce()
    {
        currentPierce--;
        if(currentPierce <= 0)
        {
            Destroy(gameObject);
        }
    }
}
