using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GridSystemVisual : MonoBehaviour
{
    [SerializeField] private Transform gridTilePrefab;

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

        LevelData.changeTileData += UpdataVisual;
        GridManager.addUnitOnGridTile += AddUnit;
        GridManager.removeUnitOnGridTile += RemoveUnit;
    }

    // 타일값 갱신 이후 호출되야 함.
    private void UpdataVisual(GridPosition position)
    {
        ChangeSprite(position);
    }

    // 타일이 아니라 작물 이미지 변경으로 교체 필요.
    private void ChangeSprite(GridPosition position)
    {
        GridTileVisual visual = tileVisuals[position.x, position.y].GetComponent<GridTileVisual>();

        Element targetElement = GridManager.Instance.GetTileData(position).element;

        visual.SetTileColor(ElementColor(targetElement));
    }

    private void AddUnit(GridPosition position, TileUnit unit)
    {
        if (!GridManager.Instance.CheckOnGrid(position)) return;

        GridTileVisual visual = tileVisuals[position.x, position.y].GetComponent<GridTileVisual>();

        visual.SetAlpha(ElementRelationColor(position, unit));
    }

    private float ElementRelationColor(GridPosition position, TileUnit unit)
    {
        float result = 0f;

        if(GridManager.Instance.GetTileData(position).element.GetElementType() == ElementType.None)
        {
            GridTileVisual visual = tileVisuals[position.x, position.y].GetComponent<GridTileVisual>();

            visual.SetTileColor(Color.black);
            return 0.5f;
        }


        TetrisUnit tetrisUnit = unit as TetrisUnit;
        ElementRelation relation = GridManager.Instance.GetTileData(position).element.GetElementRelation(tetrisUnit.GetElement());

        switch (relation)
        {
            case ElementRelation.Equal:
                result = 0.5f;
                break;
            case ElementRelation.Disadvantage:
                result = 0.05f;
                break;
            case ElementRelation.Advantage:
                result = 1f;
                break;
        }

        return result;
    }

    private void RemoveUnit(GridPosition position, TileUnit unit)
    {
        if (!GridManager.Instance.CheckOnGrid(position)) return;

        GridTileVisual visual = tileVisuals[position.x, position.y].GetComponent<GridTileVisual>();

        List<TileUnit> units = GridManager.Instance.GetUnitListAtGridPosition(position);
        if (units.Count > 1) return;

        ChangeSprite(position);
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
                result = Color.blue;
                break;
            case ElementType.Grass:
                result = Color.green;
                break;
        }

        result.a = 0.5f;

        return result;
    }
}
