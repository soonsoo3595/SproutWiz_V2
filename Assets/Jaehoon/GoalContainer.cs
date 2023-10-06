using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalContainer : MonoBehaviour
{
    public Sprite[] elementSprites;
    public MainGame mainGame;

    public List<Goal> goalObjectList;
    private List<GoalData> goalDataList;
    
    void Start()
    {
        TileFactory.harvest += AfterHarvest;

        UpdateContainer();
    }

    public void UpdateContainer()
    {
        Deactivation();
        GetGoal();
        
        for(int i = 0; i < goalDataList.Count; i++)
        {
            goalObjectList[i].gameObject.SetActive(true);
            Goal goalObject = goalObjectList[i];
            goalObject.goalData.SetData(goalDataList[i].element, goalDataList[i].count);

            goalObject.image.sprite = elementSprites[(int)(goalObject.goalData.element.GetElementType()) - 1];
            goalObject.text.text = $"x{goalObject.goalData.count}";
        }
    }

    public void Deactivation()
    {
        for (int i = 0; i < goalObjectList.Count; i++)
        {
            goalObjectList[i].gameObject.SetActive(false);
        }
    }
    
    private void GetGoal()
    {
        Stage stage = mainGame.stage;
        
        stage.SetStage();
        goalDataList = stage.goalList;
        goalDataList.Sort(SortGoal);
    }
    
    private int SortGoal(GoalData op1, GoalData op2)
    {
        return op1.element.GetElementType() < op2.element.GetElementType() ? -1 : 1;
    }

    private void UpdateText(Goal goal)
    {
        goal.text.text = $"x{goal.goalData.count}";
    }
    
    private void AfterHarvest(TileData tile)
    {
        Element element = tile.GetElement();

        for (int i = 0; i < goalDataList.Count; i++)
        {
            if (goalObjectList[i].goalData.element != element) continue;
            goalObjectList[i].goalData.count = Mathf.Clamp(goalObjectList[i].goalData.count - 1, 0, 99);
            UpdateText(goalObjectList[i]);
        }

        if (IsAchieveGoal())
        {
            Debug.Log("Achieve");
            mainGame.stage.NextStage();
            UpdateContainer();
        }
        else
        {
            Debug.Log("Fail");
        }
    }

    private bool IsAchieveGoal()
    {
        foreach (var goal in goalObjectList)
        {
            if (goal.goalData.count > 0)
                return false;
        }

        return true;
    }
}
