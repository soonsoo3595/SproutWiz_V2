using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] Transform UI;
    [SerializeField] GridSystemVisual GridSystemVisual;
    [SerializeField] TutorialBlind tutorialBlind;

    private int TutorialOrder;
    private int TetrisOrder;

    (int x, int y)[][] positions = new (int x, int y)[][] 
    {
        new[] { (1, 1), (2, 1), (3, 1), (1, 2) },
        new[] { (2, 1), (3, 1), (2, 2), (3, 2) },
        new[] { (0, 1), (1, 1), (2, 1), (1, 2) },
        new[] { (0, 0), (0, 1), (0, 2) }
    };

    int gridWidth;
    int gridHeight;

    private void Start()
    {
        TutorialOrder = 0;
        TetrisOrder = 0;

        gridWidth = GridManager.Instance.GetWidth();
        gridHeight = GridManager.Instance.GetHeight();

        EventManager.applyTetris += ApplyTetris;
    }

    public void ProceedStep()
    {
        // 다른 기능 터치효과 막아야함.

        Debug.Log($"튜토리얼 번호 : {TutorialOrder}");

        if(TutorialOrder == 0)
        {
            UI.GetComponent<TutorialUI>().ShowTimeAndBlockGuide();

            tutorialBlind.ShowObject(0);
        }
        else if(TutorialOrder == 1)
        {
            UI.GetComponent<TutorialUI>().DisableBackground();
            tutorialBlind.HideObject(0);

            ResetDeployable();
            EnableTargetGrid();
        }
        else if(TutorialOrder == 2)
        {
            EnableTargetGrid();
        }
        else if(TutorialOrder == 3)
        {
            // 수확 설명
            UI.GetComponent<TutorialUI>().ShowTextBox(0);
        }
        else if(TutorialOrder == 4)
        {
            UI.GetComponent<TutorialUI>().DisableBackground();

            EnableTargetGrid();
        }
        else if(TutorialOrder == 5)
        {
            // 잠금 설명
            UI.GetComponent<TutorialUI>().ShowTextBox(1);
        }
        else if(TutorialOrder == 6)
        {
            // 리롤 설명
            UI.GetComponent<TutorialUI>().ShowTextBox(3);
            tutorialBlind.ShowObject(1);
        }
        else if(TutorialOrder == 7)
        {
            UI.GetComponent<TutorialUI>().DisableBackground();
            EnableTargetGrid();

            tutorialBlind.HideObject(1);
        }
        else if(TutorialOrder == 8)
        { 
            // 마나맥 실행
            MiniGameController.Instance.ExecuteTutorialMinigame(EMinigameType.DrawLine);

            UI.GetComponent<TutorialUI>().ShowTextBox(4);
        }
        else if(TutorialOrder == 9)
        {
            UI.GetComponent<TutorialUI>().DisableBackground();
        }
        else if (TutorialOrder == 10)
        {
            UI.GetComponent<TutorialUI>().ShowTextBox(5);

            // 그리폰 실행
            MiniGameController.Instance.ExecuteTutorialMinigame(EMinigameType.Griffon);
            UI.GetComponent<TutorialUI>().ShowTextBox(5);

        }
        else if (TutorialOrder == 11)
        {
            UI.GetComponent<TutorialUI>().DisableBackground();
        }
        else if (TutorialOrder == 12)
        {
            UI.GetComponent<TutorialUI>().ShowTextBox(6);
        }

            TutorialOrder++;
    }

    private void EnableTargetGrid()
    {
        foreach ((int x, int y) in positions[TetrisOrder])
        {
            GridPosition pos = new GridPosition(x, y);

            GridManager.Instance.SetDeployableGrid(pos, true);

            GridSystemVisual.SetBlinking(true, pos);
        }

        TetrisOrder++;
    }

    public void ResetDeployable()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                GridManager.Instance.SetDeployableGrid(new GridPosition(x, y), false);
            }
        }
    }

    public void DisableDeployable(GridPosition pos)
    {
        GridManager.Instance.SetDeployableGrid(pos, false);
    }

    private void ApplyTetris(TetrisObject tetrisObject)
    {
        ProceedStep();
    }
}
