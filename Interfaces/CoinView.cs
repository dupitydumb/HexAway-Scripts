using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CoinView : BaseView
{
    public Canvas uiCanvas;

    public RectTransform coinContent;

    public RectTransform lifeContent;

    public GameObject coinPrefab;

    public RectTransform targetRoot;

    public RectTransform coinImage;

    private RectTransform coinObj1, coinObj2, coinObj3;

    public Text coinTxt;

    public Text heartTxt;

    public Text heartFullTxt;

    public GameObject addHeart;

    public override void InitView()
    {
        GameObject obj1 = Instantiate(coinPrefab) as GameObject;
        obj1.SetActive(true);
        coinObj1 = obj1.GetComponent<RectTransform>();
        coinObj1.SetParent(transform);
        coinObj1.localScale = Vector3.one;


        GameObject obj2 = Instantiate(coinPrefab) as GameObject;
        obj2.SetActive(true);
        coinObj2 = obj2.GetComponent<RectTransform>();
        coinObj2.SetParent(transform);
        coinObj2.localScale = Vector3.one;

        GameObject obj3 = Instantiate(coinPrefab) as GameObject;
        obj3.SetActive(true);
        coinObj3 = obj3.GetComponent<RectTransform>();
        coinObj3.SetParent(transform);
        coinObj3.localScale = Vector3.one;


        coinObj1.gameObject.SetActive(false);
        coinObj2.gameObject.SetActive(false);
        coinObj3.gameObject.SetActive(false);

        UpdateCoinTxt();
    }

    public override void Start()
    {
        
    }

    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
            OnButtonConsumePressed();
    }

    public void MoreCoin()
    {
        AudioManager.instance.clickSound.Play();
        GameManager.instance.uiManager.shopPopup.InitView();
        GameManager.instance.uiManager.shopPopup.ShowView();
       // Debug.Log("MORE COINS");
    }

    public void ShowFillLives()
    {
        AudioManager.instance.clickSound.Play();
        GameManager.instance.uiManager.fillLivesPopup.InitView();
        GameManager.instance.uiManager.fillLivesPopup.ShowView();
    }

    public void OnLivesChanged()
    {
///        Debug.Log("Update Lives : " + GameManager.instance.livesManager.LivesText);
        heartTxt.text = GameManager.instance.livesManager.LivesText;
    }

    public void OnTimeToNextLifeChanged()
    {
        heartFullTxt.text = GameManager.instance.livesManager.RemainingTimeString;
        GameManager.instance.uiManager.fillLivesPopup.textTime.text = GameManager.instance.livesManager.RemainingTimeString;
    }

    public void UpdateCoinTxt()
    {
        coinTxt.text = GameManager.instance.coinValue.ToString();
    }

    List<Vector3> arcPoint = new List<Vector3>();

    Vector3 midlePoint;

    public void SpawnCoin(Vector3 spawnPos)
    {
        if (!coinObj3.gameObject.activeInHierarchy)
        {
            coinObj1.gameObject.SetActive(true);
            coinObj2.gameObject.SetActive(true);
            coinObj3.gameObject.SetActive(true);
            spawnPos = new Vector3(spawnPos.x, spawnPos.y, spawnPos.z) - Camera.main.transform.forward;
            coinObj1.position = WorldToCanvasPosition(uiCanvas, coinObj1, Camera.main, spawnPos);
            coinObj2.position = WorldToCanvasPosition(uiCanvas, coinObj2, Camera.main, spawnPos);
            coinObj3.position = WorldToCanvasPosition(uiCanvas, coinObj3, Camera.main, spawnPos);

            arcPoint.Clear();
            midlePoint = BetweenP(coinObj1.localPosition, targetRoot.localPosition, 0.5f);

            for (int i = 0; i < 10; i++)
            {
                arcPoint.Add(SampleParabola(coinObj1.localPosition, midlePoint, -100.0f, (float)i / 9.0f));
            }

            for (int i = 0; i < 10; i++)
            {
                arcPoint.Add(SampleParabola(midlePoint, targetRoot.localPosition, 100.0f, (float)i / 9.0f));
            }

            coinObj1.DOLocalPath(arcPoint.ToArray(), 0.75f, PathType.Linear).SetLoops(1).SetEase(Ease.Linear).OnComplete(() =>
            {
                coinObj1.gameObject.SetActive(false);
            });

            coinObj2.DOLocalPath(arcPoint.ToArray(), 0.75f, PathType.Linear).SetLoops(1).SetDelay(0.1f).SetEase(Ease.Linear).OnComplete(() =>
            {
                coinObj2.gameObject.SetActive(false);
            });

            coinObj3.DOLocalPath(arcPoint.ToArray(), 0.75f, PathType.Linear).SetLoops(1).SetDelay(0.2f).SetEase(Ease.Linear).OnComplete(() =>
            {
                coinObj3.gameObject.SetActive(false);
            });

            coinImage.DOScale(Vector3.one * 1.25f, 0.25f).SetDelay(0.75f).SetEase(Ease.OutBounce).OnComplete(() =>
            {
                coinImage.DOScale(Vector3.one, 0.25f).SetEase(Ease.OutBounce);
            });
        }
       
    }

    private Vector2 WorldToCanvasPosition(Canvas canvas, RectTransform canvasRect, Camera camera, Vector3 position)
    {
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(camera, position);
        return screenPoint;
    }

    Vector3 BetweenP(Vector3 start, Vector3 end, float percent)
    {
        return (start + percent * (end - start));
    }

    Vector3 SampleParabola(Vector3 start, Vector3 end, float height, float t)
    {
        if (Mathf.Abs(start.y - end.y) < 0.1f)
        {
            //start and end are roughly level, pretend they are - simpler solution with less steps
            Vector3 travelDirection = end - start;
            Vector3 result = start + t * travelDirection;
            result.y += Mathf.Sin(t * Mathf.PI) * height;
            return result;
        }
        else
        {
            //start and end are not level, gets more complicated
            Vector3 travelDirection = end - start;
            Vector3 levelDirecteion = end - new Vector3(start.x, end.y, start.z);
            Vector3 right = Vector3.Cross(travelDirection, levelDirecteion);
            Vector3 up = Vector3.Cross(right, travelDirection);
            if (end.y > start.y) up = -up;
            Vector3 result = start + t * travelDirection;
            result += (Mathf.Sin(t * Mathf.PI) * height) * up.normalized;
            return result;
        }
    }


    public void OnButtonConsumePressed()
    {
        if (GameManager.instance.livesManager.ConsumeLife())
        {
            // Go to your game!
            Debug.Log("A life was consumed and the player can continue!");
            //ResultDisplay.Show(true);
        }
        else
        {
            // Tell player to buy lives, then:
            // LivesManager.GiveOneLife();
            // or
            // LivesManager.FillLives();
            Debug.Log("Not enough lives to play!");
            // ResultDisplay.Show(false);
        }
    }

    public void OnButtonGiveOnePressed()
    {
        GameManager.instance.livesManager.GiveOneLife();
    }

    public void OnButtonFillPressed()
    {
        GameManager.instance.livesManager.FillLives();
    }

    public void OnButtonInfinitePressed()
    {
        GameManager.instance.livesManager.GiveInifinite(30);
    }

    public void OnButtonIncreaseMaxPressed()
    {
        GameManager.instance.livesManager.AddLifeSlots(1);
        Debug.LogFormat("Max lives is now {0}", GameManager.instance.livesManager.MaxLives);
    }

    public void OnButtonResetPressed()
    {
        GameManager.instance.livesManager.ResetPlayerPrefs();
        Debug.LogFormat("Max lives is now {0}", GameManager.instance.livesManager.MaxLives);
        OnLivesChanged();
        OnTimeToNextLifeChanged();
    }

}
