using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class TileData
{
    private GridPosition gridPosition;
    private Element element;

    public GrowPoint growPoint;
    
    public TileData(GridPosition gridPosition)
    {
        this.gridPosition = gridPosition;

        this.growPoint = GrowPoint.Seed;
        this.element = new Element();
    }

    public override string ToString()
    {
        return element.ToString() + '\n' + growPoint.ToString() + '\n';
    }

    public Element GetElement() => element;
    
    public void InitTile()
    {
        growPoint = GrowPoint.Seed;
        element.Init();
    }
}
