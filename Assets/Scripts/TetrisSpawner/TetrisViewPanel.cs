using System.Collections.Generic;
using UnityEngine;

public class TetrisViewPanel : MonoBehaviour
{
    [SerializeField]
    private List<RectTransform> ViewSlots;

    [SerializeField]
    private float TetrisSize = 50;

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
            Transform tetris = Instantiate(preViewSystem.GetRandomTetris(), slot);
            tetris.GetComponent<TetrisObject>().SetAllUnitState(1, preViewSystem.GetRandomElement());

            SetDefaultSize(tetris);

            tetrisList.Add(tetris);
        }

        EventManager.Instance.applyTetris += ApplyTetris;
    }

    private void ApplyTetris(TetrisObject tetrisObject)
    {
        SetTileData(tetrisObject);

        tetrisList.Remove(tetrisObject.transform);

        Transform newTetris = Instantiate(preViewSystem.GetRandomTetris());
        newTetris.GetComponent<TetrisObject>().SetAllUnitState(1, preViewSystem.GetRandomElement());

        tetrisList.Add(newTetris);

        int count = 0;

        foreach (Transform tetris in tetrisList)
        {
            tetris.SetParent(ViewSlots[count]);
            SetDefaultSize(tetris);

            count++;
        }
    }

    private static void SetTileData(TetrisObject tetrisObject)
    {
        List<TetrisUnit> units = tetrisObject.GetUnitList();

        foreach (TetrisUnit unit in units)
        {
            GridManager.Instance.SetElement(unit.GetElement(), unit.GetGridPosition());
        }
    }


    private void SetDefaultSize(Transform tetris)
    {
        tetris.localScale = new Vector3(TetrisSize, TetrisSize);
        tetris.localPosition = Vector3.zero;
    }
}
