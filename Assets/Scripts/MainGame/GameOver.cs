using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Leaderboards;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public MainGame mainGame;

    [Header("Text")]
    public TextMeshProUGUI[] records;
    public TextMeshProUGUI scoreTxt;
    public TextMeshProUGUI goldTxt;

    [Header("Button")]
    public Button retryBtn;
    public Button skipBtn;

    private int score;
    private int gold;
    private bool isSkipped = false;

    private void Start()
    {
        retryBtn.onClick.AddListener(Retry);
        skipBtn.onClick.AddListener(Skip);

        EventManager.resetMainGame += Init;
    }

    private void OnEnable()
    {
        score = mainGame.rewardSystem.Score;
        gold = mainGame.rewardSystem.Gold;

        SaveRecord();
        StartCoroutine(ShowResult());
    }

    private void Retry()
    {
        GameManager.Instance.soundBGM.RestartBGM();
        mainGame.Retry();
    }

    private void Init()
    {
        for(int i = 0; i < records.Length; i++)
        {
            records[i].text = "0000";
        }

        scoreTxt.text = "0";
        goldTxt.text = "0G";
        isSkipped = false;
    }

    private void Skip()
    {
        if (isSkipped) return;

        StopAllCoroutines();

        for(int i = 0; i < records.Length; i++)
        {
            string formattedNumber = mainGame.gameRecord.GetRecord((RecordType)i).ToString("0000");
            records[i].text = formattedNumber;
        }

        scoreTxt.text = score.ToString("N0");
        goldTxt.text = $"{gold}G";

        isSkipped = true;
    }

    IEnumerator ShowResult()
    {
        goldTxt.text = $"{gold}G";

        for (int i = 0; i < records.Length; i++)
        {
            int record = mainGame.gameRecord.GetRecord((RecordType)i);
            for (int j = 0; j <= record; j++)
            {
                string formattedNumber = j.ToString("0000");
                records[i].text = formattedNumber;

                yield return new WaitForSeconds(0.01f);
            }
        }

        for (int i = 0; i <= score;)
        {
            string scoreText = i.ToString("N0");
            scoreTxt.text = scoreText;

            if (score - i > 100)
                i += 100;
            else if (score - i > 10)
                i += 10;
            else
                i++;

            yield return new WaitForSeconds(0.01f);
        }
        
        yield return null;
    }

    private async void SaveRecord()
    {
        DataManager.playerData.totalScore += score;
        await LeaderboardsService.Instance.AddPlayerScoreAsync(DataManager.TotalLeaderboardId, score);

        if(DataManager.playerData.bestScore < score)
        {
            DataManager.playerData.bestScore = score;
            await LeaderboardsService.Instance.AddPlayerScoreAsync(DataManager.TopLeaderboardId, score);
        }

        DataManager.playerData.gold += gold;
    }
}
