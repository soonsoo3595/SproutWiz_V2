using UnityEngine;

public class TileUnit : MonoBehaviour
{
    private GridPosition gridPosition;
    private bool onGrid;

    private bool isDeployable;

    private void Awake()
    {
        onGrid = false;
        isDeployable = false;
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
            CheckDeployableGrid();
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

        CheckDeployableGrid();
    }

    private void CheckDeployableGrid()
    {
        if (!onGrid) return;

        isDeployable = GridManager.Instance.CheckDeployableGrid(gridPosition);
    }


    public bool GetOnGrid()
    {
        return onGrid;
    }

    public bool GetDeployable()
    {
        return isDeployable;
    }

    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }
}