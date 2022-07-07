using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
	private GameObject ayarlar;
	public GameObject exit;
	public GameObject CanvasSahne;
	[SerializeField] private Toggle ToggleSFX;
	[SerializeField] private Toggle ToggleMusic;
	[SerializeField] private Toggle ToggleNarrator;
	[SerializeField] private bool sfx_on;
	[SerializeField] private bool music_on;
	[SerializeField] private bool narrator_on;

	void Awake()
	{
		///TODO Load options data
	}

	void Start()
	{
		ayarlar = GameObject.Find("Ayarlar");
		ayarlar.GetComponent<Button>().onClick.AddListener(Loader);

		ToggleSFX.SetIsOnWithoutNotify(sfx_on);
		ToggleMusic.SetIsOnWithoutNotify(music_on);
		ToggleNarrator.SetIsOnWithoutNotify(narrator_on);
	}

	void Loader()
	{
		CanvasSahne.SetActive(true);
	   
	}

	public void Quit()
	{
		CanvasSahne.SetActive(false);
	}

	#region ButtonClickFunctions
	public void ButtonSFXClick()
	{
		sfx_on = !sfx_on;
		ToggleSFX.isOn = sfx_on;
	}

	public void ButtonMusicClick()
	{
		music_on = !music_on;
		ToggleMusic.isOn = music_on;
		if (music_on)
		{
			AudioListener.volume = 1;
		}
		else
		{
			AudioListener.volume = 0;
		}
	}

	public void ButtonNarratorClick()
	{
		narrator_on = !narrator_on;
		ToggleNarrator.isOn = narrator_on;
	}

	public void ButtonLanguageClick(int _index)
	{
		///TODO Select language that based on index
		switch (_index)
		{
			case 0:
				Debug.Log("Turkish");
				break;
			case 1:
				Debug.Log("English");
				break;
		}
	}
	#endregion
}
