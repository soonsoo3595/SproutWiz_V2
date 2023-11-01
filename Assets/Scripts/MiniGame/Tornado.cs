using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : MiniGameBase
{
    GridPosition originPosition;

    public Tornado()
    {
        originPosition = new GridPosition(1, 1);
        AffectPositions = new GridPosition[9];
    }

    public override GridPosition[] Execute()
    {
        SelectOriginPoint();
        CalculateAffectPosition();

        Debug.Log("Tornado");

        return AffectPositions;
    }


    private void CalculateAffectPosition()
    {
        int count = 0;

        for(int x = originPosition.x - 1; x < originPosition.x + 2; x++)
        {
            for(int y = originPosition.y - 1; y < originPosition.y + 2; y++) 
            {
                AffectPositions[count] = new GridPosition(x, y);
                count++;
            }
        }
    }

    private void SelectOriginPoint()
    {
        int gridSizeX = GridManager.Instance.GetWidth();
        int gridSizeY = GridManager.Instance.GetHeight();


        // TODO: 발생 불가 지점 예외처리 필요함.
        int x = Random.Range(0 + 1, gridSizeX - 1);
        int y = Random.Range(0 + 1, gridSizeY - 1);

        originPosition = new GridPosition(x, y);
    }


}
