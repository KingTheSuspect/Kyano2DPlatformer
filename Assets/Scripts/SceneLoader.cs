using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
	public static SceneLoader instance;

	public int currentChapter;
	public int currentLevel;

	void Awake()
	{
		instance = this;
	}

	public void LoadScene()
	{
		int scene = (currentChapter - 1) * 5 + currentLevel;
		if (scene <= LevelSelectMenu.instance.GetLastLevelPassed() + 1)
		{
			Debug.Log($"{scene}");
			SceneManager.LoadScene(scene); 
		}
		else
		{
			Debug.Log("locked!");
		}
	}
}
