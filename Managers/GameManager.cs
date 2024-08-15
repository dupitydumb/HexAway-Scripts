using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public BoardController boardController;

    public BoardGenerator boardGenerator;

    public CellHolder cellHolder;

    public ColorConfig colorConfig;

    public RewardConfig rewardConfig;

    public PoolManager poolManager;

    public UIManager uiManager;

    public LivesManager livesManager;

    public ParticleSystem confetiVfx;

    public ParticleSystem hammerExplosionVfx;

    [HideInInspector]
    public int levelIndex;

    public int levelTest;

    public bool isTestMode;

    public int coinValue;

    public int currentLuckyWheel;

    public int hammerBoosterValue;

    public int moveBoosterValue;

    public int shuffleBoosterValue;

    public enum GAME_STATE
    {
        HOME,
        READY,
        PLAYING,
        SHOW_POPUP,
        GAME_WIN,
        GAME_OVER
    }

    public GAME_STATE currentGameState;

    private void Awake()
    {
        instance = this;
        Application.targetFrameRate = 60;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void LoadGame()
    {
        LoadGameData();
        ShowHome();
    }

    private void LoadGameData()
    {
        if (PlayerPrefs.GetInt("FirstGame") == 0)
        {
            levelIndex = 1;
            coinValue = 50;
            hammerBoosterValue = 1;
            moveBoosterValue = 1;
            shuffleBoosterValue = 1;
            currentLuckyWheel = 1;
            PlayerPrefs.SetInt("FirstGame", 1);
            PlayerPrefs.SetInt("Coin", coinValue);
            PlayerPrefs.SetInt("Hammer", hammerBoosterValue);
            PlayerPrefs.SetInt("Move", moveBoosterValue);
            PlayerPrefs.SetInt("Shuffle", shuffleBoosterValue);
            PlayerPrefs.SetInt("CurrentLevel", levelIndex);
            PlayerPrefs.SetInt("CurrentLuckyWheel", currentLuckyWheel);
        }
        else
        {
            coinValue = PlayerPrefs.GetInt("Coin");
            levelIndex = PlayerPrefs.GetInt("CurrentLevel");
            hammerBoosterValue = PlayerPrefs.GetInt("Hammer");
            moveBoosterValue = PlayerPrefs.GetInt("Move");
            shuffleBoosterValue = PlayerPrefs.GetInt("Shuffle");
            currentLuckyWheel = PlayerPrefs.GetInt("CurrentLuckyWheel");
        }

        if (isTestMode)
            levelIndex = levelTest;
    }

    private void ShowHome()
    {
        currentGameState = GAME_STATE.HOME;
        poolManager.InitPool();
        uiManager.homeView.InitView();
        uiManager.homeView.ShowView();
        uiManager.coinView.InitView();
        uiManager.coinView.ShowView();
        uiManager.coinView.coinContent.gameObject.SetActive(true);
        uiManager.coinView.lifeContent.gameObject.SetActive(true);
    }

    private void InitGame()
    {
        currentGameState = GAME_STATE.READY;
        boardGenerator.InitBoardGenerator();
        boardController.InitBoardController();
        uiManager.homeView.HideView();
        uiManager.coinView.ShowView();
        uiManager.coinView.coinContent.gameObject.SetActive(true);
        uiManager.coinView.lifeContent.gameObject.SetActive(false);
        uiManager.gameView.InitView();
        uiManager.gameView.ShowView();
        uiManager.startGoalPanel.InitView();
        uiManager.startGoalPanel.ShowView();
    }

    public void PlayGame()
    {
        InitGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            AddCoin(1000);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            uiManager.questGiftReward.SetModel(rewardConfig.rewardQuest3);
            uiManager.questGiftReward.InitView();
            uiManager.questGiftReward.ShowView();
        }
    }

    public void ShowGameWin()
    {
        currentGameState = GAME_STATE.GAME_WIN;
        AudioManager.instance.winSound.Play();
        cellHolder.ClearCellHolder();
        boardController.ClearBoard();
        boardController.RotateAnim();
        uiManager.gameView.HideView();
        confetiVfx.Play();
        StartCoroutine(ShowGameWinIE());
    }

    public void ShowGameLose()
    {
        currentGameState = GAME_STATE.GAME_OVER;
        AudioManager.instance.winSound.Play();
        StartCoroutine(ShowGameLoseIE());
    }

    IEnumerator ShowGameWinIE()
    {
        yield return new WaitForSeconds(2.0f);
        uiManager.popupWin.InitView();
        uiManager.popupWin.ShowView();

        if (!isTestMode)
        {
            levelIndex++;

            if (currentLuckyWheel < 5)
                currentLuckyWheel++;

            PlayerPrefs.SetInt("CurrentLuckyWheel", currentLuckyWheel);
            PlayerPrefs.SetInt("CurrentLevel", levelIndex);
        }

        uiManager.questPopup.IncreaseProgressQuest(0, 1);
        uiManager.questPopup.IncreaseProgressQuest(4, 1);
    }


    IEnumerator ShowGameLoseIE()
    {
        yield return new WaitForSeconds(1.0f);
        uiManager.popupLose.InitView();
        uiManager.popupLose.ShowView();
    }

    public void Replay()
    {
        uiManager.popupWin.HideView();
        InitGame();
    }

    public void NextLevel()
    {
        uiManager.popupWin.HideView();
        InitGame();
    }

    public void BackToHome()
    {
        currentGameState = GAME_STATE.HOME;
        cellHolder.ClearCellHolder();
        //boardController.ClearBoard();
        boardGenerator.ClearMap();
        uiManager.gameView.DisableArrow();
        uiManager.gameView.HideView();
        uiManager.homeView.InitView();
        uiManager.homeView.ShowView();
        uiManager.coinView.InitView();
        uiManager.coinView.ShowView();
        uiManager.coinView.coinContent.gameObject.SetActive(true);
        uiManager.coinView.lifeContent.gameObject.SetActive(true);
    }

    public void AddCoin(int moreCoin)
    {
        StartCoroutine(AddCoinIE(moreCoin));
    }

    public void SubCoin(int subCoin)
    {
        coinValue -= subCoin;
        PlayerPrefs.SetInt("Coin", coinValue);
        uiManager.coinView.UpdateCoinTxt();
    }

    IEnumerator AddCoinIE(int moreCoin)
    {
        AudioManager.instance.coinFlySound.Play();
        uiManager.coinView.SpawnCoin(Vector3.zero - new Vector3(0.0f, 10.0f, 0.0f));
        yield return new WaitForSeconds(0.75f);
        AudioManager.instance.coinFlySound.Stop();
        AudioManager.instance.coinCollectSound.Play();
        coinValue += moreCoin;
        PlayerPrefs.SetInt("Coin", coinValue);
        uiManager.questPopup.IncreaseProgressQuest(1, moreCoin);
        uiManager.questPopup.IncreaseProgressQuest(3, moreCoin);
        uiManager.coinView.UpdateCoinTxt();
    }

    public void AddHammerBooster(int moreValue)
    {
        hammerBoosterValue += moreValue;
        PlayerPrefs.SetInt("Hammer", hammerBoosterValue);
    }

    public void AddMoveBooster(int moreValue)
    {
        moveBoosterValue += moreValue;
        PlayerPrefs.SetInt("Move", moveBoosterValue);
    }

    public void AddShuffleBooster(int moreValue)
    {
        shuffleBoosterValue += moreValue;
        PlayerPrefs.SetInt("Shuffle", shuffleBoosterValue);
    }
}
