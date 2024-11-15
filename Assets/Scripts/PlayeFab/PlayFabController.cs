using PlayFab;
using PlayFab.ClientModels;
using System; //DateTimeを使用する為追加。
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFabController : MonoBehaviour
{
    // [SerializeField] private GameObject gameManager;
    const string STATISTICS_NAME = "HighScore";
    [SerializeField] private RankingDisplay rankingDisplay;

    void Start()
    {
        
    }

    public void LogInPlayFab(string userName)
    {
        // 日付と時刻をIDにする
        if(GameManager.playerId == "")
        {
            GameManager.playerId = DateTime.Now.ToString();
        }

        PlayFabClientAPI.LoginWithCustomID(
            new LoginWithCustomIDRequest { CustomId = GameManager.playerId, CreateAccount = true},
            result => {
                Debug.Log("ログイン成功！");
                SetUserName(userName);
            },
            error => Debug.Log("ログイン失敗"));
    }
    private void SetUserName(string userName)
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = userName
        };

        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnSuccess, OnError);

        void OnSuccess(UpdateUserTitleDisplayNameResult result)
        {
            Debug.Log("success!");
        }

        void OnError(PlayFabError error)
        {
            Debug.Log($"{error.Error}");
        }
    }

    void Update()
    {
    }

    // スコア送信後にランキングを取得する
    public void SubmitScore(int playerScore)
    {
        PlayFabClientAPI.UpdatePlayerStatistics(
            new UpdatePlayerStatisticsRequest
            {
                Statistics = new List<StatisticUpdate>()
                {
                    new StatisticUpdate
                    {
                        StatisticName = STATISTICS_NAME,
                        Value = playerScore
                    }
                }
            },
            result =>
            {
                Debug.Log("スコア送信完了です");
                StartCoroutine(DelayCoroutine(3, () =>
                {
                   RequestLeaderBoard();
                   RequestAroundRanking();
                }));

                
            },
            error =>
            {
                Debug.Log(error.GenerateErrorReport());
                RequestLeaderBoard();
                RequestAroundRanking();
            }
        );
    }
    public void RequestLeaderBoard()
    {
        PlayFabClientAPI.GetLeaderboard(
            new GetLeaderboardRequest
            {
                StatisticName = STATISTICS_NAME,
                StartPosition = 0,
                MaxResultsCount = 3
            },
            result =>
            {
                result.Leaderboard.ForEach(
                    x => {
                        rankingDisplay.setRanking(x.Position, x.DisplayName, x.StatValue.ToString());
                    }
                );
            },
            error =>
            {
                Debug.Log(error.GenerateErrorReport());
            }
            );
    }


    // 自分の周りのランキングを取得する countが1のため、自分のランキングのみを取得する
    private void RequestAroundRanking()
    {
        var request = new GetLeaderboardAroundPlayerRequest
        {
            StatisticName = STATISTICS_NAME, // 統計情報名を指定します。
            MaxResultsCount = 1 // 自分と+-5位をあわせて合計11件を取得します。
        };

        PlayFabClientAPI.GetLeaderboardAroundPlayer(request, OnSuccess, OnError);

        void OnSuccess(GetLeaderboardAroundPlayerResult leaderboardResult)
        {
            foreach (var item in leaderboardResult.Leaderboard)
            {
                rankingDisplay.setAroundRanking((item.Position+1).ToString(), item.DisplayName, item.StatValue.ToString());
            }
        }

        void OnError(PlayFabError error)
        {
            Debug.Log($"{error.Error}");
        }
    }

    private IEnumerator DelayCoroutine(float seconds, Action action)
    {
        yield return new WaitForSeconds(seconds);
        action?.Invoke();
    }
}