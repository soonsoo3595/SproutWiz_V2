using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalContainer : MonoBehaviour
{
    public GameObject goalPrefab;
    public List<StageInfo> stageInfos;
    public MainGame mainGame;
    public Sprite[] elementSprites;

    void Start()
    {
        UpdateContainer();
    }

    public void UpdateContainer()
    {
        List<GoalData> list = GetGoal();

        list.Sort(SortGoal);
        
        for(int i = 0; i < list.Count; i++)
        {
            GameObject goalObject = Instantiate(goalPrefab, transform, false);

            Goal goal = goalObject.GetComponent<Goal>();
            goal.goalData.SetData(list[i].element, list[i].count);

            goal.image.sprite = elementSprites[(int)(goal.goalData.element.GetElementType()) - 1];
            goal.text.text = $"x{goal.goalData.count}";
        }

    }

    public List<GoalData> GetGoal()
    {
        Stage stage = mainGame.stage;
        int currentStage = stage.currentStage;
        
        stage.SetStageGoal(stageInfos[currentStage - 1]);

        return stage.goalList;
    }
    
    private int SortGoal(GoalData op1, GoalData op2)
    {
        return op1.element.GetElementType() < op2.element.GetElementType() ? -1 : 1;
    }
    
    
}
