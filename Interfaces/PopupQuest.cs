using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupQuest : BasePopup
{
    public RectTransform scrollViewRoot;

    public Text textEndQuest;

    public Image imageGiftProgress;

    public Transform starFlyTargetPoint;

    public Transform adStarSpawnPoint;

    public Transform prefab_star;

    public List<QuestItemView> questItemViewList;

    public List<QuestItemData> questItemDataList;

    public Sprite[] questIconList;

    public QuestItemView questItemObj;

    public bool isLoadElements;

    public int currentStar;

    public Image unlockGiftProgress;

    public override void InitView()
    {
        if (!isLoadElements)
            LoadElements();

        LoadData();
        LoadView();
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

    public void LoadElements()
    {
        isLoadElements = true;
        questItemViewList.Add(questItemObj);

        for (int i = 0; i < questItemDataList.Count - 1; i++)
        {
            QuestItemView questObj = Instantiate(questItemObj);
            questObj.transform.parent = scrollViewRoot;
            questObj.transform.localPosition = Vector3.zero;
            questObj.transform.localScale = Vector3.one;
            questItemViewList.Add(questObj);
        }
    }

    public void LoadView()
    {

        for (int i = 0; i < questItemDataList.Count; i++)
        {
            questItemViewList[i].itemIndex = i;

            if (questItemDataList[i].typeOfQuest == QuestItemData.TYPE_QUEST.LEVELS)
            {
                questItemViewList[i].questIcon.sprite = questIconList[0];
                questItemViewList[i].descriptionTxt.text = "Complete " + questItemDataList[i].progressMax + " levels";
            }
            else if (questItemDataList[i].typeOfQuest == QuestItemData.TYPE_QUEST.USE_WHEEL)
            {
                questItemViewList[i].questIcon.sprite = questIconList[1];
                questItemViewList[i].descriptionTxt.text = "Use Wheel " + questItemDataList[i].progressMax + " times";
            }
            else if (questItemDataList[i].typeOfQuest == QuestItemData.TYPE_QUEST.COINS)
            {
                questItemViewList[i].questIcon.sprite = questIconList[2];
                questItemViewList[i].descriptionTxt.text = "Collect " + questItemDataList[i].progressMax + " coins";
            }

            if (questItemDataList[i].currentProgress < questItemDataList[i].progressMax)
            {
                questItemViewList[i].progressTxt.text = questItemDataList[i].currentProgress + "/" + questItemDataList[i].progressMax;
                questItemViewList[i].progressBar.fillAmount = (float)questItemDataList[i].currentProgress / (float)questItemDataList[i].progressMax;

            }
            else
            {
                questItemViewList[i].progressTxt.text = questItemDataList[i].progressMax + "/" + questItemDataList[i].progressMax;
                questItemViewList[i].progressBar.fillAmount = 1.0f;

            }
            questItemViewList[i].rewardValueTxt.text = questItemDataList[i].rewardValue.ToString();

            if (questItemDataList[i].isFinish == 1)
            {
                questItemViewList[i].doneIcon.gameObject.SetActive(true);
            }
        }

        if (currentStar < 50)
            unlockGiftProgress.fillAmount = (float)currentStar / 50.0f;
        else
            unlockGiftProgress.fillAmount = 1.0f;
    }

    public void LoadData()
    {
        currentStar = PlayerPrefs.GetInt("StarQuest");

        for (int i = 0; i < questItemDataList.Count; i++)
        {
            questItemDataList[i].questIndex = i;
            questItemDataList[i].isFinish = PlayerPrefs.GetInt("IsFinishQuest" + i.ToString());
            questItemDataList[i].currentProgress = PlayerPrefs.GetInt("QuestProgress" + i.ToString());
        }
    }

    public void IncreaseProgressQuest(int questIndex, int moreValue)
    {
        questItemDataList[questIndex].currentProgress += moreValue;
        PlayerPrefs.SetInt("QuestProgress" + questIndex.ToString(), questItemDataList[questIndex].currentProgress);
    }

    public void ReceiveStar(int cellIndex)
    {
        if(questItemDataList[cellIndex].currentProgress >= questItemDataList[cellIndex].progressMax && questItemDataList[cellIndex].isFinish == 0)
        {
            currentStar += questItemDataList[cellIndex].rewardValue;
            PlayerPrefs.SetInt("StarQuest", currentStar);
            PlayerPrefs.SetInt("IsFinishQuest" + cellIndex.ToString(), 1);

            LoadData();
            LoadView();
            AudioManager.instance.coinCollectSound.Play();
            Debug.Log("Collect Star");
        }
        else
        {
            Debug.Log("Not enough");
        }
    }

    public void ClaimGift(int giftIndex)
    {
        HideView();
        if(currentStar >= 15 && giftIndex == 0 && PlayerPrefs.GetInt("Gift1") == 0)
        {
            PlayerPrefs.SetInt("Gift1", 1);
            GameManager.instance.uiManager.questGiftReward.SetModel(GameManager.instance.rewardConfig.rewardQuest1);
            GameManager.instance.uiManager.questGiftReward.InitView();
            GameManager.instance.uiManager.questGiftReward.ShowView();
        }
        else if (currentStar >= 30 && giftIndex == 1 && PlayerPrefs.GetInt("Gift2") == 0)
        {
            PlayerPrefs.SetInt("Gift2", 1);
            GameManager.instance.uiManager.questGiftReward.SetModel(GameManager.instance.rewardConfig.rewardQuest2);
            GameManager.instance.uiManager.questGiftReward.InitView();
            GameManager.instance.uiManager.questGiftReward.ShowView();
        }

        else if (currentStar >= 50 && giftIndex == 2 && PlayerPrefs.GetInt("Gift3") == 0)
        {
            PlayerPrefs.SetInt("Gift3", 1);
            GameManager.instance.uiManager.questGiftReward.SetModel(GameManager.instance.rewardConfig.rewardQuest3);
            GameManager.instance.uiManager.questGiftReward.InitView();
            GameManager.instance.uiManager.questGiftReward.ShowView();
        }
    }
}


[Serializable]
public class QuestItemData
{
    public enum TYPE_QUEST
    {
        LEVELS,
        COINS,
        USE_WHEEL
    }

    public TYPE_QUEST typeOfQuest;

    public int questIndex;

    public int currentProgress;

    public int progressMax;

    public int rewardValue;

    public int isFinish;
}