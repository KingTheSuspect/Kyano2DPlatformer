using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
	public static SceneLoader instance;

	public int currentChapter;

	void Awake()
	{
		instance = this;
	}

	public void LoadScene(int _buttonIndex)
	{
		int scene = (currentChapter - 1) * 5 + _buttonIndex;
		Debug.Log($"{scene}");
		//SceneManager.LoadScene(scene); 
	}
}
