using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance { get; private set; }

    [SerializeField] private Transform gridDebugObjectPrefab;
    [SerializeField] private Transform debugObjectContainer;

    private GridSystem gridSystem;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log("LevelGrid already exist");
            Destroy(gameObject);
            return;
        }

        gridSystem = new GridSystem(5, 5);
        gridSystem.CreateDebugObjcet(gridDebugObjectPrefab, debugObjectContainer);
    }

    public void AddUnitAtGridPosition(GridPosition gridPosition, TileUnit unit)
    {
        GridTile gridObject = gridSystem.GetGridTile(gridPosition);
        gridObject.AddUnit(unit);
    }

    public List<TileUnit> GetUnitListAtGridPosition(GridPosition gridPosition)
    {
        GridTile gridObject = gridSystem.GetGridTile(gridPosition);
        return gridObject.GetUnitList();
    }

    public void RemoveUnitAtGridPosition(GridPosition gridPosition, TileUnit unit)
    {
        GridTile gridObject = gridSystem.GetGridTile(gridPosition);
        gridObject.RemoveUnit(unit);
    }

    public void UnitMovedGridPosition(TileUnit unit, GridPosition fromGridPosition, GridPosition toGridPosition)
    {
        RemoveUnitAtGridPosition(fromGridPosition, unit);
        AddUnitAtGridPosition(toGridPosition, unit);
    }

    public GridPosition GetGridPosition(Vector3 worldPosition) => gridSystem.GetGridPosition(worldPosition);

    public Vector3 GetWorldPosition(GridPosition gridPosition) => gridSystem.GetWorldPosition(gridPosition.x, gridPosition.y);

    public int GetWidth() => gridSystem.GetWidth();

    public int GetHeight() => gridSystem.GetHeight();


    public bool CheckOnGrid(GridPosition gridPosition) => gridSystem.CheckOnGrid(gridPosition);

    public bool CheckOnGrid(Vector3 position) => gridSystem.CheckOnGrid(position);




    //public void SetElement(Element element, GridPosition gridPosition) => gridSystem.SetElement(element, gridPosition);
}
