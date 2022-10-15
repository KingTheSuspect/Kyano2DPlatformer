using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour {

	Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.gameObject.name.Equals ("Player")) {
			PlatformManager.Instance.StartCoroutine("SpawnPlatform",
					new Vector2 (transform.position.x, transform.position.y));
			Invoke ("DropPlatform", 0.8f);
			DropPlatform();
			Destroy (gameObject, 0.8f);
		}

	}

	void DropPlatform()
	{
		rb.isKinematic = false;
	}

}
