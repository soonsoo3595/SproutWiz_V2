using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EventManager;

public static class EventManager
{
    public delegate void TileHarvest(TileData tile);        // 타일에 있는 식물 수확 시
    public static TileHarvest tileHarvest;

    public delegate void HarvestCount(int count);
    public static HarvestCount harvestCount;

    public delegate void AfterApplyTetris();
    public static AfterApplyTetris afterApplyTetris;

    public delegate void ResetMainGame();
    public static ResetMainGame resetMainGame;

    public delegate void TimeOver();
    public static TimeOver timeOver;

    public delegate void ApplyTetris(TetrisObject tetrisObject);
    public static ApplyTetris applyTetris;

    public delegate void ChangeTileData(GridPosition gridPosition);
    public static ChangeTileData changeTileData;

    public delegate void AddUnitOnGridTile(GridPosition position, TileUnit unit);
    static public AddUnitOnGridTile addUnitOnGridTile;

    public delegate void RemoveUnitOnGridTile(GridPosition position, TileUnit unit);
    static public RemoveUnitOnGridTile removeUnitOnGridTile;
}
