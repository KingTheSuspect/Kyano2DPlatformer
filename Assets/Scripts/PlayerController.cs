using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private Rigidbody2D _rb;

	[SerializeField] float speed;
	
	[HideInInspector] public static bool isJumping;

	private bool _canDoubleJump = false;
	
	//Ground check deðerleri (Sadece aþaðýyý kontrol ediyor)
	public bool isGrounded;
	public LayerMask groundLayer;
	[SerializeField]private float checkDistance = 0.515f;
	[SerializeField] private Transform playerFeet;
	[SerializeField] private bool haveSwitchController;
	[SerializeField] private Vector3 startPosition;


	private Vector3 playerScale;
	private void Awake()
	{
		_rb = GetComponent<Rigidbody2D>();
	}

	private void Start()
	{
		playerScale = transform.localScale;
		startPosition = transform.position;
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
		_rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, _rb.velocity.y);
	}

	private bool OnDialogue => DialogueManager.GetInstance().isDialoguePlaying;
	
	private void Jump()
	{
		if (Input.GetKeyDown(KeyCode.W) && isGrounded)
		{
			Debug.Log("jump");
			_canDoubleJump = true;
			_rb.velocity = new Vector2(_rb.velocity.y, speed);
		}
		else if (Input.GetKeyDown(KeyCode.W) && _canDoubleJump)
		{
			_canDoubleJump = false;
			_rb.velocity = new Vector2(_rb.velocity.y, speed);
		}
	}
	private void GroundCheck()
	{
		var hit = Physics2D.Raycast(playerFeet.position, Vector2.down, checkDistance, groundLayer);
		if (hit)
		{
			isJumping = false;
			isGrounded = true;
		}
		else isGrounded = false;
		//Debug.Log(isGrounded);
	}

	public void Respawn()
	{
		if (haveSwitchController)
		{
			var switchController = CharacterSwitchController.instance;
			switchController.index = 1;
			switchController.SwitchCharacter();
		}
		_rb.velocity = Vector2.zero;
		transform.position = startPosition;
	}

	void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.CompareTag("Spike"))
		{
			Respawn();
		}
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Spring"))
		{
			var spring = collision.GetComponent<SpringData>();
			_rb.velocity = new Vector2(_rb.velocity.x, speed * spring.GetPower());
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("MovingPlatform"))
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
	}
}
