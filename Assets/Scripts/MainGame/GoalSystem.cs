using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class GoalSystem : MonoBehaviour
{
    public MainGame mainGame;
    public GoalMaker goalMaker;

    public List<GameObject> goalObjects;
    private List<Goal> goalList;

    private int totalGoalCount;
    private int goalScore;
    private int goalGold;

    void Start()
    {
        Assign();
    }

    public void UpdateContainer()
    {
        Deactivation();
        totalGoalCount = 0;
        goalList = goalMaker.GetGoalList();
        
        for(int i = 0; i < goalList.Count; i++)
        {
            totalGoalCount += goalList[i].count;

            int index = (int)goalList[i].element - 1;
            goalObjects[index].SetActive(true);

            goalObjects[index].GetComponentInChildren<TextMeshProUGUI>().text = $"{goalList[i].count}";
        }
    }
    
    public void RetryGame()
    {
        Deactivation();
        goalMaker.InitStage();
    }

    public void IsAchieveGoal()
    {
        bool flag = true;

        for (int i = 0; i < goalList.Count; i++)
        {
            if (goalList[i].count > 0)
            {
                flag = false;
                break;
            }
        }

        if (!flag) return;

        GameManager.Instance.soundEffect.PlayOneShotSoundEffect("clearGoal");

        mainGame.rewardSystem.AddGold(totalGoalCount * goalGold);
        mainGame.rewardSystem.AddScore(totalGoalCount * goalScore);
        // mainGame.gameRecord.achieveGoalCount++;
        PlayAnimation();
        goalMaker.NextStage();
        UpdateContainer();

        mainGame.timer.AddTime(10f);
        GridManager.Instance.ResetDeployableGrid();
        EventManager.changeTileData(new GridPosition(-1, -1));
    }

    private void Assign()
    {
        goalScore = DataManager.GameData.goalScore;
        goalGold = DataManager.GameData.goalGold;

        #region 이벤트 등록
        EventManager.tileHarvest += UpdateGoal;
        EventManager.afterApplyTetris += IsAchieveGoal;
        EventManager.resetMainGame += RetryGame;
        #endregion

    }

    private void Deactivation()
    {
        for (int i = 0; i < goalObjects.Count; i++)
        {
            goalObjects[i].SetActive(false);
        }
    }

    private void UpdateGoal(TileData tile)
    {
        ElementType element = tile.GetElement().GetElementType();

        for(int i = 0; i < goalList.Count; i++)
        {
            if (goalList[i].element != element) continue;
            goalList[i].count = Mathf.Clamp(goalList[i].count - 1, 0, 99);

            int index = (int)element - 1;
            goalObjects[index].GetComponentInChildren<TextMeshProUGUI>().text = $"{goalList[i].count}";
        }
    }

    private void PlayAnimation()
    {
        Sequence sequence = DOTween.Sequence()
            .Append(transform.DOScale(Vector3.zero, 0.1f))
            .Append(transform.DOScale(Vector3.one, 1f));

        sequence.Play();

    }
}