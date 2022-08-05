using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPos : MonoBehaviour
{
    public Transform t;

    private void Update()
    {
        transform.position = t.position;
    }
}
