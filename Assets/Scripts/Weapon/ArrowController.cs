using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : WeaponController
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        //�����ӵ�
        GameObject ArrowBullet = Instantiate(CurrentbuttleObj);
        ArrowBullet.transform.position = transform.position;
        ArrowBullet.GetComponent<BulletBehaviour>().DirectionChecker(transform.parent.forward);
        
    }
}
