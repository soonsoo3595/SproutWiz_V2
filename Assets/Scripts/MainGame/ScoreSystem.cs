using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    private int score;

    public MainGame mainGame;
    public Text scoreTxt;


    [Header("Score")]
    public int normalHarvestScore = 100;
    public List<int> multiHarvestScore;
    
    void Start()
    {
        InitScore();
        LevelData.addHarvestScore += Harvest;
    }

    private void UpdateScoreText() => scoreTxt.text = $"Score : {score}";

    private void AddScore(int score) 
    {
        if (mainGame.isFeverOn)
            score *= 2;

        this.score += score;
        UpdateScoreText();
    }

    public void InitScore()
    {
        score = 0;
        UpdateScoreText();
    }

    public void Harvest(int count)
    {
        if (count == 0) return;

        int curScore = normalHarvestScore * count + multiHarvestScore[count];

        Debug.Log(count + "°³ ¼öÈ®ÇØ¼­ " + curScore + "Á¡ È¹µæ");
        AddScore(curScore);
        mainGame.feverSystem.IncreaseGauge(count);
    }
}
