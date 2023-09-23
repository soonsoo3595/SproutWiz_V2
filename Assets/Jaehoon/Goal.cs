using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalData
{
    public Element element;
    public int count;

    public GoalData()
    {
        this.element = null;
        this.count = 0;
    }

    public GoalData(Element element, int count)
    {
        this.element = element;
        this.count = count;
    }
}
public class Goal : MonoBehaviour
{
    public Image image;
    public Text text;

    [HideInInspector] public GoalData goalData = new GoalData();

}
