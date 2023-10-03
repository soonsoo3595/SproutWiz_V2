using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

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
        this.maxGrowPoint = 6;  // 나중에 maxGrowPoint를 따로 빼서 하드코딩 안하게
        this.element = new Element();
    }

    public override string ToString()
    {
        return element.ToString() + '\n' + growPoint.ToString() + '\n';
    }

    public int GetGrowPoint() => growPoint;
    public void SetGrowPoint(int point)
    {
        growPoint = Mathf.Clamp(growPoint + point, 0, maxGrowPoint);

        if (growPoint >= maxGrowPoint)
        {
            Debug.Log("수확");
            InitTile();
        }
    }

    public Element GetElement() => element;
    
    public void InitTile()
    {
        growPoint = 0;
        element.Init();
    }
}
