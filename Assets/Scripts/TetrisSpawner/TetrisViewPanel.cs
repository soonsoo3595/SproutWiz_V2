using System.Collections.Generic;
using UnityEngine;

public class TetrisViewPanel : MonoBehaviour
{
    [SerializeField]
    private List<RectTransform> ViewSlots;

    private List<Transform> tetrisList;

    private PreViewSystem preViewSystem;


    private void Awake()
    {
        preViewSystem = new PreViewSystem(ViewSlots.Count);
        tetrisList = new List<Transform>();
    }

    private void Start()
    {
        foreach (RectTransform slot in ViewSlots)
        {
            Transform tetris = Instantiate(preViewSystem.GetNewTetris(), slot);
            tetris.localScale = new Vector3(50f, 50f, 50f);
            tetris.localPosition = Vector3.zero;

            tetrisList.Add(tetris);
        }

        EventManager.Instance.applyTetris += ApplyTetris;
    }

    private void ApplyTetris(TetrisObject tetrisObject)
    {
        SetTileData(tetrisObject);

        tetrisList.Remove(tetrisObject.transform);
        tetrisList.Add(Instantiate(preViewSystem.GetNewTetris()));

        int count = 0;

        foreach (Transform tetris in tetrisList)
        {
            tetris.SetParent(ViewSlots[count]);
            tetris.localScale = new Vector3(50f, 50f, 50f);
            tetris.localPosition = Vector3.zero;

            count++;
        }
    }

    private static void SetTileData(TetrisObject tetrisObject)
    {
        List<TileUnit> units = tetrisObject.GetUnitList();

        foreach (TileUnit unit in units)
        {
            GridManager.Instance.SetElement(Element.Fire, unit.GetGridPosition());
        }
    }
}
