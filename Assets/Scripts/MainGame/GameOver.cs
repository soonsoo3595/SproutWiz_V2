using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public MainGame mainGame;

    public TextMeshProUGUI[] records;
    public TextMeshProUGUI scoreTxt;
    public Button retryBtn;
    public Button homeBtn;
    public Button skipBtn;

    private List<int> gameRecords;
    private int score;
    private bool isSkipped = false;

    private void Start()
    {
        retryBtn.onClick.AddListener(Retry);
        skipBtn.onClick.AddListener(Skip);
    }

    private void OnEnable()
    {
        gameRecords = mainGame.gameRecord.GetRecord();
        score = mainGame.scoreSystem.Score;

        StartCoroutine(ShowResult());
    }

    private void Retry()
    {
        Init();
        mainGame.Retry();
    }

    private void Init()
    {
        for(int i = 0; i < records.Length; i++)
        {
            records[i].text = "0000";
        }

        scoreTxt.text = "0";
        isSkipped = false;
    }

    private void Skip()
    {
        if (isSkipped) return;

        StopAllCoroutines();

        for(int i = 0; i < records.Length; i++)
        {
            string formattedNumber = gameRecords[i].ToString("0000");
            records[i].text = formattedNumber;
        }

        scoreTxt.text = score.ToString("N0");

        isSkipped = true;
    }

    IEnumerator ShowResult()
    {
        for (int i = 0; i < records.Length; i++)
        {
            for (int j = 0; j <= gameRecords[i]; j++)
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
}
