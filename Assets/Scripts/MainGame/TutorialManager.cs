using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] Transform UI;
    [SerializeField] GridSystemVisual GridSystemVisual;

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
        }
        else if(TutorialOrder == 1)
        {
            UI.GetComponent<TutorialUI>().DisableButton();

            ResetDeployable();
            EnableTargetGrid();
        }
        else if(TutorialOrder == 2)
        {
            EnableTargetGrid();
        }
        else if(TutorialOrder == 3)
        {
            EnableTargetGrid();

            UI.GetComponent<TutorialUI>().ShowTextBox();
        }
        else if(TutorialOrder == 4)
        {
            UI.GetComponent<TutorialUI>().DisableButton();

            EnableTargetGrid();
        }
        else if(TutorialOrder == 5)
        {

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
