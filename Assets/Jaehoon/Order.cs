using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class Order
{
    private TileData tile;
    private TetrisUnit unit;

    public Order(TileData tile, TetrisUnit unit)
    {
        this.tile = tile;
        this.unit = unit;
    }

    public TileData GetTile() => tile;
    public TetrisUnit GetUnit() => unit;

}
