using System.Collections;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public bool evil;

	private Rigidbody2D _rb;

	[SerializeField] float speed;

	private bool _isJumping;

	private bool _canDoubleJump;

	public static bool canMove = true;

	public bool isGrounded;
	[SerializeField] public LayerMask groundLayers;
	[SerializeField] private float checkDistance = 0.515f;
	[SerializeField] private Transform playerFeet;

	private void Awake()
	{
		_rb = GetComponent<Rigidbody2D>();
	}

	private void Start()
	{
		canMove = true;
	}

	private void Update()
	{
		if (OnDialogue) return;
		GroundCheck();
		Jump();
	}

	private void FixedUpdate()
	{
		if (OnDialogue) return;
		Move();
	}

	private void Move()
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
			_isJumping = true;
			_canDoubleJump = true; 
			_rb.velocity = new Vector2(_rb.velocity.y, speed);
		}
		else if (Input.GetKeyDown(KeyCode.W) && (_canDoubleJump || (!_isJumping && !isGrounded)))
		{
			_isJumping = true;
			_canDoubleJump = false;
			_rb.velocity = new Vector2(_rb.velocity.y, speed);
		}
	}
	private void GroundCheck()
	{
		float[] checkParts = new float[] { 0, .5f, -.5f };
		for (int i = 0; i < 3; i++)
		{
			if (Physics2D.Raycast(playerFeet.position + new Vector3(checkParts[i],0,0), Vector2.down, checkDistance, groundLayers))
			{
				isGrounded = true;
				return;
			}
		}

		isGrounded = false;
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
		SceneManager.LoadScene(scene.name);
		yield return new WaitForSeconds(.1f);
		canMove = true;
	}

	#region  TriggersAndCollisions
	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.CompareTag("Button"))
		{
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
			_rb.velocity = new Vector2(_rb.velocity.x, speed * spring.GetPower());
		}

		if (collision.gameObject.CompareTag("Box"))
		{
			_rb.interpolation = RigidbodyInterpolation2D.Interpolate;
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
			_rb.interpolation = RigidbodyInterpolation2D.None;
		}
	}
	#endregion
}
