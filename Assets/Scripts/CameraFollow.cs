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

    public bool checkMaxPos;
    public float minX, maxX, minY, maxY;
    void Update()
    {
        Follow();
    }

    private void Follow()
    {
        Vector3 targetPos = target.position + offset;
        Vector3 smoothPos = Vector3.Lerp(this.transform.position, targetPos, smoothNumber * Time.deltaTime);
        this.transform.position = smoothPos;
        /*if (target.position.x > minX && target.position.x < maxX)
        {
            Vector3 smoothPos = Vector3.Lerp(this.transform.position, targetPos, smoothNumber * Time.deltaTime);
            transform.position = new Vector3(smoothPos.x, transform.position.y, smoothPos.z);
        }
        if (target.position.y > minY && target.position.y < maxY && checkMaxPos)
        {
            Vector3 smoothPos = Vector3.Lerp(this.transform.position, targetPos, smoothNumber * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, smoothPos.y, smoothPos.z);
        }
        else if (!checkMaxPos)
        {
            
        }*/

    }
}
