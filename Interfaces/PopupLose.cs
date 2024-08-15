using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using GoogleMobileAds.Api;
using UnityEngine;
using static AdsControl;
using UnityEngine.Advertisements;

public class PopupLose : BasePopup
{
    public Transform offerTrans;

    public override void InitView()
    {
       
    }

    public override void Start()
    {
       
    }

    public override void Update()
    {
       
    }

    public override void ShowView()
    {
        canvasGroup.alpha = 1.0f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        isShow = true;
        contentGroup.alpha = 0.0f;
        rootTrans.localScale = Vector3.one * 0.35f;
        offerTrans.localScale = Vector3.zero;

        rootTrans.DOScale(Vector3.one, 0.25f).SetEase(Ease.OutBounce).OnComplete(() => {

            DOTween.To(() => contentGroup.alpha, x => contentGroup.alpha = x, 1.0f, 0.5f).SetDelay(0.35f).SetEase(Ease.Linear)
            .OnComplete(() => {

                offerTrans.localScale = Vector3.one * 0.35f;
                offerTrans.DOScale(Vector3.one, 0.25f).SetEase(Ease.OutBounce).OnComplete(() => {
                  

                });

            });

        });
    }

    public override void HideView()
    {
        base.HideView();
    }

    public void GoToHome()
    {
        AudioManager.instance.clickSound.Play();
        HideView();
        GameManager.instance.livesManager.ConsumeLife();
        GameManager.instance.BackToHome();
    }

    private void Retrive()
    {
        HideView();
        GameManager.instance.currentGameState = GameManager.GAME_STATE.PLAYING;
        GameManager.instance.boardController.DestroyThreeColums();
        GameManager.instance.cellHolder.ShuffleHolder();
    }

    public void RetriveByCoin()
    {
        AudioManager.instance.clickSound.Play();
        if (GameManager.instance.coinValue >= 100)
        {
            Retrive();
        }
        
    }

    public void RetriveByAds()
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
        Retrive();
    }

    public void ShowRWUnityAds()
    {
        AdsControl.Instance.PlayUnityVideoAd((string ID, UnityAdsShowCompletionState callBackState) =>
        {

            if (ID.Equals(AdsControl.Instance.adUnityRWUnitId) && callBackState.Equals(UnityAdsShowCompletionState.COMPLETED))
            {
                Retrive();
            }

            if (ID.Equals(AdsControl.Instance.adUnityRWUnitId) && callBackState.Equals(UnityAdsShowCompletionState.COMPLETED))
            {
                AdsControl.Instance.LoadUnityAd();
            }

        });
    }

    public void BuySpecialPack()
    {
        AudioManager.instance.clickSound.Play();
        GameManager.instance.uiManager.shopPopup.BuyIAPPackage(Config.IAPPackageID.special_offer);
    }
}
