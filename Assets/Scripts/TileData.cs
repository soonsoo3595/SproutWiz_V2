using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Element
{
    None,
    Fire,
    Warter,
    Grass
}


public class TileData
{
    private GridPosition gridPosition;

    public int growPoint;
    public int maxGrowPoint;
    public Element element;

    public TileData(GridPosition gridPosition)
    {
        this.gridPosition = gridPosition;
        this.growPoint = 0;
        this.maxGrowPoint = 0;
        this.element = Element.None;
    }

    public override string ToString()
    {
        return element.ToString() + '\n' + growPoint.ToString();
    }
}
