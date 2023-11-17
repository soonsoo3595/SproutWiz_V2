using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    public MainGame mainGame;
    public GameObject combo;
    public Text scoreTxt;

    private int score;
    public int Score
    {
        get => score;
        set
        {
            score = value;
            UpdateScoreText();
        }
    }

    [Header("Score")]
    public int normalHarvestScore = 100;
    public List<int> multiHarvestScore;
    
    void Start()
    {
        InitScore();
        EventManager.harvestCount += Harvest;
    }

    private void UpdateScoreText() => scoreTxt.text = $"Score : {Score}";

    private void AddScore(int score) 
    {
        if (mainGame.isFeverOn)
            score *= 2;

        Score += score;
    }

    public void InitScore() => Score = 0;
    
    public void Harvest(int count)
    {
        if (count == 0) return;

        int curScore = normalHarvestScore * count + multiHarvestScore[count];

        Debug.Log(count + "°³ ¼öÈ®ÇØ¼­ " + curScore + "Á¡ È¹µæ");
        StartCoroutine(PrintCombo(count));
        AddScore(curScore);
    }

    IEnumerator PrintCombo(int count)
    {
        if (count <= 1) yield break;

        combo.SetActive(true);
        
        Text comboTxt = combo.GetComponentInChildren<Text>();

        switch(count)
        {
            case 2:
                comboTxt.text = "Good!";
                break;
            case 3:
                comboTxt.text = "Nice!";
                break;
            case 4:
                comboTxt.text = "Excellent!";
                break;
        }

        yield return new WaitForSeconds(1f);

        combo.SetActive(false);
    }
}
