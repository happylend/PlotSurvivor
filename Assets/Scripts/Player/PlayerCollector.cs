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
            //��Ʒ�������
            Rigidbody rb = other.GetComponent<Rigidbody>();
            Vector3 foreceDirection = (transform.position - other.transform.position).normalized;
            rb.velocity = foreceDirection * pullSpeed;

            collectible.collect();
            Debug.Log("��þ���");
        }
    }
}
