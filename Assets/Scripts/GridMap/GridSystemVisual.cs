using UnityEngine;

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
    }

    // Ÿ�ϰ� ���� ���� ȣ��Ǿ� ��.
    private void UpdataVisual(GridPosition position)
    {
        ChangeSprite(position);
    }

    // Ÿ���� �ƴ϶� �۹� �̹��� �������� ��ü �ʿ�.
    private void ChangeSprite(GridPosition position)
    {
        GridTileVisual visual = tileVisuals[position.x, position.y].GetComponent<GridTileVisual>();

        Element targetElement = GridManager.Instance.GetTileData(position).element;

        visual.SetTileColor(ElementColor(targetElement));
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

        result.a = 0.7f;

        return result;
    }
}
