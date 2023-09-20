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

    public void InitTile()
    {
        growPoint = 0;
        element.InitElement();
    }

    public void SetData(TetrisUnit unit)
    {
        if (element.IsNone())
        {
            element.SetElementType(unit.GetElement().GetElementType());
            growPoint++;
        }
        else
        {
            switch (element.GetElementRelation(unit.GetElement()))
            {
                case ElementRelation.Advantage:
                    growPoint = Mathf.Clamp(growPoint + unit.GetGrowPoint() * 2, 0, maxGrowPoint);
                    break;
                case ElementRelation.Equal:
                    growPoint = Mathf.Clamp(growPoint + unit.GetGrowPoint(), 0, maxGrowPoint);
                    break;
                case ElementRelation.Disadvantage:
                    growPoint = 0;
                    break;
            }
        }

        UpdateTile();
    }

    // 변경 필요!!!!!
    public void UpdateTile()
    {
        if(growPoint == 0)
        {
            InitTile();
        }
        else if(growPoint == maxGrowPoint)
        {
            Debug.Log(gridPosition.ToString() + "수확");
            InitTile();
        }
        
    }
}
