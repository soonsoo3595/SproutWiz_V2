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

    private void Start()
    {
        retryBtn.onClick.AddListener(Retry);
    }

    private void OnEnable()
    {
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
    }

    IEnumerator ShowResult()
    {
        List<int> gameRecords = mainGame.gameRecord.GetRecord();
        int score = mainGame.scoreSystem.Score;

        for (int i = 0; i < records.Length; i++)
        {
            for (int j = 0; j <= gameRecords[i]; j++)
            {
                string formattedNumber = j.ToString("0000");
                records[i].text = formattedNumber;

                yield return new WaitForSeconds(0.1f);
            }
        }

        for (int i = 0; i <= score;)
        {
            string scoreText = i.ToString("N0");
            scoreTxt.text = scoreText;

            if (score - i > 1000)
                i += 1000;
            else if (score - i > 100)
                i += 100;
            else if (score - i > 10)
                i += 10;
            else
                i++;

            yield return new WaitForSeconds(0.1f);
        }

        yield return null;
    }
}
