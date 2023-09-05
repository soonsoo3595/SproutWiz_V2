using UnityEngine;

public class GridSystem
{
    private int width;
    private int height;
    private GridTile[,] gridTileArray;

    private GridTile OutOfGrid;

    public GridSystem(int width, int height)
    {
        this.width = width;
        this.height = height;

        // 맵 밖에 있는 경우 null값 대신 임시GridPosition 반환
        OutOfGrid = new GridTile(new GridPosition(-100, -100));

        gridTileArray = new GridTile[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                gridTileArray[x, y] = new GridTile(new GridPosition(x, y));
            }
        }
    }

    public void CreateDebugObjcet(Transform debugPrefab, Transform container)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Transform debugTransform = Transform.Instantiate(debugPrefab, GetWorldPosition(x, y), Quaternion.identity);
                debugTransform.SetParent(container);

                GridDebugObject gridDebugObject = debugTransform.GetComponent<GridDebugObject>();
                gridDebugObject.SetGridObject(GetGridTile(new GridPosition(x, y)));
            }
        }
    }

    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y, 0);
    }

    public Vector3 GetWorldPosition(GridPosition gridPosition)
    {
        return GetWorldPosition(gridPosition.x, gridPosition.y);
    }

    public GridPosition GetGridPosition(Vector3 worldPosition)
    {
        return new GridPosition(Mathf.RoundToInt(worldPosition.x), Mathf.RoundToInt(worldPosition.y));
    }

    public GridTile GetGridTile(GridPosition gridPosition)
    {
        if (CheckOnGrid(gridPosition))
        {
            return gridTileArray[gridPosition.x, gridPosition.y];
        }
        else
        {
            Debug.Log($"Out Of Grid  {gridPosition}");
            return OutOfGrid;
        }

    }

    public int GetWidth()
    {
        return width;
    }

    public int GetHeight()
    {
        return height;
    }

    public bool CheckOnGrid(GridPosition gridPosition)
    {
        if (gridPosition >= (0, 0) && gridPosition < (width, height))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckOnGrid(Vector3 position)
    {
        GridPosition gridPosition = GetGridPosition(position);

        return CheckOnGrid(gridPosition);
    }
}
