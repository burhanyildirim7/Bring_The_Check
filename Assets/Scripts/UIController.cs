using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
	public static UIController instance;
	public GameObject TapToStartPanel, LoosePanel, GamePanel, WinPanel;
	public  Text  gamePlayScoreText, winScreenScoreText, levelNoText,totalScoreText;
	public Slider playerSlider;



	private void Awake()
	{
		if (instance == null) instance = this;
		else Destroy(this);
	}

	private void Start()
	{
		StartUI();
	}

	public void StartUI()
	{
		ActivateTapToStartScreen();
	}

	public void SetLevelText(int levelNo)
	{
		levelNoText.text = "Level " + levelNo.ToString();
	}

	// TAPTOSTART TU?UNA BASILDI?INDA  --- G?R?? EKRANINDA VE LEVEL BA?LARINDA
	public void TapToStartButtonClick()
	{

		GameManager.instance.isContinue = true;
		//PlayerController.instance.SetArmForGaming();
		TapToStartPanel.SetActive(false);
		GamePanel.SetActive(true);
		PlayerController.instance.PlayerSkateAnim();
		SetLevelText(LevelController.instance.totalLevelNo);

	}

	// RESTART TU?UNA BASILDI?INDA  --- LOOSE EKRANINDA
	public void RestartButtonClick()
	{
		GamePanel.SetActive(false);
		LoosePanel.SetActive(false);
		TapToStartPanel.SetActive(true);
		LevelController.instance.RestartLevelEvents();
	}


	// NEXT LEVEL TU?UNA BASILDI?INDA  --- W?N EKRANINDA
	public void NextLevelButtonClick()
	{
		TapToStartPanel.SetActive(true);
		WinPanel.SetActive(false);
		GamePanel.SetActive(false);
		LevelController.instance.NextLevelEvents();
		SetTotalScoreText();
	}


	public void SetScoreText()
	{
		gamePlayScoreText.text = GameManager.instance.score.ToString();
	}

	public void SetTotalScoreText()
	{
		totalScoreText.text = PlayerPrefs.GetInt("total").ToString();
	}

	public void WinScreenScore()
	{
		winScreenScoreText.text = GameManager.instance.score.ToString();
	}

	public void ActivateWinScreen()
	{
		GamePanel.SetActive(false);
		WinPanel.SetActive(true);
		WinScreenScore();
	}

	public void ActivateLooseScreen()
	{
		GamePanel.SetActive(false);
		LoosePanel.SetActive(true);
	}

	public void ActivateGameScreen()
	{
		GamePanel.SetActive(true);
		TapToStartPanel.SetActive(false);
	}

	public void ActivateTapToStartScreen()
	{
		TapToStartPanel.SetActive(true);
		WinPanel.SetActive(false);
		LoosePanel.SetActive(false);
		totalScoreText.text = PlayerPrefs.GetInt("total").ToString();
	}

	public IEnumerator SliderIncrease()
	{
		bool control = true;
		float sliderValue = playerSlider.value;
		float nextValue = sliderValue + 0.1f;
		while (control)
		{
			sliderValue += 0.02f;
			playerSlider.value = sliderValue;
			if (nextValue <= playerSlider.value) control = false;
			yield return new WaitForSeconds(0.02f);
		}
	}

	public IEnumerator SliderDecrease()
	{
		bool control = true;
		float sliderValue = playerSlider.value;
		float nextValue = sliderValue - 0.06f;
		while (control)
		{
			sliderValue -= 0.02f;
			playerSlider.value = sliderValue;
			if (nextValue >= playerSlider.value) control = false;
			yield return new WaitForSeconds(0.02f);
		}
	}

}
