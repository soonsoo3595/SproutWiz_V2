using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class RewardSystem : MonoBehaviour
{
    public MainGame mainGame;
    public GameObject combo;
    public TextMeshProUGUI scoreTxt;

    private int score;
    public int Score
    {
        get => score;
        set
        {
            score = value;
            scoreTxt.text = $"{score}";
        }
    }

    [Header("Score")]
    public int normalHarvestScore = 100;
    public List<int> multiHarvestScore;

    private CinemachineImpulseSource impulseSource;

    void Start()
    {
        InitScore();

        EventManager.harvestCount += Harvest;
        EventManager.resetMainGame += InitScore;

        impulseSource = FindObjectOfType<CinemachineImpulseSource>();
    }

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

        mainGame.gameRecord.harvestCount += count;

        StartCoroutine(MultiHarvest(count));
        AddScore(curScore); 
    }

    IEnumerator MultiHarvest(int count)
    {
        if (count <= 1) yield break;

        combo.SetActive(true);
        
        TextMeshProUGUI comboTxt = combo.GetComponentInChildren<TextMeshProUGUI>();

        mainGame.gameRecord.multiHarvestCount++;

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

        impulseSource.GenerateImpulse();

        yield return new WaitForSeconds(1f);

        combo.SetActive(false);
    }
}
