using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class StartGoalPanel : BasePopup
{
    public Text levelTxt;

    public Text goalTxt;

    public override void InitView()
    {
        levelTxt.text = "Level " + GameManager.instance.levelIndex.ToString();
        goalTxt.text = GameManager.instance.boardGenerator.levelConfig.Goals[0].Target.ToString();
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
        rootTrans.localScale = Vector3.one * 0.15f;
        rootTrans.DOScale(Vector3.one, 1.0f).SetEase(Ease.OutBounce).OnComplete(() =>
        {
            HideView();

        });
    }

    public override void HideView()
    {
        rootTrans.DOScale(Vector3.one * 0.5f, 0.25f).SetDelay(1.0f).SetEase(Ease.Linear).OnComplete(() =>
        {
            canvasGroup.alpha = 0.0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            isShow = false;
            GameManager.instance.currentGameState = GameManager.GAME_STATE.PLAYING;
        });
    }
}
