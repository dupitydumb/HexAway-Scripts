using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PopupSetting : BasePopup
{
    public GameObject musicOn, musicOff;

    public GameObject soundOn, soundOff;

    public GameObject vibrationOn, vibrationOff;

    public string androidGameUrl;

    public string iosGameUrl;

    public string termUrl;

    public string privacyUrl;

    public string contactUrl;

    public RectTransform homeBtn;

    public override void InitView()
    {
        if (GameManager.instance.currentGameState == GameManager.GAME_STATE.HOME)
            homeBtn.gameObject.SetActive(false);
        else if (GameManager.instance.currentGameState == GameManager.GAME_STATE.PLAYING)
            homeBtn.gameObject.SetActive(true);

        if (AudioManager.instance.musicState == 0)
        {
            musicOn.SetActive(false);
            musicOff.SetActive(true);
        }
        else
        {
            musicOn.SetActive(true);
            musicOff.SetActive(false);
        }

        if (AudioManager.instance.soundState == 0)
        {
            soundOn.SetActive(false);
            soundOff.SetActive(true);
        }
        else
        {
            soundOn.SetActive(true);
            soundOff.SetActive(false);
        }

        if (AudioManager.instance.hapticState == 0)
        {
            vibrationOn.SetActive(false);
            vibrationOff.SetActive(true);
        }
        else
        {
            vibrationOn.SetActive(true);
            vibrationOff.SetActive(false);
        }
    }

    public override void Start()
    {
        
    }

    public override void Update()
    {
       
    }

    public override void HideView()
    {
        base.HideView();
        GameManager.instance.uiManager.coinView.ShowView();
    }

    public void BackToHome()
    {
        AudioManager.instance.clickSound.Play();
        HideView();
        GameManager.instance.uiManager.quitGamePopup.ShowView();
        //GameManager.instance.BackToHome();
    }

    public void ToggleSound()
    {

        if (AudioManager.instance.soundState == 0)
        {
            soundOn.SetActive(true);
            soundOff.SetActive(false);
            AudioManager.instance.ToogleSound(true);
        }
        else
        {
            soundOn.SetActive(false);
            soundOff.SetActive(true);
            AudioManager.instance.ToogleSound(false);
        }
    }

    public void ToggleMusic()
    {

        if (AudioManager.instance.musicState == 0)
        {
            musicOn.SetActive(true);
            musicOff.SetActive(false);
            AudioManager.instance.ToogleMusic(true);
        }
        else
        {
            musicOn.SetActive(false);
            musicOff.SetActive(true);
            AudioManager.instance.ToogleMusic(false);
        }
    }

    public void ToggleHaptic()
    {

        if (AudioManager.instance.hapticState == 0)
        {
            vibrationOn.SetActive(true);
            vibrationOff.SetActive(false);
            AudioManager.instance.ToogleHaptic(true);
        }
        else
        {
            vibrationOn.SetActive(false);
            vibrationOff.SetActive(true);
            AudioManager.instance.ToogleHaptic(false);
        }
    }

    public void Restore()
    {
        IAPManager.instance.RestorePurchases();
    }
}
