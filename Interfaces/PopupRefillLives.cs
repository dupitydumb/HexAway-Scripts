using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using static AdsControl;

public class PopupRefillLives : BasePopup
{
	public Text textTime;

    public override void InitView()
    {
        //GameManager.instance.uiManager.coinView.InitView();
        //GameManager.instance.uiManager.coinView.ShowView();
    }

    public override void Start()
    {
        
    }

    public override void Update()
    {
        
    }

    public void MoreLivesByCoin()
    {
        HideView();
        AudioManager.instance.clickSound.Play();
        if(GameManager.instance.coinValue >= 150)
        {
            GameManager.instance.SubCoin(150);
            GameManager.instance.livesManager.GiveOneLife();
        }
        else
        {
            GameManager.instance.uiManager.moreCoinPopup.InitView();
            GameManager.instance.uiManager.moreCoinPopup.ShowView();
        }
    }

    public void MoreLivesByAds()
    {
        HideView();
        AudioManager.instance.clickSound.Play();
        WatchAds();
    }

    public override void HideView()
    {
        base.HideView();
        GameManager.instance.uiManager.coinView.ShowView();
    }

    public void WatchAds()
    {
        AudioManager.instance.clickSound.Play();
        if (AdsControl.Instance.currentAdsType == ADS_TYPE.ADMOB)
        {
            if (AdsControl.Instance.rewardedAd != null)
            {
                if (AdsControl.Instance.rewardedAd.CanShowAd())
                {
                    AdsControl.Instance.ShowRewardAd(EarnReward);
                }
            }
        }
        else if (AdsControl.Instance.currentAdsType == ADS_TYPE.UNITY)
        {
            ShowRWUnityAds();
        }
        else if (AdsControl.Instance.currentAdsType == ADS_TYPE.MEDIATION)
        {
            if (AdsControl.Instance.rewardedAd.CanShowAd())

                AdsControl.Instance.ShowRewardAd(EarnReward);

            else
                ShowRWUnityAds();
        }
    }

    public void EarnReward(Reward reward)
    {
        GameManager.instance.livesManager.GiveOneLife();
    }

    public void ShowRWUnityAds()
    {
        AdsControl.Instance.PlayUnityVideoAd((string ID, UnityAdsShowCompletionState callBackState) =>
        {

            if (ID.Equals(AdsControl.Instance.adUnityRWUnitId) && callBackState.Equals(UnityAdsShowCompletionState.COMPLETED))
            {
                GameManager.instance.livesManager.GiveOneLife();
            }

            if (ID.Equals(AdsControl.Instance.adUnityRWUnitId) && callBackState.Equals(UnityAdsShowCompletionState.COMPLETED))
            {
                AdsControl.Instance.LoadUnityAd();
            }

        });
    }
}
