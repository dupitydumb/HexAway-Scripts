using System;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using static AdsControl;

public class DailyItemView : MonoBehaviour
{
    public GameObject collectBtn;

    public enum REWARD_TYPE
    {
        GOLD,
        LIVES,
        HAMMER,
        MOVE,
        SHUFFLE
    }

    public REWARD_TYPE currentRewardType;

    public GameObject doneImage;

    public Text rewardvalueTxt;

    public int rewardValue;

    public int itemIndex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Collect()
    {
        long dailyKey = (long)(DateTime.Today.Subtract(new DateTime(2019, 1, 1))).TotalSeconds;
        PlayerPrefs.SetInt("Daily" + dailyKey.ToString() + itemIndex.ToString(), 1);
        GetReward();
        DisableItem();
    }

    public void CollectAds()
    {
        WatchAds();
    }

    public void InitItem()
    {
        if(currentRewardType == REWARD_TYPE.LIVES)
            rewardvalueTxt.text = "+ " + rewardValue.ToString() + " s";
        else
            rewardvalueTxt.text = rewardValue.ToString();
    }

    public void EnableItem()
    {
        collectBtn.SetActive(true);
        doneImage.SetActive(false);
    }

    public void DisableItem()
    {
        collectBtn.SetActive(false);
        doneImage.SetActive(true);
    }

    private void GetReward()
    {
        if (currentRewardType == REWARD_TYPE.GOLD)
        {
            GameManager.instance.AddCoin(rewardValue);
        }

        if (currentRewardType == REWARD_TYPE.HAMMER)
        {
            GameManager.instance.AddHammerBooster(rewardValue);
        }

        if (currentRewardType == REWARD_TYPE.MOVE)
        {
            GameManager.instance.AddMoveBooster(rewardValue);
        }

        if (currentRewardType == REWARD_TYPE.SHUFFLE)
        {
            GameManager.instance.AddShuffleBooster(rewardValue);
        }

        if (currentRewardType == REWARD_TYPE.LIVES)
        {
            GameManager.instance.livesManager.GiveInifinite(10);
        }
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
        AudioManager.instance.rewardDone.Play();
        long dailyKey = (long)(DateTime.Today.Subtract(new DateTime(2019, 1, 1))).TotalSeconds;
        PlayerPrefs.SetInt("Daily" + dailyKey.ToString() + itemIndex.ToString(), 1);
        GetReward();
        DisableItem();
    }

    public void ShowRWUnityAds()
    {
        AdsControl.Instance.PlayUnityVideoAd((string ID, UnityAdsShowCompletionState callBackState) =>
        {

            if (ID.Equals(AdsControl.Instance.adUnityRWUnitId) && callBackState.Equals(UnityAdsShowCompletionState.COMPLETED))
            {
                AudioManager.instance.rewardDone.Play();
                long dailyKey = (long)(DateTime.Today.Subtract(new DateTime(2019, 1, 1))).TotalSeconds;
                PlayerPrefs.SetInt("Daily" + dailyKey.ToString() + itemIndex.ToString(), 1);
                GetReward();
                DisableItem();
            }

            if (ID.Equals(AdsControl.Instance.adUnityRWUnitId) && callBackState.Equals(UnityAdsShowCompletionState.COMPLETED))
            {
                AdsControl.Instance.LoadUnityAd();
            }

        });
    }
}
