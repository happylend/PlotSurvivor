using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private Vector3 aimPoint;
    public float rotSpeed = 0.5f;

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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if(hit.transform.tag!=("Player"))
            {
                aimPoint = new Vector3(hit.point.x, hit.point.y, hit.point.z);

                float tempAngle = Vector3.Angle(transform.forward, aimPoint - transform.position);//自身和目标的夹角
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(aimPoint - transform.position), rotSpeed);
            }


        }
    }
}
