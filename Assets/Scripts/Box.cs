using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Button"))
        {
            Debug.Log("Box pressed to: "+col.gameObject.name);
            col.gameObject.GetComponent<IButtonInteract>().Interact(true);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Button"))
        {
            col.gameObject.GetComponent<IButtonInteract>().Interact(false);
        }
    }
}
