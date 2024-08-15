using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestItemView : MonoBehaviour
{
    public int itemIndex;

    public Text descriptionTxt;

    public Text progressTxt;

    public Image progressBar;

    public Image questIcon;

    public Text rewardValueTxt;

    public Image doneIcon;

    public void CollectStar()
    {
        GameManager.instance.uiManager.questPopup.ReceiveStar(itemIndex);
    }
}
