using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EventManager;

public static class EventManager
{
    #region MainGameEvent
    public delegate void TileHarvest(TileData tile);        // Ÿ�Ͽ� �ִ� �Ĺ� ��Ȯ ��
    public static TileHarvest tileHarvest;

    public delegate void HarvestCount(int count);           // �Ĺ� ��Ȯ �� �� �� ��Ȯ�ߴ��� �ʿ��� �� 
    public static HarvestCount harvestCount;

    public delegate void AfterApplyTetris();                // ��Ʈ���� ������ �� 
    public static AfterApplyTetris afterApplyTetris;

    public delegate void ResetMainGame();                   // ���� ����� �� Ÿ�̸�, ���� �� �� �ʱ�ȭ
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
