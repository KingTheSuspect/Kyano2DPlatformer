using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class LevelButton : MonoBehaviour//, IPointerDownHandler, IPointerUpHandler
{
	private int number;
	private TextMeshProUGUI tmp;
	//private Button button;

	void Awake()
	{
		tmp = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
		//button = GetComponent<Button>();

		number = int.Parse(tmp.text);
	}

	void Update()
	{
		
	}

	public int GetNumber()
	{
		return number;
	}

	public void SetNumber(int _number)
	{
		number = _number;
		SetText();
		SetTexture();
		SetState();
	}

	void SetText()
	{
		tmp.text = $"{number}";
	}

	void SetTexture()
	{
		//var spriteState = button.spriteState;
		int chapter = LevelSelectMenu.instance.GetCurrentChapter() - 1;
		var textures = LevelSelectMenu.instance.textures;
		
		GetComponent<Image>().sprite = textures.defaultTextures[chapter];
		//spriteState.highlightedSprite = textures.defaultTextures[chapter];
		//spriteState.selectedSprite = textures.defaultTextures[chapter];
		//spriteState.pressedSprite = textures.pressedTextures[chapter];
		//spriteState.disabledSprite = textures.disabledTextures[chapter];
		
		//button.spriteState = spriteState;
	}

	void SetState()
	{
		int chapter = LevelSelectMenu.instance.GetCurrentChapter() - 1;
		var textures = LevelSelectMenu.instance.textures;
		if (number <= LevelSelectMenu.instance.GetLastLevelPassed() + 1)
		{
			GetComponent<Image>().sprite = textures.defaultTextures[chapter];
		}
		else
		{
			GetComponent<Image>().sprite = textures.disabledTextures[chapter];
		}
		//button.interactable = number <= LevelSelectMenu.instance.GetLastLevelPassed() + 1;
	}

	/*public void OnPointerDown(PointerEventData eventData)
	{
		if (Input.GetMouseButtonDown(0)) transform.GetChild(0).localPosition = Vector3.down * 3f;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (Input.GetMouseButtonUp(0)) transform.GetChild(0).localPosition = Vector3.up * 3.2f;
	}*/
}
