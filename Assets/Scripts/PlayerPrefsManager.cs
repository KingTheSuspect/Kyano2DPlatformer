using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
	private void OnApplicationQuit()
	{
		PlayerPrefs.Save();
	}
}
