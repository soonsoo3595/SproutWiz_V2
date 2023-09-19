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
    public GrowthStep step;

    public TileData(GridPosition gridPosition)
    {
        this.gridPosition = gridPosition;
        this.growPoint = 0;
        this.maxGrowPoint = 6;  // 나중에 maxGrowPoint를 따로 빼서 하드코딩 안하게
        this.element = new Element();
        this.step = GrowthStep.Empty;
    }

    public override string ToString()
    {
        return element.ToString() + '\n' + growPoint.ToString() + '\n' + step.ToString();
    }

    public void InitTile()
    {
        growPoint = 0;
        step = GrowthStep.Empty;
    }

    public void SetData(TetrisUnit unit)
    {
        if (step == GrowthStep.Empty)
        {
            element = unit.GetElement();
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
        else if(growPoint == 1)
        {
            step = GrowthStep.Sprout;
        }
        else if(growPoint > 1 && growPoint < maxGrowPoint)
        {
            step = GrowthStep.GrowUp;
        }
        else if(growPoint == maxGrowPoint)
        {
            step = GrowthStep.Harvest;
            InitTile();
        }
        
    }
}
