using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
[CreateAssetMenu(fileName = "RewardConfig")]

public class RewardConfig : ScriptableObject
{
    public List<RewardModel> rewardQuest1;

    public List<RewardModel> rewardQuest2;

    public List<RewardModel> rewardQuest3;
}

[System.Serializable]
public class RewardModel
{
    public enum RewardType
    {
        COIN,
        HAMMER,
        MOVE,
        SHUFFLE,
        LIVE
    }

    public RewardType rewardType;

    public int rewardValue;

}