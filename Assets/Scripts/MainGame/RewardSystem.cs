using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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
            scoreTxt.text = score.ToString("N0");
        }
    }

    [Header("Score")]
    public int normalHarvestScore = 100;
    public List<int> multiHarvestScore;
    
    void Start()
    {
        InitScore();

        EventManager.harvestCount += Harvest;
        EventManager.resetMainGame += InitScore;
    }

    private void AddScore(int score) => Score += score;
    
    public void InitScore() => Score = 0;
    
    public void Harvest(int count)
    {
        if (count == 0) return;

        int curScore = normalHarvestScore * count + multiHarvestScore[count];

        if(mainGame.isFeverOn) curScore *= 2;

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
                GameManager.Instance.soundEffect.PlayOneShotSoundEffect("double");
                comboTxt.text = "Good!";
                break;
            case 3:
                GameManager.Instance.soundEffect.PlayOneShotSoundEffect("triple");
                comboTxt.text = "Nice!";
                break;
            case 4:
                GameManager.Instance.soundEffect.PlayOneShotSoundEffect("quadruple");
                comboTxt.text = "Excellent!";
                break;
        }

        yield return new WaitForSeconds(1f);

        combo.SetActive(false);
    }
}
