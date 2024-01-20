using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DrawLineGame : MonoBehaviour, IMiniGame
{
    [SerializeField] Transform startPointPrefab;
    [SerializeField] Transform midPointPrefab;

    GridPosition[] pathPointPositions;
    GridPosition[] direction;

    readonly int MaxPathPointCount = 5;

    Transform startPoint;


    private void Awake()
    {
        direction = new GridPosition[4];
        direction[0] = new GridPosition(0, 1);  // 상
        direction[1] = new GridPosition(0, -1); // 하
        direction[2] = new GridPosition(-1, 0); // 좌
        direction[3] = new GridPosition(1, 0);  // 우
    }

    public void Excute()
    {
        Debug.Log("한 붓 그리기 실행!");

        int pathLength = Random.Range(2, MaxPathPointCount);
        pathPointPositions = new GridPosition[pathLength + 1];

        GridPosition startGridPosition = SelectStartPoint();
        Vector3 startPointWorldPos = GridManager.Instance.GetWorldPosition(startGridPosition);
        startPointWorldPos.z = 10;

        pathPointPositions[0] = startGridPosition;

        startPoint = Instantiate(startPointPrefab, startPointWorldPos, Quaternion.identity);

        Debug.Log($"pathLength = {pathLength}");
        Debug.Log($"startPoint = {startGridPosition.x}, {startGridPosition.y}");

        for (int j = 1; j < pathLength; j++)
        {
            GridPosition preGridPosition = pathPointPositions[j - 1];
            GridPosition nextGridPosition = new GridPosition(-1, -1);

            List<GridPosition> validPositions = new List<GridPosition>();

            // 4방향 중 유효한 블록이 몇 개인지 체크
            for (int i = 0; i < 4; i++)
            {
                int nextPosX = preGridPosition.x + direction[i].x;
                int nextPosY = preGridPosition.y + direction[i].y;

                // 맵 사이즈 체크
                if (nextPosX < 0 || nextPosX >= GridManager.Instance.GetWidth())
                    continue;
                if (nextPosY < 0 || nextPosY >= GridManager.Instance.GetHeight())
                    continue;


                nextGridPosition = new GridPosition(nextPosX, nextPosY);

                if (IsDuplicated(nextGridPosition))
                    continue;

                validPositions.Add(nextGridPosition);
            }

            // 유효한 방향 중 랜덤 선택
            int randomDirection = Random.Range(0, validPositions.Count);

            pathPointPositions[j] = validPositions[randomDirection];
        }

        for(int i = 1; i < pathLength; i++)
        {
            Vector3 spawnPointWorldPos = GridManager.Instance.GetWorldPosition(pathPointPositions[i]);
            spawnPointWorldPos.z = 10;

            Instantiate(midPointPrefab, spawnPointWorldPos, Quaternion.identity);

            Debug.Log($"MidPoint : {pathPointPositions[i].x}, {pathPointPositions[i].y}");
        }

        startPoint.GetComponent<StartPoint>().DrawLine(pathPointPositions[0], pathPointPositions[1]);
    }

    private GridPosition SelectStartPoint()
    {
        int gridSizeX = GridManager.Instance.GetWidth();
        int gridSizeY = GridManager.Instance.GetHeight();

        int x = Random.Range(0, gridSizeX);
        int y = Random.Range(0, gridSizeY);

        return new GridPosition(x, y);
    }

    private void SearchNextPoint()
    {

    }

    private bool IsDuplicated(GridPosition position)
    {
        for(int i = 0; i < pathPointPositions.Length; i++)
        {
            if (pathPointPositions.Equals(position))
            {
                return true;
            }
        }

        return false;
    }
}
