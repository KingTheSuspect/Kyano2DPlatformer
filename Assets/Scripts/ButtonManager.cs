using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour, IButtonInteract
{
    private bool _isInteracted;
    public void Interact(bool i)
    {
        Debug.Log("Interacted Button: "+ gameObject.name);
        GetComponent<Animator>().SetBool("ButtonPressed", i);
        _isInteracted = i;
    }
    public bool IsInteracted()
    {
        return _isInteracted;
    }
}
