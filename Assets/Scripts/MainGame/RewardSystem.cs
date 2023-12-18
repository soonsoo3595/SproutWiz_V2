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
            scoreTxt.text = score.ToString("N0");
        }
    }

    public int gold;

    [Header("Score")]
    public int normalHarvestScore = 100;
    public List<int> multiHarvestScore;

    private CinemachineImpulseSource impulseSource;

    void Start()
    {
        Init();

        EventManager.harvestCount += Harvest;
        EventManager.resetMainGame += Init;

        // TODO : FindObject 수정 필요.
        impulseSource = FindObjectOfType<CinemachineImpulseSource>();
    }

    public void AddScore(int score)
    {
        Score += score;
    }
    
    public void AddGold(int gold)
    {
        this.gold += gold;
    }

    public void Init()
    {
        Score = 0;
        gold = 0;
    }
    
    public void Harvest(int count)
    {
        if (count == 0) return;

        GameManager.Instance.soundEffect.PlayOneShotSoundEffect("harvest");

        int curScore = normalHarvestScore * count + multiHarvestScore[count];

        if (mainGame.isFeverOn) curScore *= 2;
        Debug.Log(count + "개 수확해서 " + curScore + "점 획득");

        mainGame.gameRecord.harvestCount += count;

        StartCoroutine(MultiHarvest(count));
        AddScore(curScore); 
    }

    IEnumerator MultiHarvest(int count)
    {
        if (count <= 1) yield break;

        mainGame.gameRecord.multiHarvestCount++;

        combo.GetComponent<ComboEffect>().Play(count);

        switch(count)
        {
            case 2:
                GameManager.Instance.soundEffect.PlayOneShotSoundEffect("double");
                break;
            case 3:
                GameManager.Instance.soundEffect.PlayOneShotSoundEffect("triple");
                break;
            case 4:
                GameManager.Instance.soundEffect.PlayOneShotSoundEffect("quadruple");
                break;
        }

        impulseSource.GenerateImpulse();

        yield return new WaitForSeconds(1f);
    }
}
