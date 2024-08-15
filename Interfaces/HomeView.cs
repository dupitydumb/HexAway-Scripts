using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeView : BaseView
{
    public Text currentLevelTxt;

    public Text spinProgressTxt;

    public Image spinProgressBar;

    public override void InitView()
    {
        currentLevelTxt.text = "Level " + GameManager.instance.levelIndex.ToString();
        spinProgressBar.fillAmount = (float)(GameManager.instance.currentLuckyWheel) / 5.0f;
        spinProgressTxt.text = GameManager.instance.currentLuckyWheel.ToString() + "/5";
    }

    public override void Start()
    {

    }

    public override void Update()
    {

    }

    public void PlayGame()
    {
        if (GameManager.instance.livesManager.lives > 0)
        {
            AudioManager.instance.clickSound.Play();
            GameManager.instance.PlayGame();
        }
        else
        {
            AudioManager.instance.clickSound.Play();
            GameManager.instance.uiManager.fillLivesPopup.InitView();
            GameManager.instance.uiManager.fillLivesPopup.ShowView();
        }
    }

    public void ShowSetting()
    {
        AudioManager.instance.clickSound.Play();
        GameManager.instance.uiManager.settingPopup.InitView();
        GameManager.instance.uiManager.settingPopup.ShowView();
    }

    public void ShowShop()
    {
        AudioManager.instance.clickSound.Play();
        GameManager.instance.uiManager.shopPopup.InitView();
        GameManager.instance.uiManager.shopPopup.ShowView();
    }


    public void ShowPiggyBank()
    {
        AudioManager.instance.clickSound.Play();
        GameManager.instance.uiManager.piggyBankPopup.InitView();
        GameManager.instance.uiManager.piggyBankPopup.ShowView();
    }

    public void ShowQuest()
    {
        AudioManager.instance.clickSound.Play();
        GameManager.instance.uiManager.questPopup.InitView();
        GameManager.instance.uiManager.questPopup.ShowView();
    }

    public void ShowDaily()
    {

        AudioManager.instance.clickSound.Play();
        GameManager.instance.uiManager.dailyPopup.InitView();
        GameManager.instance.uiManager.dailyPopup.ShowView();

    }

    public void ShowLuckyWheel()
    {
        if (GameManager.instance.currentLuckyWheel == 5)
        {
            AudioManager.instance.clickSound.Play();
            GameManager.instance.uiManager.luckyWheelView.InitView();
            GameManager.instance.uiManager.luckyWheelView.ShowView();
        }

    }
}
