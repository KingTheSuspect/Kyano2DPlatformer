using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBallController : MonoBehaviour
{
    public float speed;
    private Rigidbody2D _rb;
    public bool movingClockWise = true;
    public float maxDir;
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        DirectionCheck();
        if (movingClockWise)
        {
            transform.Rotate(0,0, speed * Time.deltaTime);
        }
        else
        {
            transform.Rotate(0,0, -speed * Time.deltaTime);
        }
    }

    void DirectionCheck()
    {
        if (transform.rotation.z > maxDir)
        {
            movingClockWise = false;
        }
        else if (transform.rotation.z < -maxDir)
        {
            movingClockWise = true;
        }
    }
}
