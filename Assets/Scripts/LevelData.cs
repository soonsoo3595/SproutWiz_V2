using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class LevelData
{
    private TileData[,] tileDataArray;

    public delegate void ApplyTetris(TetrisObject tetrisObject);
    static public ApplyTetris applyTetris;

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

        foreach (TetrisUnit unit in units)
        {
            TileData tile = GetData(unit.GetGridPosition());
            tile.SetData(unit);
        }
    }

    public TileData GetData(GridPosition gridPosition)
    {
        return tileDataArray[gridPosition.x, gridPosition.y];
    }

}
