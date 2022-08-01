using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFeet : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        

        if (collision.gameObject.CompareTag("PlatformUp"))
        {
            Debug.Log("up");
            Physics2D.IgnoreLayerCollision(7,8,false);
        }
    }
}
