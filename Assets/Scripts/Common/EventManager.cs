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

    public delegate void RecordUpdate(RecordType type, int count = 1);
    public static RecordUpdate recordUpdate;

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

    public delegate void MiniGameSuccess(EMinigameType type, int index);
    static public MiniGameSuccess miniGameSuccess;
    #endregion

    #region CraftShopEvent
    public delegate void SetSkillInfoPopup(SkillElement skillElement);
    public static SetSkillInfoPopup setSkillInfo;

    public delegate void UpdateUI();
    public static UpdateUI updateUI;
    #endregion

    public static void ClearMainGameEvents()
    {
        tileHarvest = null;
        harvestCount = null;
        afterApplyTetris = null;
        recordUpdate = null;
        resetMainGame = null;
        mainGameOver = null;
        applyTetris = null;
        changeTileData = null;
        addUnitOnGridTile = null;
        removeUnitOnGridTile = null;
        miniGameSuccess = null;
    }
}
