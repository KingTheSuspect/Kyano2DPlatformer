using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private Rigidbody2D _rb;

	[SerializeField] float speed;
	
	[HideInInspector] public static bool isJumping;

	private bool _canDoubleJump = false;
	
	[SerializeField] private float respawnTime;
	[SerializeField] private Vector3 startPosition;
	public static bool canMove = true;
	
	//Ground check deðerleri (Sadece aþaðýyý kontrol ediyor)
	public bool isGrounded;
	public LayerMask groundLayer;
	[SerializeField]private float checkDistance = 0.515f;
	[SerializeField] private Transform playerFeet;
	

	
	private void Awake()
	{
		_rb = GetComponent<Rigidbody2D>();
	}

	private void Start()
	{
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
		if (!canMove) return;
		_rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, _rb.velocity.y);
	}

	private bool OnDialogue => DialogueManager.GetInstance().isDialoguePlaying;
	
	private void Jump()
	{
		if (!canMove) return;
		if (Input.GetKeyDown(KeyCode.W) && isGrounded)
		{
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

	IEnumerator Respawn()
	{
		/*if (haveSwitchController)
		{
			var switchController = CharacterSwitchController.instance;
			switchController.index = 1;
			switchController.SwitchCharacter();
		}
		_rb.velocity = Vector2.zero;
		transform.position = startPosition;*/
		Scene scene = SceneManager.GetActiveScene();
		canMove = false;
		Initiate.Fade(scene.name, Color.black, respawnTime);
		yield return new WaitForSeconds(.1f);
		canMove = true;
	}
	void OnTriggerEnter2D(Collider2D collision)
	{
		
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Spring"))
		{
			collision.gameObject.GetComponent<Animator>().SetTrigger("Bounce");
			var spring = collision.gameObject.GetComponent<SpringData>();
			_rb.velocity = new Vector2(_rb.velocity.x, speed * spring.GetPower());
		}
		if (collision.gameObject.CompareTag("MovingPlatform"))
		{
			transform.SetParent(collision.transform);
		}
		if (collision.gameObject.CompareTag("Spike"))
		{
			StartCoroutine(Respawn());
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
