using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] float speed;

    int jumpCount = 2;

    [HideInInspector] public static bool isJumping;

    private bool canDoubleJump = false;
    
    //Ground check deðerleri (Sadece aþaðýyý kontrol ediyor)
    public bool isGrounded;
    public LayerMask groundLayer;
    [SerializeField]private float checkDistance = 0.515f;
    [SerializeField] private Transform playerFeet;
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
        if(OnDialogue) return;
        GroundCheck();
        Move();
        Jump();
    }
    protected void Move()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);
    }

    private bool OnDialogue => DialogueManager.GetInstance().isDialoguePlaying;
    
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            canDoubleJump = true;
            jump();
        }
        else if (Input.GetKeyDown(KeyCode.W) && canDoubleJump)
        {
            canDoubleJump = false;
            jump();
        }
        void jump()
        {
            isJumping = true;
            rb.velocity = new Vector2(rb.velocity.y, speed);
        }
    }

    private void GroundCheck()
    {
        var hit = Physics2D.Raycast(playerFeet.position, Vector2.down, checkDistance, groundLayer);
        if (hit)
        {
            isJumping = false;
            canDoubleJump = true;
            isGrounded = true;
        }
        else isGrounded = false;
        Debug.Log(isGrounded);
    }
}
