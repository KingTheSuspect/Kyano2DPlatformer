using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchbleBox : MonoBehaviour
{   
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
}

