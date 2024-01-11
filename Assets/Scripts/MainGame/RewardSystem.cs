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

    public int Gold;

    private int harvestScore;
    private List<int> multiHarvestScore = new List<int> { 0, 0, 0, 0, 0 };
    private float magicEffect;

    private CinemachineImpulseSource impulseSource;

    void Start()
    {
        InitReward();
        Assign();

        EventManager.harvestCount += Harvest;
        EventManager.resetMainGame += InitReward;

        // TODO : FindObject 수정 필요.
        impulseSource = FindObjectOfType<CinemachineImpulseSource>();
    }

    public void AddScore(int score)
    {
        Score += score;
    }
    
    public void AddGold(int gold)
    {
        Gold += gold;
    }

    public void InitReward()
    {
        Score = 0; Gold = 0;
    }
    
    public void Harvest(int count)
    {
        if (count == 0) return;

        GameManager.Instance.soundEffect.PlayOneShotSoundEffect("harvest");

        int plusScore = harvestScore * count + multiHarvestScore[count];
        if (mainGame.isMagicOn)
        {
            float magicScore = plusScore * magicEffect;
            plusScore = (int)magicScore;
        }

        Debug.Log(count + "개 수확해서 " + plusScore + "점 획득");
        AddScore(plusScore); 

        mainGame.gameRecord.harvestCount += count;

        StartCoroutine(MultiHarvest(count));
    }

    private void Assign()
    {
        #region 일반 수확 점수 계산
        {
            int level = DataManager.playerData.level_HarvestScore;
            harvestScore = DataManager.GameData.harvestScore + DataManager.GameData.upgrade_HarvestScore[level];
        }
        #endregion

        #region 멀티 수확 점수 계산
        {
            int level = DataManager.playerData.level_MultiHarvestScore;
            multiHarvestScore[2] = DataManager.GameData.multiHarvestScore[2] + DataManager.GameData.upgrade_DoubleHarvestScore[level];
            multiHarvestScore[3] = DataManager.GameData.multiHarvestScore[3] + DataManager.GameData.upgrade_TripleHarvestScore[level];
            multiHarvestScore[4] = DataManager.GameData.multiHarvestScore[4] + DataManager.GameData.upgrade_QuadraHarvestScore[level];
        }
        #endregion

        #region 햇빛마법 효과(점수 보너스)
        {
            int level = DataManager.playerData.level_SunshineMagicEffect;
            magicEffect = DataManager.GameData.sunshineMagicEffect + DataManager.GameData.upgrade_sunshineMagicEffect[level];
        }
        #endregion
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
