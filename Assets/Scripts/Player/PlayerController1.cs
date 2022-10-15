using System.Collections;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerController1 : MonoBehaviour 
{

    //Movement
    public float speed;
    public float jumpForce;
    private float moveInput;
    float moveVelocity;
    public bool evil;
    public Transform ownerObject;
	private Rigidbody2D rb;

    public bool facingRight = true;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private int extraJumps;
    public int extraJumpsValue;

    //Grounded Vars
    public bool grounded = true;

    void Start() {
        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
    }

    private bool OnDialogue => DialogueManager.GetInstance().isDialoguePlaying;

    void FixedUpdate () {
        if (OnDialogue) return;


        //Hareket
        moveInput =Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        if(facingRight == false && moveInput > 0) {
            Flip();
        }  else if (facingRight == true && moveInput < 0){
            Flip();

        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
    }

    void Update() {

        if(isGrounded == true) {
            extraJumps = extraJumpsValue;
        }
        
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space) && extraJumps > 0) {
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;

        } else if (Input.GetKeyDown(KeyCode.W) && extraJumps == 0 && isGrounded == true) { 
            rb.velocity = Vector2.up * jumpForce;
        }

    }

    IEnumerator Respawn()
	{
		/*if (haveSwitchController)
		{
			var switchController = CharacterSwitchController.instance;
			switchController.index = 1;
			switchController.SwitchCharacter();
		}
		rb.velocity = Vector2.zero;
		transform.position = startPosition;*/
		Scene scene = SceneManager.GetActiveScene();
		SceneManager.LoadScene(scene.name);
		yield return new WaitForSeconds(.1f);	
	}

    	#region  TriggersAndCollisions
	private void OnTriggerEnter2D(Collider2D col)
	{

		if (col.gameObject.CompareTag("Button"))
		{
			col.gameObject.GetComponent<IButtonInteract>().Interact(true);
		}

		if (col.gameObject.CompareTag("OwnerStarter"))
		{
			
			GameObject.FindWithTag("MainCamera").GetComponent<CameraFollow>().target = ownerObject;
		}

		if (col.gameObject.CompareTag("Spike"))
			{
				StartCoroutine(Respawn());
			}


	}

	private void OnTriggerExit2D(Collider2D col)
	{

        if (col.gameObject.CompareTag("Ground"))
        {
            grounded = false;
        } 


		if (col.gameObject.CompareTag("Button"))
		{
			col.gameObject.GetComponent<IButtonInteract>().Interact(false);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (!evil)
		{
			if (collision.gameObject.CompareTag("SpikeBall"))
			{
				StartCoroutine(Respawn());
			}

			if (collision.gameObject.CompareTag("Spike"))
			{
				StartCoroutine(Respawn());
			}
		}

		if (collision.gameObject.CompareTag("Spring"))
		{
			collision.gameObject.GetComponent<Animator>().SetTrigger("Bounce");
			var spring = collision.gameObject.GetComponent<SpringData>();
			rb.velocity = new Vector2(rb.velocity.x, speed * spring.GetPower());
		}

		if (collision.gameObject.CompareTag("Box"))
		{
			rb.interpolation = RigidbodyInterpolation2D.Interpolate;
		}

		if (collision.gameObject.CompareTag("MovingPlatform"))
		{
			transform.SetParent(collision.transform);
		}

		if (collision.gameObject.CompareTag("MovingPlatforms"))
		{
			transform.SetParent(collision.transform);
		}
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("MovingPlatform"))
		{
			transform.SetParent(collision.transform.parent.parent);
		}

		if (collision.gameObject.CompareTag("MovingPlatforms"))
		{
			transform.SetParent(collision.transform.parent.parent.parent);
		}
		
		if (collision.gameObject.CompareTag("Box"))
		{
			rb.interpolation = RigidbodyInterpolation2D.None;
		}
	}
	#endregion

    public void Flip() {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }


}

