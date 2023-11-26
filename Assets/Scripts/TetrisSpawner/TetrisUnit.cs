using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TetrisUnit : TileUnit
{
    [SerializeField] SpriteRenderer unitSprite;
    [SerializeField] SpriteSetting SpriteSet;

    Element element;

    private void Start()
    {
        SetBlockSprite();
        unitSprite.transform.rotation = Quaternion.identity;
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


    private void SetBlockSprite()
    {
        switch (element.GetElementType())
        {
            case ElementType.None:
                break;
            case ElementType.Fire:
                unitSprite.sprite = SpriteSet.FireTetris;
                break;
            case ElementType.Water:
                unitSprite.sprite = SpriteSet.WaterTetris;
                break;
            case ElementType.Grass:
                unitSprite.sprite = SpriteSet.GrassTetris;
                break;
        }
    }
}
