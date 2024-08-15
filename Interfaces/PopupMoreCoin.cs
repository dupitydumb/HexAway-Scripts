using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using GoogleMobileAds.Api;
using static AdsControl;
using UnityEngine.Advertisements;

public class PopupMoreCoin : BasePopup
{
    public Button openShopBtn, moreCoinBtn;

    public override void Start()
    {
        
    }

    public override void Update()
    {
        
    }

    public override void InitView()
    {
        
    }

    public void OpenShop()
    {
        AudioManager.instance.clickSound.Play();
        HideView();
        GameManager.instance.uiManager.shopPopup.ShowView();
    }

    public void MoreCoins()
    {
        AudioManager.instance.clickSound.Play();
        WatchAds();
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
        HideView();
        GameManager.instance.uiManager.coinView.InitView();
        GameManager.instance.uiManager.coinView.ShowView();
        GameManager.instance.AddCoin(50);
    }

    public void ShowRWUnityAds()
    {
        AdsControl.Instance.PlayUnityVideoAd((string ID, UnityAdsShowCompletionState callBackState) =>
        {

            if (ID.Equals(AdsControl.Instance.adUnityRWUnitId) && callBackState.Equals(UnityAdsShowCompletionState.COMPLETED))
            {
                HideView();
                GameManager.instance.uiManager.coinView.InitView();
                GameManager.instance.uiManager.coinView.ShowView();
                GameManager.instance.AddCoin(50);
            }

            if (ID.Equals(AdsControl.Instance.adUnityRWUnitId) && callBackState.Equals(UnityAdsShowCompletionState.COMPLETED))
            {
                AdsControl.Instance.LoadUnityAd();
            }

        });
    }
}
