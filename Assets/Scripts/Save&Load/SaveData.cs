using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
	public int lastLevelPassed;

	public SaveData(int _lastLevelPassed)
	{
		lastLevelPassed = _lastLevelPassed;
	}
}
