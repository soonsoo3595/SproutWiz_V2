using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance { get; private set; }

    [SerializeField] private GameSetting gameSetting;
    [SerializeField] private Transform gridDebugObjectPrefab;
    [SerializeField] private Transform debugObjectContainer;

    private GridSystem gridSystem;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("LevelGrid already exist");
            Destroy(gameObject);
            return;
        }

        gridSystem = new GridSystem(gameSetting.GridMapWidth, gameSetting.GridMapHeight);

        // 디버그 오브젝트.
        //gridSystem.CreateDebugObjcet(gridDebugObjectPrefab, debugObjectContainer);

        EventManager.resetMainGame += gridSystem.ResetGridTile;
    }

    public void AddUnitAtGridPosition(GridPosition gridPosition, TileUnit unit)
    {
        GridTile gridObject = gridSystem.GetGridTile(gridPosition);

        EventManager.addUnitOnGridTile(gridPosition, unit);

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

        if(EventManager.removeUnitOnGridTile != null)
            EventManager.removeUnitOnGridTile(gridPosition, unit);

        gridObject.RemoveUnit(unit);
    }

    public void UnitMovedGridPosition(TileUnit unit, GridPosition fromGridPosition, GridPosition toGridPosition)
    {
        RemoveUnitAtGridPosition(fromGridPosition, unit);
        AddUnitAtGridPosition(toGridPosition, unit);
    }

    public GridPosition GetGridPosition(Vector3 worldPosition) => gridSystem.GetGridPosition(worldPosition);

    public Vector3 GetWorldPosition(GridPosition gridPosition) => gridSystem.GetWorldPosition(gridPosition);

    public int GetWidth() => gridSystem.GetWidth();

    public int GetHeight() => gridSystem.GetHeight();


    public bool CheckOnGrid(GridPosition gridPosition) => gridSystem.CheckOnGrid(gridPosition);

    public bool CheckOnGrid(Vector3 position) => gridSystem.CheckOnGrid(position);


    public bool CheckDeployableGrid(GridPosition gridPosition) => gridSystem.CheckDeployableGrid(gridPosition);

    public void SetDeployableGrid(GridPosition gridPosition, bool state) => gridSystem.SetDeployableGrid(gridPosition, state);

    public void ResetDeployableGrid() => gridSystem.ResetDeployableGrid();


    public TileData GetTileData(GridPosition position) => gridSystem.GetTileData(position);

    public GameSetting GetSetting() => gameSetting;

    
}
