using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] Transform UI;
    [SerializeField] GridSystemVisual GridSystemVisual;
    [SerializeField] TutorialBlind tutorialBlind;

    [SerializeField] GameObject bottomBlind;

    [SerializeField] Button CastingButton;
    [SerializeField] SpawnBox spawnBox;

    [SerializeField] GameObject drawLineIcon;

    [SerializeField] SceneChange sceneChange;

    [SerializeField] ManaCollector manaCollector;

    [SerializeField] MainGame mainGame;

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

        TutorialUI tutorialUI = UI.GetComponent<TutorialUI>();

        if (TutorialOrder == 0)
        {
            // 제한시간, 스크롤 설명
            tutorialUI.ShowTimeAndBlockGuide(true);

            tutorialUI.ActiveBackground(true);
            tutorialBlind.ShowObject(0);

            CastingButton.enabled = false;
            spawnBox.enabled = false;
        }
        else if(TutorialOrder == 1)
        {
            // 목표 설명
            tutorialUI.ShowTimeAndBlockGuide(false);

            tutorialBlind.HideObject(0);
            tutorialBlind.ShowObject(2);
        }
        else if(TutorialOrder == 2)
        {
            // 드레그 설명
            tutorialBlind.HideObject(2);

            tutorialUI.ActiveBackground(false);
            tutorialUI.EnableDragText(true);
            tutorialUI.ActivateButton(false);

            ResetDeployable();
            EnableTargetGrid();

            spawnBox.enabled = true;
        }
        else if(TutorialOrder == 3)
        {
            tutorialUI.DisableUI();

            tutorialUI.EnableDragText(false);
            EnableTargetGrid();
        }
        else if(TutorialOrder == 4)
        {
            // 수확 설명
            tutorialUI.ShowTextBox(0);
            tutorialUI.ActiveBackground(true);

            tutorialBlind.ShowObject(4);
        }
        else if(TutorialOrder == 5)
        {
            tutorialUI.DisableUI();

            tutorialBlind.HideObject(4);
            EnableTargetGrid();
        }
        else if(TutorialOrder == 6)
        {
            // 잠금 설명
            tutorialUI.ShowTextBox(1);
        }
        else if(TutorialOrder == 7)
        {
            // 리롤 설명
            tutorialUI.ShowTextBox(3);
            tutorialBlind.ShowObject(1);
        }
        else if(TutorialOrder == 8)
        {
            // 리롤 유도
            tutorialUI.EnableRerollText(true);
            tutorialBlind.HideObject(1);

            tutorialUI.HideTextBox();
            tutorialUI.ActiveBackground(false);
            tutorialUI.ActivateButton(false);

            CastingButton.enabled = true;
            spawnBox.enabled = false;
        }
        else if(TutorialOrder == 9)
        {
            tutorialUI.DisableUI();
            tutorialUI.ActiveBackground(true);
            tutorialUI.EnableRerollText(false);
            EnableTargetGrid();

            CastingButton.enabled = false;
            spawnBox.enabled = true;
        }
        else if(TutorialOrder == 10)
        {
            GridPosition[] pos = new GridPosition[3];
            pos[0] = new GridPosition(1, 1);
            pos[1] = new GridPosition(1, 2);
            pos[2] = new GridPosition(2, 1);

            for (int i = 0; i < 3; i++)
            {
                GridManager.Instance.SetDeployableGrid(pos[i], true);
                EventManager.changeTileData(pos[i]);
            }

            tutorialUI.ShowTextBox(7);
            tutorialUI.ActiveBackground(true);
        }
        else if (TutorialOrder == 11)
        {
            tutorialUI.HideTextBox();

            // 피버 설명
            tutorialUI.EnableUI();
            tutorialUI.EnableFeverText(true);
            tutorialUI.ActivateButton(true);

            manaCollector.Mana = 100;

            tutorialBlind.ShowObject(3);
        }
        else if (TutorialOrder == 12)
        {
            tutorialBlind.HideObject(3);
            tutorialUI.EnableFeverText(false);

            manaCollector.ChargeMana(1);
            manaCollector.Mana = 0;

            // 마나맥 실행, 설명.
            MiniGameController.Instance.ExecuteTutorialMinigame(EMinigameType.DrawLine);

            tutorialUI.ShowTextBox(4);

            drawLineIcon.SetActive(true);
        }
        else if (TutorialOrder == 13)
        {
            tutorialUI.DisableUI();

            drawLineIcon.SetActive(false);

            spawnBox.enabled = false;
        }
        else if (TutorialOrder == 14)
        {
            tutorialUI.ShowTextBox(5);

            // 그리폰 실행
            MiniGameController.Instance.ExecuteTutorialMinigame(EMinigameType.Griffon);
            tutorialUI.ShowTextBox(5);

            mainGame.isPaused = true;
        }
        else if(TutorialOrder == 15)
        {
            tutorialUI.DisableUI();

            mainGame.isPaused = false;
        }
        else if(TutorialOrder == 16)
        {
            tutorialUI.ShowTextBox(6);
        }
        else if(TutorialOrder == 17)
        {
            sceneChange.MoveScene();
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

    public void ClickCastingCancel()
    {
        if(TutorialOrder == 9)
        {
            ProceedStep();
        }
    }
}
