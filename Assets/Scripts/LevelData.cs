using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData
{
    private TileData[,] tileDataArray;

    public delegate void ApplyTetris(TetrisObject tetrisObject);
    public static ApplyTetris applyTetris;

    public delegate void ChangeTileData(GridPosition gridPosition);
    public static ChangeTileData changeTileData;

    public delegate void CheckAchieveGoal();
    public static CheckAchieveGoal checkAchieveGoal;

    public delegate void AddHarvestScore(int count);
    public static AddHarvestScore addHarvestScore;
    
    public LevelData(int width, int height) 
    {
        InitGridData(width, height);

        applyTetris += SetTileData;
    }

    private void InitGridData(int width, int height)
    {
        tileDataArray = new TileData[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                tileDataArray[x, y] = new TileData(new GridPosition(x, y));
            }
        }
    }
    
    
    private void SetTileData(TetrisObject tetrisObject)
    {
        List<TetrisUnit> units = tetrisObject.GetUnitList();

        int count = 0;

        foreach (TetrisUnit unit in units)
        {
            TileData tile = GetData(unit.GetGridPosition());
            if (TileFactory.Instance.MakeOrder(tile, unit)) count++;

            // 값 갱신이 없으면 실행하지 말아야함.
            changeTileData(unit.GetGridPosition());
        }
        
        addHarvestScore(count);
        checkAchieveGoal();
    }

    public TileData GetData(GridPosition gridPosition)
    {
        return tileDataArray[gridPosition.x, gridPosition.y];
    }

    public void ResetData(int width, int height)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                tileDataArray[x, y].InitTile();
                changeTileData(new GridPosition(x, y));
            }
        }
    }

}
