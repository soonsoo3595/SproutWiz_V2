using DG.Tweening;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UIElements;

public class GridSystemVisual : MonoBehaviour
{
    [SerializeField] private Transform gridTilePrefab;
    [SerializeField] private SpriteSetting SpriteSet;

    private Transform[,] tileVisuals;

    readonly private Color DefualtOutLineColor = Color.white;
    readonly private Color DisadvantageTileColor = new Color(0.5f, 0.6f, 0.55f);

    private void Start()
    {
        tileVisuals = new Transform[GridManager.Instance.GetWidth(), GridManager.Instance.GetHeight()];

        for (int x = 0; x < GridManager.Instance.GetWidth(); x++)
        {
            for (int y = 0; y < GridManager.Instance.GetHeight(); y++)
            {
                GridPosition gridPosition = new GridPosition(x, y);

                Vector3 InitPosition = GridManager.Instance.GetWorldPosition(gridPosition);
                InitPosition.z = this.transform.localPosition.z;

                tileVisuals[x,y] = Instantiate(gridTilePrefab, InitPosition, Quaternion.identity, this.transform);
                
            }
        }
        UpdataAllVisuals();

        DefualtOutLineColor = Color.white;

        EventManager.changeTileData += UpdateVisual;
        EventManager.addUnitOnGridTile += AddUnit;
        EventManager.removeUnitOnGridTile += RemoveUnit;
    }

    // 타일값 갱신 이후 호출되야 함.
    private void UpdateVisual(GridPosition position)
    {
        if(position == (-1, -1))
        {
            UpdataAllVisuals();
            Debug.Log("모든 타일 비주얼 업데이트!");

            return;
        }

        ChangeSprite(position);
    }

    private void UpdataAllVisuals()
    {
        for (int x = 0; x < GridManager.Instance.GetWidth(); x++)
        {
            for (int y = 0; y < GridManager.Instance.GetHeight(); y++)
            {
                GridPosition position = new GridPosition(x, y);
                ChangeSprite(position);
            }
        }
    }


    private void ChangeSprite(GridPosition position)
    {
        GridTileVisual visual = tileVisuals[position.x, position.y].GetComponent<GridTileVisual>();

        TileData targetTile = GridManager.Instance.GetTileData(position);

        Element targetElement = targetTile.GetElement();
        GrowPoint growPoint = targetTile.growPoint;

        // TODO: 애니메이션 기능은 가능하면 분리 예정.
        if (growPoint == GrowPoint.Harvest)
        {
            visual.PlayAnimHarvest();
            visual.PlayHarvestEffect(targetElement.GetElementType());
        }
        else
        {
            visual.ResetAnimation();
        }

        if (!GridManager.Instance.CheckDeployableGrid(position))
        {
            visual.SetCropSptire(SpriteSet.LockTile);
            visual.PlayDeadEffect(targetElement.GetElementType());

            return;
        }
        else
        {
            visual.SetCropSptire(null);
        }


        // TODO: 타일, 아웃라인 컬러 부분 전체적으로 함수 추출 필요.
        visual.SetCropSptire(CropSprite(targetElement, growPoint));
        visual.SetTileColor(ElementColor(targetElement));
        visual.SetOutLineAlpha(0f);
    }

    private void AddUnit(GridPosition position, TileUnit unit)
    {
        if (!GridManager.Instance.CheckOnGrid(position)) return;

        GridTileVisual visual = tileVisuals[position.x, position.y].GetComponent<GridTileVisual>();

        // TODO: 다른 타입 유형일 경우 처리 필요.
        TetrisUnit tetrisUnit = unit as TetrisUnit;
        Element element = GridManager.Instance.GetTileData(position).GetElement();
        ElementRelation relation = element.GetElementRelation(tetrisUnit.GetElement());
           

        if (element.GetElementType() == ElementType.None)
        {
            visual.SetOutLineColor(DefualtOutLineColor);
            visual.SetOutLineAlpha(0.5f);

            visual.ResetTileColor();
        }


        switch (relation)
        {
            case ElementRelation.Equal:
                visual.SetTileColor(ElementColor(element));
                visual.SetOutLineColor(DefualtOutLineColor);
                visual.SetOutLineAlpha(1f);
                break;
            case ElementRelation.Disadvantage:
                visual.SetTileColor(DisadvantageTileColor);
                visual.SetOutLineColor(DefualtOutLineColor);
                visual.SetOutLineAlpha(1f);

                visual.AnimDepressIs(true);
                break;
            case ElementRelation.Irrelevant:
                visual.SetOutLineColor(DefualtOutLineColor);
                visual.SetOutLineAlpha(0.5f);
                break;
            default:
                break;
        }
    }

    private void RemoveUnit(GridPosition position, TileUnit unit)
    {
        if (!GridManager.Instance.CheckOnGrid(position)) return;

        List<TileUnit> units = GridManager.Instance.GetUnitListAtGridPosition(position);
        if (units.Count > 1) return;


        GridTileVisual visual = tileVisuals[position.x, position.y].GetComponent<GridTileVisual>();
        Element element = GridManager.Instance.GetTileData(position).GetElement();


        visual.SetTileColor(ElementColor(element));
        visual.SetOutLineColor(ElementColor(element));
        visual.SetOutLineAlpha(0.5f);
        visual.AnimDepressIs(false);

        if (element.GetElementType() == ElementType.None) 
        {
            visual.ResetTileColor();

            visual.SetOutLineAlpha(0f);
        }
    }

    private Color ElementColor(Element element)
    {
        Color result = new Color();

        switch (element.GetElementType())
        {
            case ElementType.None:
                result = Color.white;
                break;
            case ElementType.Fire:
                result = Color.red;
                break;
            case ElementType.Water:
                result = Color.cyan;
                break;
            case ElementType.Grass:
                result = Color.green;
                break;
        }

        return result;
    }


    private Sprite CropSprite(Element element, GrowPoint growPoint)
    {
        Sprite result = null;

        if(growPoint == GrowPoint.Seed)
        {
            result = SpriteSet.Seed;
        }
        else if(growPoint == GrowPoint.Growth)
        {
            switch (element.GetElementType())
            {
                case ElementType.None:
                    result = SpriteSet.Seed;
                    break;
                case ElementType.Fire:
                    result = SpriteSet.FireGrowth;
                    break;
                case ElementType.Water:
                    result = SpriteSet.WaterGrowth;
                    break;
                case ElementType.Grass:
                    result = SpriteSet.GrassGrowth;
                    break;
            }
        }
        else if(growPoint == GrowPoint.Harvest)
        {
            switch (element.GetElementType())
            {
                case ElementType.Fire:
                    result = SpriteSet.FireHarvest;
                    break;
                case ElementType.Water:
                    result = SpriteSet.WaterHarvest;
                    break;
                case ElementType.Grass:
                    result = SpriteSet.GrassHarvest;
                    break;
            }
        }
       

        return result;
    }
}
