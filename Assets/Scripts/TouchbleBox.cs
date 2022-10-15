using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchbleBox : MonoBehaviour
{   

    public float horizontalSpeed = 1f;
    PlayerController1 player;
    Rigidbody2D boxBody;
    Collider2D colider;
    bool isGettingPushed = false;
 
    void Awake()
    {
        boxBody = transform.parent.GetComponent<Rigidbody2D>();
    }
 
    void FixedUpdate()
    {
        if(isGettingPushed)
        {
            Vector2 velocity = boxBody.velocity;
            velocity.x = horizontalSpeed;
            boxBody.velocity = velocity;
            if (player.facingRight)
                boxBody.velocity = velocity * player.speed;
            else
                boxBody.velocity = -velocity * player.speed;
        }


    }
 
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider != null && collider.CompareTag("Player"))
        {
            player = collider.gameObject.GetComponent<PlayerController1>();
            isGettingPushed = true;
        }
    }
 
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider != null && collider.CompareTag("Player"))
        {
            isGettingPushed = false;
        }
    }
}

