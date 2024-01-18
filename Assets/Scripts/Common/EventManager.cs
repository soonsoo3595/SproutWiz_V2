using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EventManager;

public static class EventManager
{
    #region MainGameEvent
    public delegate void TileHarvest(TileData tile);        // 타일에 있는 식물 수확 시
    public static TileHarvest tileHarvest;

    public delegate void HarvestCount(int count);           // 식물 수확 시 몇 개 수확했는지 필요할 때 
    public static HarvestCount harvestCount;

    public delegate void AfterApplyTetris();                // 테트리스 놓았을 때 
    public static AfterApplyTetris afterApplyTetris;

    public delegate void ResetMainGame();                   // 게임 재시작 시 타이머, 점수 등 값 초기화
    public static ResetMainGame resetMainGame;  

    public delegate void MainGameOver();
    public static MainGameOver mainGameOver;

    public delegate void ApplyTetris(TetrisObject tetrisObject);
    public static ApplyTetris applyTetris;

    public delegate void ChangeTileData(GridPosition gridPosition);
    public static ChangeTileData changeTileData;

    public delegate void AddUnitOnGridTile(GridPosition position, TileUnit unit);
    static public AddUnitOnGridTile addUnitOnGridTile;

    public delegate void RemoveUnitOnGridTile(GridPosition position, TileUnit unit);
    static public RemoveUnitOnGridTile removeUnitOnGridTile;
    #endregion

    #region CraftShopEvent
    public delegate void SetSkillInfoPopup(SkillType skillType);
    public static SetSkillInfoPopup setSkillInfoPopup;
    #endregion

    public static void ClearEvents()
    {
        tileHarvest = null;
        harvestCount = null;
        afterApplyTetris = null;
        resetMainGame = null;
        mainGameOver = null;
        applyTetris = null;
        changeTileData = null;
        addUnitOnGridTile = null;
        removeUnitOnGridTile = null;
    }
}
