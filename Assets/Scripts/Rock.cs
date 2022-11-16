using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public float y = -1;
	float timer = 0;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
		timer += Time.deltaTime;
       

		if (timer>3)
		{

			transform.Translate(0, -y * Time.deltaTime, 0);
			if (timer > 6)
			{
				timer = 0;
			}
		}
		else
		{
			transform.Translate(0, y * Time.deltaTime, 0);
		}
    }
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag =="Player" )
		{
			//-Bölüm yeniden baþlama kodu gelecek-
		}

		

	}
    
}
