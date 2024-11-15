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
    [SerializeField] private Basket1 basket;
    [SerializeField] private PlayFabController playFabController;
    [SerializeField] private GameObject rankingObj;
    [SerializeField] private BGMManager bgmManager;
    [SerializeField] private AudioManager audioManager;
    
    private float countDownTime = 3.4f;
    private int score;
    private GameStatus gameStatus;

    public enum GameStatus
    {
        PREVSTART,  
        COUNTDOWNING, 
        PLAYING,      
        FINISHED, 
    }

    void Start()
    {
        gameStatus = GameStatus.PREVSTART;
        countDownText.text = "";
        rankingObj.SetActive(false); // ランキングオブジェクトを初期化時に非表示にする
    }

    void Update()
    {
        HandleCountDown();
    }

    // カウントダウンを管理
    private void HandleCountDown()
    {
        if (gameStatus == GameStatus.COUNTDOWNING)
        {
            countDownTime -= Time.deltaTime;
            countDownText.text = Mathf.Ceil(countDownTime).ToString(); // 小数点以下を切り捨てたカウントダウン
            if (countDownTime <= 0)
            {
                StartGame();
                countDownText.text = ""; // カウントダウンテキストを消す
            }
        }
    }

    // カウントダウンの開始
    public void StartCountDown()
    {
        gameStatus = GameStatus.COUNTDOWNING;
        countDownTime = 3.4f; // カウントダウンの初期化
        countDownText.text = Mathf.Ceil(countDownTime).ToString();
    }

    // ゲーム開始の処理
    private void StartGame()
    {
        gameStatus = GameStatus.PLAYING;
        bgmManager.startGameBGM();

        if (shakeMove != null)
        {
            shakeMove.onCanMove();
        }
        if (timerSlider != null)
        {
            timerSlider.onTimerStart();
        }
    }

    // ゲーム終了の処理
    public void finishGame()
    {
        gameStatus = GameStatus.FINISHED;
        audioManager.playHakusyuSound(); // 終了時に効果音を再生
        

        if (basket != null)
        {
            score = basket.GetScore();
        }
        else
        {
            Debug.LogWarning("Basket is not assigned.");
            score = 0; // 予期しない場合でもスコアが0になるように
        }

        bgmManager.stopGameBGM();
        
        Debug.Log("finishGame: " + score);
        
        if (rankingObj != null)
        {
            rankingObj.SetActive(true);
        }

        if (playFabController != null)
        {
            playFabController.SubmitScore(score);
        }
        else
        {
            Debug.LogWarning("PlayFabController is not assigned.");
        }
    }

    // 名前を保存しないでゲームを終了
    public void EndGame()
    {
        playerId = "";
        playerName = "";
        SceneManager.LoadScene("UI_Sample_Scene");
    }

    // ゲームを再試行
    public void RetryGame()
    {
        SceneManager.LoadScene("UI_Sample_Scene");
    }
}

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using TMPro;
// using UnityEngine.SceneManagement;

// public class GameManager : MonoBehaviour
// {
//     public static string playerId = "";
//     public static string playerName = "";

//     [SerializeField] private TextMeshProUGUI countDownText;
//     [SerializeField] private ShakeMove shakeMove;
//     [SerializeField] private TimerSlider timerSlider;
//     [SerializeField] private Basket basket;
//     [SerializeField] private PlayFabController playFabController;
//     [SerializeField] private GameObject rankingObj;
//     [SerializeField] private BGMManager bgmManager;
//     float countDownTime = 3.4f;
//     private int score;

//     public enum GameStatus
//     {
//           PREVSTART,  
//           COUNTDOWNING, 
//           PLAYING,      
//           FINISHED, 
//     }

//     private GameStatus gameStatus;
//     // Start is called before the first frame update
//     void Start()
//     {
//         gameStatus = GameStatus.PREVSTART;
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         if(gameStatus == GameStatus.COUNTDOWNING)
//         {
//             countDownTime -= Time.deltaTime;
//             countDownText.text = countDownTime.ToString("F0");
//             if(countDownTime < 0)
//             {
//                 StartGame();
//                 countDownText.text = "";
//             }
//         }
//     }

//     public void StartCountDown()
//     {
//         gameStatus = GameStatus.COUNTDOWNING;
//     }

//     void StartGame()
//     {
//         gameStatus = GameStatus.PLAYING;
//         bgmManager.startGameBGM();

//         // 動けるようにする
//         shakeMove.onCanMove();
//         // タイマー開始
//         timerSlider.onTimerStart();
//     }

//     public void finishGame()
//     {
//         gameStatus = GameStatus.FINISHED;
//         // 集計を行う
//         score = basket.GetScore();
//         bgmManager.stopGameBGM();

//         // ここでスコアを送信する
//         score = 500;
//         Debug.Log("finshiGame: " + score);
//         rankingObj.SetActive(true);
//         playFabController.SubmitScore(score);
//     }

//     // 以下二つは名前を保存するかしないか
//     public void EndGame()
//     {
//         //名前を保存しない
//         playerId = "";
//         playerName = "";
//         //シーンをリロード
//         SceneManager.LoadScene("UI_Sample_Scene");
//     }

//     public void RetryGame()
//     {
//         //シーンをリロード
//         SceneManager.LoadScene("UI_Sample_Scene");
//     }

// }