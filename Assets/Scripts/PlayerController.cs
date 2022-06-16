using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //Double jump, ground check'i t�m y�zeylerinden al�yor. Sadece alt taraftan almas�n� sa�layaca��m.



    private Rigidbody2D rb;

    [SerializeField] float speed;

    int jumpCount = 0;
    public bool isGrounded;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            jumpCount = 0;
        }
        Debug.Log(collision.gameObject.tag);
    }
   

    private void Update()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);

        
        //�u an keycode al�yor sadece, daha sonradan buray� d�zeltece�im.


        if (Input.GetKeyDown(KeyCode.Space) && jumpCount <= 1)
        {
                rb.velocity = new Vector2(rb.velocity.x, speed);
                jumpCount++;
        }
    }
}
