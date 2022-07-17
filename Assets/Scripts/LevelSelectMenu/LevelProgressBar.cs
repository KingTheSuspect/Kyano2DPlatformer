using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelProgressBar : MonoBehaviour
{
	[SerializeField] Image ForeGround;

	void Start()
	{
		
	}

	void Update()
	{
		
	}

	public void SetBar(int _progress, int _max)
	{
		var tr = ForeGround.GetComponent<RectTransform>();
		tr.sizeDelta = new Vector2((float)_progress / _max * 1385.5f, tr.sizeDelta.y);
	}
}
