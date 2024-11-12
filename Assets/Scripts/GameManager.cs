using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static string playerId = "";
    public static string playerName = "";

    [SerializeField] private TextMeshProUGUI countDownText;
    [SerializeField] private ShakeMove shakeMove;
    [SerializeField] private TimerSlider timerSlider;
    [SerializeField] private Basket basket;
    [SerializeField] private PlayFabController playFabController;
    [SerializeField] private GameObject rankingObj;
    [SerializeField] private BGMManager bgmManager;
    float countDownTime = 3.4f;
    private int score;

    public enum GameStatus
    {
          PREVSTART,  
          COUNTDOWNING, 
          PLAYING,      
          FINISHED, 
    }

    private GameStatus gameStatus;
    // Start is called before the first frame update
    void Start()
    {
        gameStatus = GameStatus.PREVSTART;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameStatus == GameStatus.COUNTDOWNING)
        {
            countDownTime -= Time.deltaTime;
            countDownText.text = countDownTime.ToString("F0");
            if(countDownTime < 0)
            {
                StartGame();
                countDownText.text = "";
            }
        }
    }

    public void StartCountDown()
    {
        gameStatus = GameStatus.COUNTDOWNING;
    }

    void StartGame()
    {
        gameStatus = GameStatus.PLAYING;
        bgmManager.startGameBGM();

        // 動けるようにする
        shakeMove.onCanMove();
        // タイマー開始
        timerSlider.onTimerStart();
    }

    public void finishGame()
    {
        gameStatus = GameStatus.FINISHED;
        // 集計を行う
        score = basket.GetScore();
        bgmManager.stopGameBGM();

        // ここでスコアを送信する
        score = 500;
        Debug.Log("finshiGame: " + score);
        rankingObj.SetActive(true);
        playFabController.SubmitScore(score);
    }

    // 以下二つは名前を保存するかしないか
    public void EndGame()
    {
        //名前を保存しない
        playerId = "";
        playerName = "";
        //シーンをリロード
        SceneManager.LoadScene("UI_Sample_Scene");
    }

    public void RetryGame()
    {
        //シーンをリロード
        SceneManager.LoadScene("UI_Sample_Scene");
    }

}
