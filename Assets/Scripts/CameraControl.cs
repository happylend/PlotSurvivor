using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform target;
    public float smoothing = 5f;
    public Vector3 offset;

    void Start()
    {
        if (target == null) { target = GameObject.FindGameObjectWithTag("Player").transform; }
    }

    private void FixedUpdate()
    {
        Vector3 targetCamPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
}
