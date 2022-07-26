using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //Double jump, ground check'i t�m y�zeylerinden al�yor. Sadece alt taraftan almas�n� sa�layaca��m.



    private Rigidbody2D rb;

    [SerializeField] float speed;

    int jumpCount = 0;
    
    
    
    //Ground check de�erleri (Sadece a�a��y� kontrol ediyor)
    public bool isGrounded;
    public LayerMask groundLayer;
    private float checkDistance = 0.515f;
    private void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*if (collision.gameObject.tag == "Ground")
        {
            jumpCount = 0;
        }
        Debug.Log(collision.gameObject.tag);*/
    }
   

    private void Update()
    {
        GroundCheck();
        if (DialogueManager.GetInstance().isDialoguePlaying)
        {
            return;
        }
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);
        //�u an keycode al�yor sadece, daha sonradan buray� d�zeltece�im.
            if (Input.GetKeyDown(KeyCode.W) && jumpCount <= 1)
        {
                rb.velocity = new Vector2(rb.velocity.x, speed);
                jumpCount++;
        }
    }

    private void GroundCheck()
    {
        //A�a��ya raycast gonderip belirledi�imiz mesafe ve y�nde ground layer mask var m� diye kontrol ediyoruz
        var hit = Physics2D.Raycast(transform.position, Vector2.down, checkDistance, groundLayer);
        // *hit boolean bir de�i�ken olarak kullan�labilir
        if (hit)
        {
            jumpCount = 0;
            isGrounded = true;
        }
        else isGrounded = false;
        Debug.Log(isGrounded);
    }
}
