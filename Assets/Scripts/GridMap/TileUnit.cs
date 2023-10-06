using UnityEngine;

public class TileUnit : MonoBehaviour
{
    private GridPosition gridPosition;
    private bool onGrid;

    private void Awake()
    {
        onGrid = false;
    }

    private void Start()
    {
        CheckEnterGrid();
    }

    private void Update()
    {
        if (onGrid)
        {
            CheckMoveGridPosition();
        }
        else
        {
            CheckEnterGrid();
        }

        onGrid = GridManager.Instance.CheckOnGrid(transform.position);
    }

    private void OnDestroy()
    {
        ExitGrid();
    }


    private void CheckMoveGridPosition()
    {
        GridPosition newGridPosition = GridManager.Instance.GetGridPosition(transform.position);

        if (newGridPosition != gridPosition)
        {
            MoveGridPosition(newGridPosition);
        }
    }

    protected virtual void MoveGridPosition(GridPosition newGridPosition)
    {
        bool isMoveInGrid = GridManager.Instance.CheckOnGrid(newGridPosition);

        if (isMoveInGrid)
        {
            MoveTo(newGridPosition);
        }
        else
        {
            ExitGrid();
        }
    }

    protected void MoveTo(GridPosition newGridPosition)
    {
        GridManager.Instance.UnitMovedGridPosition(this, gridPosition, newGridPosition);

        gridPosition = newGridPosition;
    }

    protected void ExitGrid()
    {
        GridManager.Instance.RemoveUnitAtGridPosition(gridPosition, this);
    }

    private void CheckEnterGrid()
    {
        gridPosition = GridManager.Instance.GetGridPosition(transform.position);

        if (GridManager.Instance.CheckOnGrid(gridPosition))
            GridManager.Instance.AddUnitAtGridPosition(gridPosition, this);
    }


    public bool GetOnGrid()
    {
        return onGrid;
    }

    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }
}