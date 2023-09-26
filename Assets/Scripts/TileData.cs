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
        this.maxGrowPoint = 6;  // ���߿� maxGrowPoint�� ���� ���� �ϵ��ڵ� ���ϰ�
        this.element = new Element();
    }

    public override string ToString()
    {
        return element.ToString() + '\n' + growPoint.ToString() + '\n';
    }

    public int GetGrowPoint() => growPoint;
    public void SetGrowPoint(int point) => growPoint = point; 
    public void InitTile()
    {
        growPoint = 0;
        element.Init();
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
            growPoint = element.GetElementRelation(unit.GetElement()) switch
            {
                ElementRelation.Advantage => Mathf.Clamp(growPoint + unit.GetGrowPoint() * 2, 0, maxGrowPoint),
                ElementRelation.Equal => Mathf.Clamp(growPoint + unit.GetGrowPoint(), 0, maxGrowPoint),
                ElementRelation.Disadvantage => 0,
                _ => growPoint
            };
        }

        UpdateTile();
    }

    // ���� �ʿ�!!!!!
    public void UpdateTile()
    {
        if(growPoint == 0)
        {
            InitTile();
        }
        else if(growPoint == maxGrowPoint)
        {
            Debug.Log(gridPosition.ToString() + "��Ȯ");
            InitTile();
        }
        
    }
}
