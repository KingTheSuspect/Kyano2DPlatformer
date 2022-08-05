using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    public GameObject[] buttons;
    public UnityEvent job;
    private bool _invoked;
    public float seconds;
    private void Update()
    {
        if(_invoked) return;
        foreach (var button in buttons)
        {
            if (!button.GetComponent<ButtonManager>().IsInteracted())
            {
                return;
            }
        }
        _invoked = true;
        job.Invoke();
    }
    public void ChangeY(float y)
    {
        transform.DOMoveY(y, seconds);
    }
}
