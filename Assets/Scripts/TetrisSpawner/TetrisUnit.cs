using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TetrisUnit : TileUnit
{
    Element element;

    private void Start()
    {
        SetBlockColor();
    }

    public void SetUnitState(Element element)
    {
        this.element = element;
    }
    
    public Element GetElement()
    {
        return element;
    }

    protected override void MoveGridPosition(GridPosition newGridPosition)
    {
        //base.MoveGridPosition(newGridPosition);

        bool isMoveInGrid = GridManager.Instance.CheckOnGrid(newGridPosition);
        bool isEmptyTile = false;
        
        if (GridManager.Instance.GetUnitListAtGridPosition(newGridPosition).Count < 1)
            isEmptyTile = true;
        

        if (isMoveInGrid && isEmptyTile)
        {
            MoveTo(newGridPosition);
        }
        else if(isMoveInGrid && !isEmptyTile)
        {

        }
        else
        {
            ExitGrid();
        }
    }


    private void SetBlockColor()
    {
        Color blockColor = new Color();

        switch (element.GetElementType())
        {
            case ElementType.None:
                blockColor = Color.white;
                break;
            case ElementType.Fire:
                blockColor = Color.red;
                break;
            case ElementType.Water:
                blockColor = Color.blue;
                break;
            case ElementType.Grass:
                blockColor = Color.green;
                break;
        }

        blockColor.a *= 0.7f;

        GetComponentInChildren<SpriteRenderer>().color = blockColor;
    }
}
