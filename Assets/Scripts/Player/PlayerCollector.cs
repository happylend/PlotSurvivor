using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    PlayerState playerState;
    SphereCollider playerCollector;
    public float pullSpeed = 20;

    void Start()
    {
        playerState = FindObjectOfType<PlayerState>();
        playerCollector = GetComponent<SphereCollider>();
    }

    void Update()
    {
        playerCollector.radius = playerState.currentMagnet;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out ICollectible collectible))
        {
            //物品飞向玩家
            Rigidbody rb = other.GetComponent<Rigidbody>();
            Vector3 foreceDirection = (transform.position - other.transform.position).normalized;
            rb.velocity = foreceDirection * pullSpeed;

            collectible.collect();
            Debug.Log("获得经验");
        }
    }
}
