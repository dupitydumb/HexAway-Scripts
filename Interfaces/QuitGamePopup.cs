using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGamePopup : BasePopup
{
    public override void InitView()
    {

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
        GameManager.instance.livesManager.ConsumeLife();
        GameManager.instance.BackToHome();
    }
}
