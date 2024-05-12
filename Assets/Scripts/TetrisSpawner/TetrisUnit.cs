using UnityEngine;

public class TetrisUnit : TileUnit
{
    [SerializeField] SpriteRenderer unitSprite;
    [SerializeField] SpriteSetting SpriteSet;

    Element element;

    [SerializeField] ElementType ElementType;

    private void Awake()
    {
        element = new Element(ElementType);
    }

    private void Start()
    {
        unitSprite.transform.rotation = Quaternion.identity;

        SetBlockSprite(false);
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


    public void SetBlockSprite(bool isDrag)
    {
        if(!isDrag)
        {
            switch (element.GetElementType())
            {
                case ElementType.None:
                    break;
                case ElementType.Fire:
                    unitSprite.sprite = SpriteSet.opacityFireTetris;
                    break;
                case ElementType.Water:
                    unitSprite.sprite = SpriteSet.opacityWaterTetris;
                    break;
                case ElementType.Grass:
                    unitSprite.sprite = SpriteSet.opacityGrassTetris;
                    break;
            }
        }
        else
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
}
