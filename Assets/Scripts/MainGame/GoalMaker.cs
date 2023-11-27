using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Goal
{
    public ElementType element;
    public int count;

    public Goal()
    {
        element = ElementType.None;
        count = 0;
    }

    public Goal(ElementType element, int count)
    {
        this.element = element;
        this.count = count;
    }

    public void SetData(ElementType element, int count)
    {
        this.element = element;
        this.count = count;
    }
}

public class GoalMaker : MonoBehaviour
{
    [HideInInspector] private List<Goal> goalList;

    // private int elementCount;
    private int goalTypeCount;

    private ElementType prevElement;
    private GoalType prevGoal;
    private int currentStage;

    private float r;    // 계수 r
    [SerializeField] private float minB, maxB;   // 계수 B의 최소값, 최대값

    private void Awake()
    {
        goalList = new List<Goal>();
        goalTypeCount = (int)GoalType.D;

        InitStage();
    }

    public void InitStage()
    {
        prevElement = ElementType.None;
        prevGoal = GoalType.None;
        currentStage = 1;
    }
    
    public List<Goal> GetGoalList()
    {
        goalList.Clear();

        CalcualteExpression();
        Process();
        goalList.Sort(SortGoal);

        return goalList;
    }

    public void NextStage()
    {
        currentStage++;
    }

    // r값을 계산하는 수식
    private void CalcualteExpression()
    {
        float B;

        if (currentStage == 1) B = 0f;
        else B = Random.Range(minB, maxB);

        r = 5 * math.log10(currentStage + 1) + B;
    }

    private GoalType RandomType()
    {
        int rand = currentStage == 1 ? 1 : Random.Range(1, goalTypeCount + 1);

        return (GoalType)rand;
    }

    private void Process()
    {
        GoalType curGoal = RandomType();

        if (curGoal != prevGoal) prevElement = ElementType.None;

        switch(curGoal)
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
            case GoalType.D:
                TypeD();
                break;
        }
    }

    private ElementType GetDifferentElement()
    {
        /*
        List<ElementType> elements = new List<ElementType>();
    
        if (prevElements.Count > 0)
        {
            while (prevElements.Count > 0)
            {
                List<int> list = new List<int>(3) { 1, 2, 3 };
                list.Remove((int)prevElements.Dequeue());
    
                int rand = Random.Range(0, list.Count);
                elements.Add((ElementType)list[rand]);
            }
        }
        else
        {
            while(count > 0)
            {
                List<int> list = new List<int>(3) { 1, 2, 3 };
    
                int rand = Random.Range(0, list.Count);
                elements.Add((ElementType)list[rand]);
    
                list.RemoveAt(rand);
                count--;
            }
        }
        return elements;
        */
        List<ElementType> elements = new List<ElementType>(3) { ElementType.Fire, ElementType.Water, ElementType.Grass};
        elements.Remove(prevElement);

        int rand = Random.Range(0, elements.Count);

        return elements[rand];
    }

    private void TypeA()
    {
        prevGoal = GoalType.A;
        ElementType target = GetDifferentElement();
        prevElement = target;

        Goal goal = new Goal(target, (int)math.round(r));
        goalList.Add(goal);
    }

    private void TypeB()
    {
        prevGoal = GoalType.B;

        for (int i = 0; i < 2; i++)
        {
            ElementType target = GetDifferentElement();
            prevElement = target;

            Goal goal = new Goal(target, (int)math.round(r));
            goalList.Add(goal);
        }
    }

    private void TypeC()
    {
        prevGoal = GoalType.C;

        for (int i = 0; i < 3; i++)
        {
            ElementType element = (ElementType)(i + 1);
            Goal goal = new Goal(element, (int)math.round(r));
            goalList.Add(goal);
        }
    }
    
    private void TypeD()
    {
        prevGoal = GoalType.D;

        List<float> coefficients = new List<float>(2) { 0.75f, 1.5f };

        for (int i = 0; i < 2; i++)
        {
            ElementType target = GetDifferentElement();
            prevElement = target;

            Goal goal = new Goal(target, (int)math.round(coefficients[i] * r));
            goalList.Add(goal);
        }
    }

    private int SortGoal(Goal op1, Goal op2)
    {
        return op1.element < op2.element ? -1 : 1;
    }
}
