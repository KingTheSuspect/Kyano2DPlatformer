using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectMenu : MonoBehaviour
{
	public static LevelSelectMenu instance;

	[SerializeField]
	private int lastLevelPassed;
	private int currentChapter;
	private int currentLevel;

	[SerializeField]
	private float slideTimer = 1;
	private float slideTimerCounter;

	[SerializeField] private int chapterCount;
	[SerializeField] float slideBound = 1100;
	[SerializeField] private LevelButton[] LevelButtons;
	private float[] LevelButtonPositions;

	[SerializeField] GameObject PrevButton;
	[SerializeField] GameObject NextButton;
	[SerializeField] GameObject[] DeactivateButtonsWhenSlide;

	[SerializeField] Transform Frame;
	[SerializeField] LevelProgressBar Bar;

	public LevelButtonTextures textures;

	enum State
	{
		Stay,
		Next,
		Prev
	}

	private State state;

	void Awake()
	{
		instance = this;
	}

	void Start()
	{
		Load();
		SetThis();
	}

	public void SetThis()
	{
		int _lastLevelPassed = Mathf.Clamp(lastLevelPassed, 0, 14);
		Bar.SetBar(lastLevelPassed, 15);
		SetCurrentChapter(_lastLevelPassed / 5 + 1);
		SetCurrentLevel(_lastLevelPassed % 5 + 1);
		SetFramePosition();
		GetButtonPositions();
		foreach (LevelButton button in LevelButtons)
		{
			SetLevelButtonNumber(button);
		}
		SetOtherButtons();
	}

	void FixedUpdate()
	{
		if (!isSlideEnd())//animation continues
		{
			slideTimerCounter -= Time.fixedDeltaTime;
		}
		else//animation end
		{
			if (state != State.Stay)
			{
				ResetButtonPositions();
				state = State.Stay;
			}
		}
		if (!isSlideEnd())//animation continues (even after time passed)
		{
			switch (state)
			{
				case State.Prev:
					SlideAnimation(1);
					break;
				case State.Next:
					SlideAnimation(-1);
					break;
			}
		}
	}

	public void PreviousChapter()
	{
		if (currentChapter > 1 || currentLevel > 1)
		{
			currentLevel--;
			if (currentLevel < 1)
			{
				currentLevel = 5;
				SlideStart();
				state = State.Prev;
				SetCurrentChapter(currentChapter - 1);
				DeactivateFrame();
			}
			SetCurrentLevel(currentLevel);
			SetFramePosition();
		}
	}

	public void NextChapter()
	{
		if (currentChapter < chapterCount || currentLevel < 5)
		{
			currentLevel++;
			if (currentLevel > 5)
			{
				currentLevel = 1;
				SlideStart();
				state = State.Next;
				SetCurrentChapter(currentChapter + 1);
				DeactivateFrame();
			}
			SetCurrentLevel(currentLevel);
			SetFramePosition();
		}
	}

	#region SlideAnimation
	bool isSlideEnd()
	{
		return slideTimerCounter <= 0;
	}

	void SlideStart()
	{
		slideTimerCounter = slideTimer;
		foreach (GameObject button in DeactivateButtonsWhenSlide)
		{
			button.SetActive(false);
		}
	}

	void SlideAnimation(int _multiplier)
	{
		foreach (LevelButton button in LevelButtons)
		{
			Vector3 posX = button.transform.localPosition;

			posX += Vector3.right * _multiplier * Time.fixedDeltaTime * slideBound * 2f;
			if (slideBound < posX.x * _multiplier)//if button position is out of screen bounds
			{
				posX += Vector3.left * _multiplier * slideBound * 2f;
				SetLevelButtonNumber(button);
			}

			button.transform.localPosition = posX;
		}
	}
	#endregion

	#region ButtonFunctions
	void DeactivateFrame()
	{
		Frame.gameObject.SetActive(false);
	}

	void SetFramePosition()
	{
		Frame.position = LevelButtons[currentLevel - 1].transform.position;
	}

	void GetButtonPositions()
	{
		LevelButtonPositions = new float[LevelButtons.Length];
		for (int i = 0; i < LevelButtonPositions.Length; i++)
		{
			LevelButtonPositions[i] = LevelButtons[i].transform.localPosition.x;
		}
	}

	void ResetButtonPositions()
	{
		SetOtherButtons();

		for (int i = 0; i < LevelButtonPositions.Length; i++)
		{
			LevelButtons[i].transform.localPosition = Vector3.right * LevelButtonPositions[i];
		}
	}

	void SetOtherButtons()
	{
		foreach (GameObject button in DeactivateButtonsWhenSlide)
		{
			button.SetActive(true);
		}
		//if (currentChapter <= 1) PrevButton.SetActive(false);
		//if (currentChapter >= chapterCount) NextButton.SetActive(false);
	}

	void SetLevelButtonNumber(LevelButton _button)
	{
		int buttonIndex = (_button.GetNumber() - 1) % 5;
		int chapterCont = (currentChapter - 1) * 5;
		int number = chapterCont + buttonIndex + 1;

		_button.SetNumber(number);
	}
	#endregion

	#region Getter&Setter
	public int GetCurrentChapter()
	{
		return currentChapter;
	}

	public void SetCurrentChapter(int _value)
	{
		currentChapter = _value;
		SceneLoader.instance.currentChapter = currentChapter;
	}

	public int GetCurrentLevel()
	{
		return currentLevel;
	}

	public void SetCurrentLevel(int _value)
	{
		currentLevel = _value;
		SceneLoader.instance.currentLevel = currentLevel;
	}

	public int GetLastLevelPassed()
	{
		return lastLevelPassed;
	}

	public void SetLastLevelPassed(int _value)
	{
		lastLevelPassed = _value;
	}
	#endregion

	public void Save()
	{
		PlayerPrefs.SetInt("LastLevelPassed", lastLevelPassed);
	}

	public void Load()
	{
		lastLevelPassed = PlayerPrefs.GetInt("LastLevelPassed");
	}

	private void OnApplicationQuit()
	{
		Save();
	}
}