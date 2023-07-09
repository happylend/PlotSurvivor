using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBulletBehaviour : BulletBehaviour
{
    ArrowController AC;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        base.Start();
        AC = FindObjectOfType<ArrowController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.LogError("speed: " + currentSpeed);
        Debug.LogError("dir: " + direction.normalized);
        transform.position += direction.normalized * currentSpeed * Time.deltaTime;

    }
}
