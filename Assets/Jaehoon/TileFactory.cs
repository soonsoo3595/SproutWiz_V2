using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileFactory : MonoBehaviour
{
    public static TileFactory Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

    }

    public void MakeOrder(TileData tile, TetrisUnit unit)
    {
        Order order = new Order(tile, unit);
        ProcessOrder(order);
    }

    private void ProcessOrder(Order order)
    {
        if (order.GetTile().GetElement().GetElementType() == ElementType.None)
        {
            Assign(order);
        }
        else
        {
            UpdateTile(order);
        }
    }

    public void Assign(Order order)
    {
        Element element = order.GetTile().GetElement();
        TetrisUnit unit = order.GetUnit();
        
        element.SetElementType(unit.GetElement().GetElementType());
        order.GetTile().SetGrowPoint(1);
    }

    public void UpdateTile(Order order)
    {
        Element element = order.GetTile().GetElement();
        TetrisUnit unit = order.GetUnit();

        switch (element.GetElementRelation(unit.GetElement()))
        {
            case ElementRelation.Advantage:
                order.GetTile().SetGrowPoint(unit.GetGrowPoint() * 2);
                break;
            case ElementRelation.Equal:
                order.GetTile().SetGrowPoint(unit.GetGrowPoint());
                break;
            case ElementRelation.Disadvantage:
                order.GetTile().InitTile();
                break;
        }
    }
    
}
