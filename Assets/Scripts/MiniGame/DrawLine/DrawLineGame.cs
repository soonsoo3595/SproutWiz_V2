using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

// TODO : ������Ʈ Ǯ�� ������� ��ü ���.

public class DrawLineGame : MonoBehaviour, IMiniGame
{
    [SerializeField] Transform startPointPrefab;
    [SerializeField] Transform midPointPrefab;
    [SerializeField] RectTransform CanvasWorldSpace;

    [SerializeField] int ActivationScore = 3000;
    [SerializeField] float IntervalTime = 60f;
    [SerializeField] float MinimumTerm = 20f;

    float RecentExcuteTimeInIntervar;

    readonly static int MaxPathPointCount = 5;
    static GridPosition[] direction;

    List<GridPosition> pathPointPositions;

    Transform[] drawPointsObject;
    int pathLength;


    // �巡�� �̺�Ʈ ����.
    bool isDrag = false;
    int CurrentDragSequence = 0;

    // TODO: ������ ���� �ʿ�
    int IMiniGame.ActivationScore => ActivationScore;

    float IMiniGame.IntervalTime => IntervalTime;

    float IMiniGame.MinimumTerm => MinimumTerm;

    bool isActivate;
    bool IMiniGame.IsActivate => isActivate;

    bool isRunnig;
    bool IMiniGame.IsRunnig => isRunnig;

    private void Awake()
    {
        direction = new GridPosition[4];
        direction[0] = new GridPosition(0, 1);  // ��
        direction[1] = new GridPosition(0, -1); // ��
        direction[2] = new GridPosition(-1, 0); // ��
        direction[3] = new GridPosition(1, 0);  // ��

        isActivate = false;
        isRunnig = false;
    }

    private void Start()
    {
        RecentExcuteTimeInIntervar = Random.Range(0, IntervalTime);
        Debug.Log($"������ ���� ����ð� : {RecentExcuteTimeInIntervar}");
    }

    public void Excute()
    {
        Debug.Log("�� �� �׸��� ����!");

        pathLength = Random.Range(2, MaxPathPointCount);
        pathPointPositions = new List<GridPosition>();
        drawPointsObject = new Transform[pathLength];

        GridPosition startGridPosition = SelectStartPoint();
        Vector3 startPointWorldPos = GridManager.Instance.GetWorldPosition(startGridPosition);
        startPointWorldPos.z = 10;

        pathPointPositions.Add(startGridPosition);

        drawPointsObject[0] = Instantiate(startPointPrefab, startPointWorldPos, Quaternion.identity, CanvasWorldSpace);
        StartPoint startPoint = drawPointsObject[0].GetComponent<StartPoint>();
        startPoint.SetMaster(this);

        Debug.Log($"pathLength = {pathLength}");
        Debug.Log($"startPoint = {startGridPosition.x}, {startGridPosition.y}");

        for (int j = 1; j < pathLength; j++)
        {
            GridPosition preGridPosition = pathPointPositions[pathPointPositions.Count - 1];

            List<GridPosition> validPositions = new List<GridPosition>();
            
            // 4���� �� ��ȿ�� ����� �� ������ üũ
            for (int i = 0; i < 4; i++)
            {
                //Debug.Log($"4���� üũ ����");
                int nextPosX = preGridPosition.x + direction[i].x;
                int nextPosY = preGridPosition.y + direction[i].y;

                // �� ������ üũ
                if (nextPosX < 0 || nextPosX >= GridManager.Instance.GetWidth())
                    continue;
                if (nextPosY < 0 || nextPosY >= GridManager.Instance.GetHeight())
                    continue;


                GridPosition nextGridPosition = new GridPosition(nextPosX, nextPosY);

                if (IsDuplicated(nextGridPosition))
                    continue;

                validPositions.Add(nextGridPosition);
                //Debug.Log($"validPositions Add = {nextGridPosition.x}, {nextGridPosition.y}");
            }

            // ��ȿ�� ���� �� ���� ����
            int randomDirection = Random.Range(0, validPositions.Count);

            pathPointPositions.Add(validPositions[randomDirection]);

            //Debug.Log($"{j}��° ���õ� ��ǥ = {validPositions[randomDirection].x}, {validPositions[randomDirection].y}");
        }

        for(int i = 1; i < pathPointPositions.Count; i++)
        {
            Vector3 spawnPointWorldPos = GridManager.Instance.GetWorldPosition(pathPointPositions[i]);
            spawnPointWorldPos.z = 10;

            drawPointsObject[i] = Instantiate(midPointPrefab, spawnPointWorldPos, Quaternion.identity, CanvasWorldSpace);
            
            DrawPoint drawPoint = drawPointsObject[i].GetComponent<DrawPoint>();
            drawPoint.DrawLine(pathPointPositions[i - 1], pathPointPositions[i]);
            drawPoint.SetMaster(this);

            //Debug.Log($"MidPoint : {pathPointPositions[i]}");
        }

        isRunnig = true;
    }

    private GridPosition SelectStartPoint()
    {
        int gridSizeX = GridManager.Instance.GetWidth();
        int gridSizeY = GridManager.Instance.GetHeight();

        int x = Random.Range(0, gridSizeX);
        int y = Random.Range(0, gridSizeY);

        return new GridPosition(x, y);
    }

    private bool IsDuplicated(GridPosition position)
    {
        if (pathPointPositions.Contains(position))
        {
            return true;
        }

        return false;
    }

    public void SetIsDrag(bool isDrag)
    {
        this.isDrag = isDrag;

        if(isDrag)
        {

        }
        else
        {
            if(CurrentDragSequence == pathLength)
            {
                Debug.Log($"DrawGame Success");
                //Exit();
            }
            else
            {
                Debug.Log($"DrawGame Fail");
            }
        }

        CurrentDragSequence = 1;
    }

    public void EnterDrawPoint(GridPosition position)
    {
        //Debug.Log($"CurrentDragSequence : {CurrentDragSequence}");
        //Debug.Log($"Compair Position : {pathPointPositions[CurrentDragSequence]} <-> {position}");

        if (position.Equals(pathPointPositions[CurrentDragSequence]))
        {
            if (CurrentDragSequence == pathLength - 1)
            {
                Debug.Log($"Enter Last DrawPoint : {CurrentDragSequence} / {pathLength - 1}");
                MiniGameController.Instance.ExitMiniGame(this);
            }

            Debug.Log($"CurrentDragSequence : {CurrentDragSequence} / {pathLength - 1}");

            CurrentDragSequence++;
        }
    }

    public void Exit()
    {
        if (drawPointsObject.Length <= 0)
            return;

        UpdateExcuteTime();

        for (int i = 0; i < pathLength; i++)
        {
            Destroy(drawPointsObject[i].gameObject);
        }

        isRunnig = false;
    }

    public void Activate(float activateTime)
    {
        if (isActivate)
            return;

        Debug.Log("�̴ϰ���:������ ���� �޼�");

        isActivate = true;

        UpdateExcuteTime();
    }

    public bool GetAcivate()
    {
        return isActivate;
    }

    private void UpdateExcuteTime()
    {
        float randomExcuteTime = Random.Range(0, IntervalTime);

        float term = (MinimumTerm - RecentExcuteTimeInIntervar) + randomExcuteTime;

        if (term < MinimumTerm)
        {
            RecentExcuteTimeInIntervar = randomExcuteTime + term;
        }
        else
        {
            RecentExcuteTimeInIntervar = randomExcuteTime;
        }

        Debug.Log($"������ ���� ���� �ð� : {RecentExcuteTimeInIntervar}");
    }

    public float GetNextExcuteTime()
    {
        return RecentExcuteTimeInIntervar;
    }

}
