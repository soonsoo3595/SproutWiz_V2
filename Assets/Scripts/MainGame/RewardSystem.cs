using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using System.Runtime.InteropServices;
using static EventManager;

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

    // �̴ϰ��� ����
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

        // TODO : FindObject ���� �ʿ�.
        impulseSource = FindObjectOfType<CinemachineImpulseSource>();
    }

    public void AddScore(int score)
    {
        Score += score;

        // �̴ϰ��� �ߵ� ���� üũ.
        // TODO: �ѹ��� üũ�� �� �ְ� �����ؾ���.
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

        Debug.Log(count + "�� ��Ȯ�ؼ� " + plusScore + "�� ȹ��");
        AddScore(plusScore);

        mainGame.gameRecord.AddRecord(RecordType.Harvest, count);

        StartCoroutine(MultiHarvest(count));
    }

    private void Assign()
    {
        #region �Ϲ� ��Ȯ ���� ���
        {
            int level = DataManager.skillLibrary.GetCurrentLevel(SkillType.Harvest);
            harvestScore = DataManager.GameData.HarvestScore;

            if (level != 0)
            {
                harvestScore += (int)DataManager.skillLibrary.GetEffect(SkillType.Harvest, level);
            }

            Debug.Log("��� ȿ�� ���� : " + level + ", ��Ȯ �� ���� : " + harvestScore);
        }
        #endregion

        #region ��Ƽ ��Ȯ ���� ���
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

            Debug.Log("��Ƽ ��Ȯ ���� : " + level + ", 2�� ��Ȯ �� : " + multiHarvestScore[2] + ", 3�� ��Ȯ �� : " + multiHarvestScore[3] + ", 4�� ��Ȯ �� : " + multiHarvestScore[4]);
        }
        #endregion

        #region �޺����� ȿ��(���� ���ʽ�)
        {
            int level = DataManager.skillLibrary.GetCurrentLevel(SkillType.SunshineMagicMastery);
            magicEffect = DataManager.GameData.SunshineMagicEffect;

            if(level != 0)
            {
                magicEffect = DataManager.skillLibrary.GetEffect(SkillType.SunshineMagicMastery, level);
            }

            Debug.Log("�޺� ���� �����͸� ���� : " + level + ", �޺� ���� Ȱ��ȭ ���ʽ� : " + magicEffect);
        }
        #endregion

        #region ������ ����
        {
            int level = DataManager.skillLibrary.GetCurrentLevel(SkillType.DrawStroke);

            for (int i = 0; i <= 2; i++)
            {
                DrawStrokeScore[i] = DataManager.GameData.DrawLineScore[i];
            }

            if (level != 0)
            {
                for (int i = 0; i <= 2; i++)
                {
                    DrawStrokeScore[i] = (int)DataManager.skillLibrary.GetEffect(SkillType.DrawStroke, i, level);
                }
            }

            DataManager.playerData.gold += DataManager.GameData.DrawLineGold;

            Debug.Log("�Ѻױ׸��� ���� : " + level + ", ���� : " + DrawStrokeScore);
        }
        #endregion

        #region �׸��� ��ġ ����
        {
            int level = DataManager.skillLibrary.GetCurrentLevel(SkillType.HuntBird);
            griffonScore = DataManager.GameData.GriffonScore;

            if (level != 0)
            {
                griffonScore = DataManager.skillLibrary.GetEffect(SkillType.HuntBird, level);
            }

            DataManager.playerData.gold += DataManager.GameData.GriffonGold;

            Debug.Log("�׸���(��) ��ġ ���� : " + level + ", ��ġ �� ���� : " + griffonScore);
        }
        #endregion
       
    }


    IEnumerator MultiHarvest(int count)
    {
        if (count <= 1) yield break;
        
        mainGame.gameRecord.AddRecord(RecordType.MultiHarvest);

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

    public void RewardMiniGame(EMinigameType type, int index)
    {
        int plusScore = 0;

        Debug.Log($"���� index: {index}");

        if (type == EMinigameType.DrawLine)
        {
            plusScore = DrawStrokeScore[index];
        }
        else if(type == EMinigameType.Griffon)
        {
            plusScore = (int)griffonScore;
        }

        AddScore(plusScore);
    }
}
