using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupQuestGiftReward : BasePopup
{
    public GameObject goHammer;

    public GameObject goSwap;

    public GameObject goCoin;

    public GameObject goShuffle;

    public GameObject goHeart;

    public Text textHammer;

    public Text textSwap;

    public Text textCoin;

    public Text textShuffle;

    public Text textHeart;

    public List<RewardModel> currentRewardModel;

    public void SetModel(List<RewardModel> rewardModel)
    {
        currentRewardModel = rewardModel;
    }

    public override void InitView()
    {
        for (int i = 0; i < currentRewardModel.Count; i++)
        {
            if (currentRewardModel[i].rewardType == RewardModel.RewardType.COIN)
            {
                goCoin.SetActive(true);
                textCoin.text = currentRewardModel[i].rewardValue.ToString();
            }

            if (currentRewardModel[i].rewardType == RewardModel.RewardType.HAMMER)
            {
                goHammer.SetActive(true);
                textHammer.text = currentRewardModel[i].rewardValue.ToString();
            }

            if (currentRewardModel[i].rewardType == RewardModel.RewardType.MOVE)
            {
                goSwap.SetActive(true);
                textSwap.text = currentRewardModel[i].rewardValue.ToString();
            }

            if (currentRewardModel[i].rewardType == RewardModel.RewardType.SHUFFLE)
            {
                goShuffle.SetActive(true);
                textShuffle.text = currentRewardModel[i].rewardValue.ToString();
            }

            if (currentRewardModel[i].rewardType == RewardModel.RewardType.LIVE)
            {
                goHeart.SetActive(true);
                textHeart.text = currentRewardModel[i].rewardValue.ToString();
            }
        }
    }

    public override void ShowView()
    {
        base.ShowView();
        AudioManager.instance.unlockCell.Play();
    }

    public override void Start()
    {

    }

    public override void Update()
    {

    }

    public void ClaimReward()
    {
        HideView();
        AudioManager.instance.rewardDone.Play();

        for (int i = 0; i < currentRewardModel.Count; i++)
        {
            if (currentRewardModel[i].rewardType == RewardModel.RewardType.COIN)
            {
                GameManager.instance.AddCoin(currentRewardModel[i].rewardValue);
            }

            if (currentRewardModel[i].rewardType == RewardModel.RewardType.HAMMER)
            {
                GameManager.instance.AddHammerBooster(currentRewardModel[i].rewardValue);
            }

            if (currentRewardModel[i].rewardType == RewardModel.RewardType.MOVE)
            {
                GameManager.instance.AddMoveBooster(currentRewardModel[i].rewardValue);
            }

            if (currentRewardModel[i].rewardType == RewardModel.RewardType.SHUFFLE)
            {
                GameManager.instance.AddShuffleBooster(currentRewardModel[i].rewardValue);
            }

            if (currentRewardModel[i].rewardType == RewardModel.RewardType.LIVE)
            {
                GameManager.instance.livesManager.FillLives();
            }
        }
    }
}
