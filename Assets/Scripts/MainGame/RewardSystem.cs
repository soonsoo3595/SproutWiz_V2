using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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

    // 미니게임 점수
    private float griffonScore;
    private List<int> DrawStrokeScore = new List<int> { 0, 0, 0 };
    private float miniGameGoldBonus = 1f;

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
            harvestScore = mainGame.GetData().HarvestScore;

            if (level != 0)
            {
                harvestScore += (int)DataManager.skillLibrary.GetEffect(SkillType.Harvest, level);
            }

        }
        #endregion

        #region 멀티 수확 점수 계산
        {
            int level = DataManager.skillLibrary.GetCurrentLevel(SkillType.MultiHarvest);

            for(int i = 2; i <= 4; i++)
            {
                multiHarvestScore[i] = mainGame.GetData().MultiHarvestScore[i];
            }

            if(level != 0)
            {
                for(int i = 2; i <= 4; i++)
                {
                    multiHarvestScore[i] += (int)DataManager.skillLibrary.GetEffect(SkillType.MultiHarvest, i - 2, level); 
                }
            }

        }
        #endregion

        #region 햇빛마법 효과(점수 보너스)
        {
            int level = DataManager.skillLibrary.GetCurrentLevel(SkillType.SunshineMagicMastery);
            magicEffect = mainGame.GetData().SunshineMagicEffect;

            if(level != 0)
            {
                magicEffect = DataManager.skillLibrary.GetEffect(SkillType.SunshineMagicMastery, level);
            }

        }
        #endregion

        #region 마나맥 점수
        {
            int level = DataManager.skillLibrary.GetCurrentLevel(SkillType.DrawLine);

            for (int i = 0; i <= 2; i++)
            {
                DrawStrokeScore[i] = mainGame.GetData().DrawLineScore[i];
            }

            if (level != 0)
            {
                for (int i = 0; i <= 2; i++)
                {
                    DrawStrokeScore[i] = (int)DataManager.skillLibrary.GetEffect(SkillType.DrawLine, i, level);
                }
            }

        }
        #endregion

        #region 그리폰 퇴치 점수
        {
            int level = DataManager.skillLibrary.GetCurrentLevel(SkillType.HuntBird);
            griffonScore = mainGame.GetData().GriffonScore;

            if (level != 0)
            {
                griffonScore = DataManager.skillLibrary.GetEffect(SkillType.HuntBird, level);
            }

        }
        #endregion

        #region 미니게임 골드 보너스
        {
            int level = DataManager.skillLibrary.GetCurrentLevel(SkillType.MiniGameGoldReward);

            if(level != 0)
            {
                miniGameGoldBonus += DataManager.skillLibrary.GetEffect(SkillType.MiniGameGoldReward, level);
            }

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
        int plusGold = 0;

        if (type == EMinigameType.DrawLine)
        {
            if (GameManager.Instance.isTutorial)
            {
                plusScore = 1000;
            }
            else
            {
                plusScore = DrawStrokeScore[index];
                plusGold = mainGame.GetData().DrawLineGold;
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
                plusGold = mainGame.GetData().GriffonGold;
            }
        }

        AddScore(plusScore);
        AddGold((int)(plusGold * miniGameGoldBonus));
    }
}
