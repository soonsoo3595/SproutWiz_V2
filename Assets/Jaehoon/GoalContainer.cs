using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalContainer : MonoBehaviour
{
    public List<GameObject> goals;
    public GameObject goalPrefab;
    public Stage stage;

    public Sprite[] elementSprites;

    void Start()
    {
        stage = new Stage();
        InitContainer();
    }

    public void InitContainer()
    {
        int goalSize = stage.goalList.Count;

        for(int i = 0; i < goalSize; i++)
        {
            GameObject gameObject = Instantiate(goalPrefab);

            Goal goal = gameObject.GetComponent<Goal>();
            goal.goalData.element = stage.goalList[i].element;
            goal.goalData.count = stage.goalList[i].count;

            goal.image.sprite = elementSprites[(int)(goal.goalData.element.GetElementType()) - 1];
            goal.text.text = $"x{goal.goalData.count}";

            gameObject.transform.SetParent(transform, false);
        }

    }

    
    
}
