using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaFlow : MiniGameBase
{


    public override GridPosition[] Execute()
    {
        return AffectPositions;
    }


    protected GridPosition SelectOriginPoint()
    {
        int gridSizeX = GridManager.Instance.GetWidth();
        int gridSizeY = GridManager.Instance.GetHeight();

        int x = Random.Range(0, gridSizeX);
        int y = Random.Range(0, gridSizeY);

        return new GridPosition(x, y);
    }



    
}
