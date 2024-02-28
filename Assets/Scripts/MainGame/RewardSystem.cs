using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using System.Runtime.InteropServices;

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

    // 미니게임 점수
    private float griffonScore;
    private List<int> DrawStrokeScore = new List<int> { 0, 0, 0 };

    private CinemachineImpulseSource impulseSource;

    void Start()
    {
        InitReward();
        Assign();

        EventManager.harvestCount += Harvest;
        EventManager.resetMainGame += InitReward;
        EventManager.miniGameSuccess += RewardMiniGame;

        // TODO : FindObject 수정 필요.
        impulseSource = FindObjectOfType<CinemachineImpulseSource>();
    }

    public void AddScore(int score)
    {
        Score += score;

        // 미니게임 발동 조건 체크.
        // TODO: 한번만 체크할 수 있게 변경해야함.
        if (Score >= 3000)
        {
            MiniGameController.Instance.ActivateMiniGame(EMinigameType.DrawLine, mainGame.timer.GetRunTime());
        }
        if (Score >= 4000)
        {
            MiniGameController.Instance.ActivateMiniGame(EMinigameType.Griffon, mainGame.timer.GetRunTime());
        }
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

        if (GameManager.Instance.isTutorial)
        {
            AddScore(1000);
        }
        else
        {
            Debug.Log(count + "개 수확해서 " + plusScore + "점 획득");
            AddScore(plusScore);

            int plusGold = count * 5;
            AddGold(plusGold);
        }

        EventManager.recordUpdate(RecordType.Harvest, count);

        StartCoroutine(MultiHarvest(count));
    }

    private void Assign()
    {
        #region 일반 수확 점수 계산
        {
            int level = DataManager.skillLibrary.GetCurrentLevel(SkillType.Harvest);
            harvestScore = DataManager.GameData.HarvestScore;

            if (level != 0)
            {
                harvestScore += (int)DataManager.skillLibrary.GetEffect(SkillType.Harvest, level);
            }

            Debug.Log("재배 효율 레벨 : " + level + ", 수확 시 점수 : " + harvestScore);
        }
        #endregion

        #region 멀티 수확 점수 계산
        {
            int level = DataManager.skillLibrary.GetCurrentLevel(SkillType.MultiHarvest);

            for(int i = 2; i <= 4; i++)
            {
                multiHarvestScore[i] = DataManager.GameData.MultiHarvestScore[i];
            }

            if(level != 0)
            {
                for(int i = 2; i <= 4; i++)
                {
                    multiHarvestScore[i] += (int)DataManager.skillLibrary.GetEffect(SkillType.MultiHarvest, i - 2, level); 
                }
            }

            Debug.Log("멀티 수확 레벨 : " + level + ", 2개 수확 시 : " + multiHarvestScore[2] + ", 3개 수확 시 : " + multiHarvestScore[3] + ", 4개 수확 시 : " + multiHarvestScore[4]);
        }
        #endregion

        #region 햇빛마법 효과(점수 보너스)
        {
            int level = DataManager.skillLibrary.GetCurrentLevel(SkillType.SunshineMagicMastery);
            magicEffect = DataManager.GameData.SunshineMagicEffect;

            if(level != 0)
            {
                magicEffect = DataManager.skillLibrary.GetEffect(SkillType.SunshineMagicMastery, level);
            }

            Debug.Log("햇빛 마법 마스터리 레벨 : " + level + ", 햇빛 마법 활성화 보너스 : " + magicEffect);
        }
        #endregion

        #region 마나맥 점수
        {
            int level = DataManager.skillLibrary.GetCurrentLevel(SkillType.DrawLine);

            for (int i = 0; i <= 2; i++)
            {
                DrawStrokeScore[i] = DataManager.GameData.DrawLineScore[i];
            }

            if (level != 0)
            {
                for (int i = 0; i <= 2; i++)
                {
                    DrawStrokeScore[i] = (int)DataManager.skillLibrary.GetEffect(SkillType.DrawLine, i, level);
                }
            }

            Debug.Log("한붓그리기 레벨 : " + level + ", 점수 : " + DrawStrokeScore);
        }
        #endregion

        #region 그리폰 퇴치 점수
        {
            int level = DataManager.skillLibrary.GetCurrentLevel(SkillType.HuntBird);
            griffonScore = DataManager.GameData.GriffonScore;

            if (level != 0)
            {
                griffonScore = DataManager.skillLibrary.GetEffect(SkillType.HuntBird, level);
            }

            Debug.Log("그리폰(새) 퇴치 레벨 : " + level + ", 퇴치 시 점수 : " + griffonScore);
        }
        #endregion
       
    }


    IEnumerator MultiHarvest(int count)
    {
        if (count <= 1) yield break;
        
        EventManager.recordUpdate(RecordType.MultiHarvest);

        combo.GetComponent<ComboEffect>().Play(count);

        int plusGold = 0;

        switch(count)
        {
            case 2:
                GameManager.Instance.soundEffect.PlayOneShotSoundEffect("double");
                plusGold = 10;
                break;
            case 3:
                GameManager.Instance.soundEffect.PlayOneShotSoundEffect("triple");
                plusGold = 25;
                break;
            case 4:
                GameManager.Instance.soundEffect.PlayOneShotSoundEffect("quadruple");
                plusGold = 40;
                break;
        }

        AddGold(plusGold);
        impulseSource.GenerateImpulse();

        yield return new WaitForSeconds(1f);
    }

    public void RewardMiniGame(EMinigameType type, int index)
    {
        int plusScore = 0;

        Debug.Log($"인자 index: {index}");

        if (type == EMinigameType.DrawLine)
        {
            if (GameManager.Instance.isTutorial)
            {
                plusScore = 1000;
            }
            else
            {
                plusScore = DrawStrokeScore[index];
                AddGold(DataManager.GameData.DrawLineGold);
            }
        }
        else if(type == EMinigameType.Griffon)
        {
            if (GameManager.Instance.isTutorial)
            {
                plusScore = 500;
            }
            else
            {
                plusScore = (int)griffonScore;
                AddGold(DataManager.GameData.GriffonGold);
            }
        }

        AddScore(plusScore);
    }
}
