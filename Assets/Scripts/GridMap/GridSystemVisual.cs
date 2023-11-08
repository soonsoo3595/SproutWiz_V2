using System.Collections.Generic;
using System.Text;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UIElements;

public class GridSystemVisual : MonoBehaviour
{
    [SerializeField] private Transform gridTilePrefab;
    [SerializeField] private SpriteSetting SpriteSet;

    private Transform[,] tileVisuals;

    private void Start()
    {
        tileVisuals = new Transform[GridManager.Instance.GetWidth(), GridManager.Instance.GetHeight()];

        for (int x = 0; x < GridManager.Instance.GetWidth(); x++)
        {
            for (int y = 0; y < GridManager.Instance.GetHeight(); y++)
            {
                GridPosition gridPosition = new GridPosition(x, y);
                tileVisuals[x,y] = Instantiate(gridTilePrefab, GridManager.Instance.GetWorldPosition(gridPosition), Quaternion.identity, this.transform);
            }
        }

        LevelData.changeTileData += UpdateVisual;
        GridManager.addUnitOnGridTile += AddUnit;
        GridManager.removeUnitOnGridTile += RemoveUnit;
    }

    // 타일값 갱신 이후 호출되야 함.
    private void UpdateVisual(GridPosition position)
    {
        ChangeSprite(position);
    }

    // 타일이 아니라 작물 이미지 변경으로 교체 필요.
    private void ChangeSprite(GridPosition position)
    {
        GridTileVisual visual = tileVisuals[position.x, position.y].GetComponent<GridTileVisual>();

        TileData targetTile = GridManager.Instance.GetTileData(position);

        Element targetElement = targetTile.GetElement();
        GrowPoint growPoint = targetTile.growPoint;

        visual.SetCropSptire(CropSprite(targetElement, growPoint));
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
            visual.SetOutLineColor(Color.black);

            visual.ResetTileColor();
        }


        switch (relation)
        {
            case ElementRelation.Equal:
                visual.SetTileColor(ElementColor(element));
                //visual.SetOutLineColor(ElementColor(element));
                break;
            case ElementRelation.Disadvantage:
                visual.SetTileColor(Color.black);
                visual.SetOutLineColor(Color.black);
                break;
            case ElementRelation.Irrelevant:
                visual.SetOutLineColor(Color.black);

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


    // TODO: 수확물 딜레이 처리 필요.
    private Sprite CropSprite(Element element, GrowPoint growPoint)
    {
        Sprite result = null;

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

        return result;
    }
}
