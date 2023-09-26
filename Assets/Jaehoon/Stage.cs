using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage
{
    public List<GoalData> goalList;
    public List<Element> elementList;
    
    public int currentStage;
    
    public Stage()
    {
        goalList = new List<GoalData>();
        elementList = new List<Element>();

        currentStage = 1;
        
        InitElementList();
    }

    public void SetStageGoal(StageInfo stageInfo)
    {
        for (int i = 0; i < 3; i++)
        {
            if(stageInfo.targets[i] == 0)
                continue;

            GoalData goal = new GoalData(elementList[i], stageInfo.targets[i]);
            goalList.Add(goal);
        }
    }

    public void InitElementList()
    {
        List<int> numList = new List<int>(3) { 1, 2, 3 };

        while(numList.Count > 0)
        {
            int rand = Random.Range(0, numList.Count);

            Element element = new Element((ElementType)numList[rand]);
            elementList.Add(element);

            numList.RemoveAt(rand);
        }
    }

}
