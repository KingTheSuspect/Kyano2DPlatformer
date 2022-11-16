using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public float y = -1;
    
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, y*Time.deltaTime, 0);
		
    }
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.name == "Kaya")
		{

		}

		

	}
    
}
