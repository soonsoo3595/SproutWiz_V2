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
    public void SetGrowPoint(int point)
    {
        growPoint = Mathf.Clamp(growPoint + point, 0, maxGrowPoint);

        if (growPoint >= maxGrowPoint)
        {
            Debug.Log("��Ȯ");
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
