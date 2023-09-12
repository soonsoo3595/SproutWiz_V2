using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static EventManager;

public class TetrisEvent : MonoBehaviour
{
    void Start()
    {
        EventManager.Instance.applyTetris += SetTileData;
    }


    private static void SetTileData(TetrisObject tetrisObject)
    {
        List<TetrisUnit> units = tetrisObject.GetUnitList();

        foreach (TetrisUnit unit in units)
        {
            GridManager.Instance.SetElement(unit.GetElement(), unit.GetGridPosition());
        }
    }


}
