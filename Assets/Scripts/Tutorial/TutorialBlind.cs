using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBlind : MonoBehaviour
{
    [SerializeField] GameObject timeBar;
    [SerializeField] GameObject Block;
    [SerializeField] GameObject Cast;

    [SerializeField] RectTransform OriginTimeBar;

    public void ShowObject(int index)
    {
        switch (index)
        {
            case 0:
                timeBar.SetActive(true);
                Block.SetActive(true);

                timeBar.GetComponent<RectTransform>().position = OriginTimeBar.position;
                break;
            case 1:
                Cast.SetActive(true);
                break;
        }
    }

    public void HideObject(int index)
    {
        switch(index)
        {
            case 0:
               timeBar.SetActive(false);
               Block.SetActive(false);
               break;
            case 1:
                Cast.SetActive(false);
                break;
        }
    }
}
