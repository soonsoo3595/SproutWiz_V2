using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Leaderboards;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class RankingView : MonoBehaviour
{
    public TextMeshProUGUI ranking;

    private async void OnEnable()
    {
        ranking.text = "";

        var scoreResponse = await LeaderboardsService.Instance.GetScoresAsync(DataManager.TopLeaderboardId);

        scoreResponse.Results.ForEach(score =>
        {
            ranking.text += score.Rank + ". " + score.PlayerName + " - " + score.Score + "\n";
        });
    }
}
