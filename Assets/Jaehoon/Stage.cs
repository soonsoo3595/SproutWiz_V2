using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Stage : MonoBehaviour
{
    [HideInInspector] public List<GoalData> goalList;

    private int elementCount;
    public int currentStage;

    private void Start()
    {
        goalList = new List<GoalData>();

        currentStage = 1;
        elementCount = 3;
    }

    public void SetStage()
    {
        int rand = currentStage == 1 ? 0 : Random.Range(0, 3);
        
        SetGoal((GoalType)rand);
    }

    public void NextStage()
    {
        goalList.Clear();
        currentStage++;
    }
    
    public void SetGoal(GoalType goalType)
    {
        switch(goalType)
        {
          case GoalType.A:
              TypeA();
              break;
          case GoalType.B:
              TypeB();
              break;
          case GoalType.C:
              TypeC();
              break;
        }
    }

    private void TypeA()
    {
        int rand = Random.Range(0, elementCount);

        Element element = new Element((ElementType)rand + 1);
        GoalData goalData = new GoalData(element, 2 * currentStage);
        goalList.Add(goalData);
    }

    private void TypeB()
    {
        List<int> numList = new List<int>(3) { 1, 2, 3 };

        while(numList.Count > elementCount - 2)
        {
            int rand = Random.Range(0, numList.Count);

            Element element = new Element((ElementType)numList[rand]);
            GoalData goalData = new GoalData(element, currentStage);
            goalList.Add(goalData);

            numList.RemoveAt(rand);
        }
    }

    private void TypeC()
    {
        for (int i = 0; i < 3; i++)
        {
            Element element = new Element((ElementType)i + 1);
            GoalData goalData = new GoalData(element, currentStage);
            goalList.Add(goalData);
        }
    }
    
}
