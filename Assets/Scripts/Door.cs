using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    public GameObject[] buttons;
    public UnityEvent job;
    public float seconds;
    private bool _invoked;
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
        Debug.Log("Door Opened");
        _invoked = true;
        job.Invoke();
    }
    public void ChangeY(float y)
    {
        Debug.Log("Changing Y of door");
        LeanTween.moveLocalY(gameObject, y, seconds);
    }
    public void ChangeX(float x)
    {
        LeanTween.moveLocalX(gameObject, x, seconds);
    }
}
