using UnityEngine;

public class GridSystemVisual : MonoBehaviour
{
    [SerializeField] private Transform gridTilePrefab;

    private void Start()
    {
        for (int x = 0; x < GridManager.Instance.GetWidth(); x++)
        {
            for (int y = 0; y < GridManager.Instance.GetHeight(); y++)
            {
                GridPosition gridPosition = new GridPosition(x, y);
                Instantiate(gridTilePrefab, GridManager.Instance.GetWorldPosition(gridPosition), Quaternion.identity, this.transform);
            }
        }
    }
}
