using TMPro;
using UnityEngine;

public class GridDebugObject : MonoBehaviour
{
    [SerializeField] private TextMeshPro textMeshPro;

    private GridTile gridTile;
    private TileData tileData;

    public void SetGridObject(GridTile gridTile, TileData tileData)
    {
        this.gridTile = gridTile;
        this.tileData = tileData;
    }

    private void Update()
    {
        textMeshPro.text = gridTile.ToString() + '\n' + tileData.ToString();
    }
}
