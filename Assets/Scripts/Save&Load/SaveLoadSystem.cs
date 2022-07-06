using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveLoadSystem
{
	public static void Save(SaveData _data)
	{
		string path = Application.persistentDataPath + "/save.data";

		string json = JsonUtility.ToJson(_data);

		StreamWriter writer = File.CreateText(path);
		writer.Close();

		File.WriteAllText(path, json);
	}

	public static SaveData Load()
	{
		string path = Application.persistentDataPath + "/save.data";
		if (File.Exists(path))
		{
			string data = File.ReadAllText(path);

			SaveData newSaveData = JsonUtility.FromJson<SaveData>(data);

			return newSaveData;
		}
		else
		{
			//Debug.LogError($"Save file can not be found on '{path}' location");
			return null;
		}
	}
}
