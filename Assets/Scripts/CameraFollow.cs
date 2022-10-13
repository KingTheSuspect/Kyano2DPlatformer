using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    [Range(1, 10)]
    public float smoothNumber;
    void Update()
    {
        Follow();
    }

    public void Follow()
    {
        Vector3 targetPos = target.position + offset;
        Vector3 smoothPos = Vector3.Lerp(this.transform.position, targetPos, smoothNumber * Time.deltaTime);
        this.transform.position = smoothPos;
    }
}
