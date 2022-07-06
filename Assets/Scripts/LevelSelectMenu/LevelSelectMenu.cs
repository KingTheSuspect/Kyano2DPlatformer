using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectMenu : MonoBehaviour
{
	public static LevelSelectMenu instance;

	[SerializeField]
	private int lastLevelPassed;
	private int currentChapter;

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
		SetCurrentChapter(lastLevelPassed / 5 + 1);
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
		if (currentChapter > 1)
		{
			SlideStart();
			state = State.Prev;
			SetCurrentChapter(currentChapter - 1);
		}
	}

	public void NextChapter()
	{
		if (currentChapter < chapterCount)
		{
			SlideStart();
			state = State.Next;
			SetCurrentChapter(currentChapter + 1);
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
		if (currentChapter <= 1) PrevButton.SetActive(false);
		if (currentChapter >= chapterCount) NextButton.SetActive(false);
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
		SaveData data = new SaveData(lastLevelPassed);
		SaveLoadSystem.Save(data);
	}

	public void Load()
	{
		SaveData data = SaveLoadSystem.Load();
		if (data == null)
		{
			Save();
			Load();
		}
		else
		{
			lastLevelPassed = data.lastLevelPassed;
		}
	}
}