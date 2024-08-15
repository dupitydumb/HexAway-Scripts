using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public abstract class BasePopup : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    public CanvasGroup contentGroup;

    public RectTransform rootTrans;

    [HideInInspector]
    public bool isShow;

    private GameManager.GAME_STATE lastState;

    // Start is called before the first frame update
    public abstract void Start();


    // Update is called once per frame
    public abstract void Update();

    public abstract void InitView();

    public virtual void ShowView()
    {

        //GameManager.instance.uiManager.coinView.HideView();
        lastState = GameManager.instance.currentGameState;
        GameManager.instance.currentGameState = GameManager.GAME_STATE.SHOW_POPUP;
        canvasGroup.alpha = 1.0f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        isShow = true;
        rootTrans.localScale = Vector3.one * 0.55f;


        rootTrans.DOScale(Vector3.one, 0.25f).SetEase(Ease.OutBounce).OnComplete(() => {

        });
    }

    public virtual void ShowView(string content)
    {
        ShowView();
    }

    public virtual void HideView()
    {
        GameManager.instance.currentGameState = lastState;
        AudioManager.instance.clickSound.Play();
        rootTrans.DOScale(Vector3.one * 1.15f, 0.05f).SetEase(Ease.OutQuart).OnComplete(() => {


            rootTrans.DOScale(Vector3.one * 0.85f, 0.15f).SetEase(Ease.Linear).OnComplete(() => {

                canvasGroup.alpha = 0.0f;
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
                isShow = false;
                //GameManager.instance.uiManager.coinView.ShowView();
            });

        });
    }
}
