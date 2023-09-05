using TMPro;
using UnityEngine;

public class GridDebugObject : MonoBehaviour
{
    [SerializeField] private TextMeshPro textMeshPro;

    private GridTile gridTile;

    public void SetGridObject(GridTile gridTile)
    {
        this.gridTile = gridTile;
    }

    private void Update()
    {
        textMeshPro.text = gridTile.ToString();
    }
}
