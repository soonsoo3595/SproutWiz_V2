using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage
{
    public List<GoalData> goalList;
    public List<Element> elementList;

    public Stage()
    {
        goalList = new List<GoalData>();
        elementList = new List<Element>();

        InitElemetList();
        SetStageGoal(1);
    }
    public void SetStageGoal(int num)
    {
        if(num == 1)
        {
            Stage1();
        }
        else if(num == 2)
        {
            Stage2();
        }
        else if (num == 3)
        {
            Stage3();
        }
        else if( num == 4)
        {
            Stage4();
        }
        else if(num ==5)
        {
            Stage5();
        }
        else if(num >= 6)
        {
            Stage6(num);
        }
    }

    public void InitElemetList()
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

    public void Stage1()
    {
        for(int i = 0; i < 2; i++)
        {
            GoalData goal = new GoalData(elementList[i], 2);
            goalList.Add(goal);
        }
    }

    public void Stage2()
    {

    }

    public void Stage3()
    {

    }

    public void Stage4()
    {

    }

    public void Stage5()
    {

    }

    public void Stage6(int num)
    {

    }
}
