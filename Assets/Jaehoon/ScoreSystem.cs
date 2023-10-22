using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    private int score;

    public Text scoreTxt;

    [Header("Score")]
    public int normalHarvestScore = 100;
    public List<int> multiHarvestScore;
    
    void Start()
    {
        score = 0;
        UpdateScoreText();
        LevelData.addHarvestScore += Harvest;
    }

    private void UpdateScoreText() => scoreTxt.text = $"Score : {score}";

    private void AddScore(int score) 
    {
        this.score += score;
        UpdateScoreText();
    }

    public void Harvest(int count)
    {
        int score = 0;

        score = normalHarvestScore * count;
        score += multiHarvestScore[count];

        AddScore(score);
    }
}
