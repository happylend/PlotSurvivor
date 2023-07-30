using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBulletBehaviour : BulletBehaviour
{
    // Start is called before the first frame update
    protected virtual void Start()
    {
        base.Start();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += direction.normalized * currentSpeed * Time.deltaTime;

    }
}
