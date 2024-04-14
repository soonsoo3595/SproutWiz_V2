using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Leaderboards;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class RankingView : MonoBehaviour
{
    public TextMeshProUGUI ranking;

    private void OnEnable()
    {
        StartCoroutine(ClickBoard());
    }

    private IEnumerator ClickBoard()
    {
        while (true)
        {
            if (GameManager.Instance.CheckNetwork())
            {
                break;
            }

            yield return null;
        }

        GetRanking();
    }

    private async void GetRanking()
    {
        ranking.text = "";

        var scoreResponse = await LeaderboardsService.Instance.GetScoresAsync(DataManager.TopLeaderboardId);

        scoreResponse.Results.ForEach(score =>
        {
            ranking.text += score.Rank + ". " + score.PlayerName + " - " + score.Score + "\n";
        });
    }
}
