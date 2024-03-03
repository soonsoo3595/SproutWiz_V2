using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using System.Reflection;

public class GoalSystem : MonoBehaviour
{
    public MainGame mainGame;
    public GoalMaker goalMaker;

    public List<GameObject> onGoals;
    public List<GameObject> offGoals;
    public List<GameObject> locations;

    private List<Goal> goalList;

    private int totalGoalCount;
    private float goalScoreBonus = 1f;
    private bool isWaiting = false;

    void Start()
    {
        Assign();
        Activate();
    }

    public void UpdateContainer()
    {
        totalGoalCount = 0;
        goalList = goalMaker.GetGoalList();

        //튜토리얼 목표.
        if(GameManager.Instance.isTutorial)
        {
            goalList = goalMaker.GetTutorialGoalList();
        }

        for (int i = 0; i < goalList.Count; i++)
        {
            if (goalList[i].count == 0) continue;

            totalGoalCount += goalList[i].count;

            OffGoal(i);
            
            offGoals[i].GetComponentInChildren<TextMeshProUGUI>().text = $"{goalList[i].count}";
        }
    }
    
    public void RetryGame()
    {
        Activate();
        goalMaker.InitStage();
    }

    public void IsClearGoals()
    {
        bool isClear = true;

        for (int i = 0; i < goalList.Count; i++)
        {
            if (goalList[i].count > 0)
            {
                isClear = false;
                break;
            }
        }

        if (!isClear || isWaiting) return;

        GameManager.Instance.soundEffect.PlayOneShotSoundEffect("clearGoal");

        // 튜토리얼에서는 다음 목표 진행이 없음.
        if (GameManager.Instance.isTutorial)
        {
            mainGame.rewardSystem.AddScore(1000);
            return;
        }

        EventManager.recordUpdate(RecordType.ClearGoal);
        GetRewards();

        StartCoroutine(NextGoal());
    }

    public IEnumerator StartAnimation(bool skip = false)
    {
        for (int i = 0; i < 3; i++)
        {
            OnGoal(i);

            if (skip)
            {
                yield return null;
            }
            else
            {
                yield return new WaitForSeconds(1f);
            }
        }
    }

    private void Assign()
    {
        {
            int level = DataManager.skillLibrary.GetCurrentLevel(SkillType.GoalReward);
            goalScoreBonus += DataManager.skillLibrary.GetEffect(SkillType.GoalReward, level);
            Debug.Log("목표 달성 시 점수 보너스 : " + goalScoreBonus);
        }

        #region 이벤트 등록
        EventManager.tileHarvest += UpdateGoal;
        EventManager.afterApplyTetris += IsClearGoals;
        EventManager.resetMainGame += RetryGame;
        #endregion
    }


    private void Activate()
    {
        for (int i = 0; i < onGoals.Count; i++)
        {
            onGoals[i].SetActive(false);

            offGoals[i].transform.localPosition = locations[i].transform.localPosition;
            offGoals[i].GetComponentInChildren<TextMeshProUGUI>().text = "";
            offGoals[i].SetActive(true);
        }
    }

    private void OnGoal(int index)
    {
        offGoals[index].transform.DOLocalMoveY(offGoals[index].transform.localPosition.y - 85f, 0.5f).SetEase(Ease.OutBack)
            .onComplete = () => {
                offGoals[index].GetComponentInChildren<TextMeshProUGUI>().text = "";
                offGoals[index].SetActive(false); onGoals[index].SetActive(true);
            };
    }

    private void OffGoal(int index)
    {
        onGoals[index].SetActive(false); offGoals[index].SetActive(true);

        offGoals[index].transform.DOLocalMoveY(offGoals[index].transform.localPosition.y + 85f, 0.5f).SetEase(Ease.OutBack);
    }

    private void UpdateGoal(TileData tile)
    {
        ElementType element = tile.GetElement().GetElementType();
        int index = (int)element - 1;

        if (goalList[index].count == 0) return;

        goalList[index].count = Mathf.Clamp(goalList[index].count - 1, 0, 99);
        offGoals[index].GetComponentInChildren<TextMeshProUGUI>().text = $"{goalList[index].count}";

        if (goalList[index].count == 0)
        {
            OnGoal(index);
        }
    }

    private IEnumerator NextGoal()
    {
        isWaiting = true;
        yield return new WaitForSeconds(1f);

        isWaiting = false;
        goalMaker.NextStage();
        UpdateContainer();

        GridManager.Instance.ResetDeployableGrid();
        EventManager.changeTileData(new GridPosition(-1, -1));
    }

    private void GetRewards()
    {
        int plusScore = (int)(totalGoalCount * 10 * goalScoreBonus) + (totalGoalCount / 3 * 100);
        mainGame.rewardSystem.AddScore(plusScore);

        int plusGold = (totalGoalCount * 5) + (totalGoalCount / 3 * 50); 
        mainGame.rewardSystem.AddGold(plusGold);

        mainGame.timer.AddTime(10f);
    }
}