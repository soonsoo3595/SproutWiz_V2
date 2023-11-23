using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData
{
    private TileData[,] tileDataArray;

    public LevelData(int width, int height) 
    {
        InitGridData(width, height);

        EventManager.applyTetris += SetTileData;
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

            // �� ������ ������ �������� ���ƾ���.
            EventManager.changeTileData(unit.GetGridPosition());
        }

        EventManager.harvestCount(count);
        EventManager.afterApplyTetris();
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
                EventManager.changeTileData(new GridPosition(x, y));
            }
        }
    }

}
