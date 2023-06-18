using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    RaycastHit hit;
    public GameObject Bullet;
    public Transform spawnPosition;
    [HideInInspector]
    public int currentProjectile = 0;
    public float speed = 1000;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
