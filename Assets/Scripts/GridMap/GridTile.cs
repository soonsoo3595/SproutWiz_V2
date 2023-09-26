using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile
{
    private GridPosition gridPosition;
    private List<TileUnit> units;


    public GridTile(GridPosition gridPosition)
    {
        this.gridPosition = gridPosition;
        units = new List<TileUnit>();
    }

    public override string ToString()
    {
        if (units.Count == 0)
        {
            return gridPosition.ToString();
        }
        else
        {
            string unitString = "";

            foreach (TileUnit unit in units)
            {
                unitString += unit.name + "\n";
            }

            return gridPosition.ToString() + "\n" + unitString;
        }
    }

    public void AddUnit(TileUnit unit)
    {
        units.Add(unit);
    }

    public void RemoveUnit(TileUnit unit)
    {
        units.Remove(unit);
    }

    public List<TileUnit> GetUnitList()
    {
        return units;
    }
}
