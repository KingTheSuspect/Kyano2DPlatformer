using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
	[SerializeField] Transform platform;
	[SerializeField] Transform borderLeft;
	[SerializeField] Transform borderRight;

	[SerializeField] float speed;
	[SerializeField] int directionMultiplier = 1;

	[Header("Optional:")]
	[SerializeField] ButtonManager button;

	void Start()
	{
		
	}

	void Update()
	{
		if (button == null || button.IsInteracted())
		Move();
		CheckDirection();
	}

	void Move()
	{
		platform.position += Vector3.right * Time.deltaTime * speed * directionMultiplier;
	}

	void CheckDirection()
	{
		float halfScaleX = platform.localScale.x / 2;
		if (platform.localPosition.x - halfScaleX < borderLeft.localPosition.x)
		{
			directionMultiplier = 1;
		}
		else if (platform.localPosition.x + halfScaleX > borderRight.localPosition.x)
		{
			directionMultiplier = -1;
		}
	}
	public float GetSpeed()
	{
		return speed;
	}

	public void SetSpeed(float _speed)
	{
		speed = _speed;
	}
}
