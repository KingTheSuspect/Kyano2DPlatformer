using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //Double jump, ground check'i tüm yüzeylerinden alýyor. Sadece alt taraftan almasýný saðlayacaðým.



    private Rigidbody2D rb;

    [SerializeField] float speed;

    int jumpCount = 0;
    
    
    
    //Ground check deðerleri (Sadece aþaðýyý kontrol ediyor)
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
        //Þu an keycode alýyor sadece, daha sonradan burayý düzelteceðim.
            if (Input.GetKeyDown(KeyCode.W) && jumpCount <= 1)
        {
                rb.velocity = new Vector2(rb.velocity.x, speed);
                jumpCount++;
        }
    }

    private void GroundCheck()
    {
        //Aþaðýya raycast gonderip belirlediðimiz mesafe ve yönde ground layer mask var mý diye kontrol ediyoruz
        var hit = Physics2D.Raycast(transform.position, Vector2.down, checkDistance, groundLayer);
        // *hit boolean bir deðiþken olarak kullanýlabilir
        if (hit)
        {
            jumpCount = 0;
            isGrounded = true;
        }
        else isGrounded = false;
        Debug.Log(isGrounded);
    }
}
