using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSpike : MonoBehaviour
{
    public GameObject platform;
    public Transform up, down;
    public float speed;
    [SerializeField] private float _direction = 1;
    private void Update()
    {
        DirectionCheck();
        platform.transform.position += Vector3.up * (speed * Time.deltaTime * _direction);
    }

    private void DirectionCheck()
    {
        if (platform.transform.position.y > up.position.y)
        {
            _direction = -1;
        }
        else if (platform.transform.position.y < down.position.y)
        {
            _direction = 1;
        }
    }
}
