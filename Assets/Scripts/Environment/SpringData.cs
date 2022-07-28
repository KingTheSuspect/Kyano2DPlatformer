using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringData : MonoBehaviour
{
	[SerializeField] float power;

	public float GetPower()
	{
		return power;
	}

	public void SetPower(float _power)
	{
		power = _power;
	}
}
