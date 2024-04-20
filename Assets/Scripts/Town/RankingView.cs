using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Leaderboards;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

[System.Serializable]
public class Ranker
{
    public TextMeshProUGUI userName;
    public TextMeshProUGUI userScore;
}

public class RankingView : MonoBehaviour
{
    [SerializeField] private Ranker[] rankers;
    [SerializeField] private TextMeshProUGUI playerName;
    [SerializeField] private TextMeshProUGUI playerTopScore;
    [SerializeField] private TextMeshProUGUI playerTotalScore;

    private void OnEnable()
    {
        ClickBoard();
    }

    private void ClickBoard()
    {
        GameManager.Instance.CheckNetwork();

        playerName.text = DataManager.playerData.userName;
        playerTopScore.text = DataManager.playerData.bestScore.ToString();
        playerTotalScore.text = DataManager.playerData.totalScore.ToString();

        GetTopLeaderboard();
    }

    public async void GetTopLeaderboard()
    {
        SceneObject.Instance.ShowSAEMO(true);
        var scoreResponse = await LeaderboardsService.Instance.GetScoresAsync(DataManager.TopLeaderboardId);
        SceneObject.Instance.ShowSAEMO(false);

        for (int i = 0; i < 10; i++)
        {
            if (i >= scoreResponse.Results.Count)
            {
                rankers[i].userName.text = "";
                rankers[i].userScore.text = "";
                continue;
            }
            rankers[i].userName.text = scoreResponse.Results[i].PlayerName;
            rankers[i].userScore.text = scoreResponse.Results[i].Score.ToString();
        }
    }

    public async void GetTotalLeaderboard()
    {
        SceneObject.Instance.ShowSAEMO(true);
        var scoreResponse = await LeaderboardsService.Instance.GetScoresAsync(DataManager.TotalLeaderboardId);
        SceneObject.Instance.ShowSAEMO(false);

        for (int i = 0; i < 10; i++)
        {
            if (i >= scoreResponse.Results.Count)
            {
                rankers[i].userName.text = "";
                rankers[i].userScore.text = "";
                continue;
            }
            rankers[i].userName.text = scoreResponse.Results[i].PlayerName;
            rankers[i].userScore.text = scoreResponse.Results[i].Score.ToString();
        }
    }
}
