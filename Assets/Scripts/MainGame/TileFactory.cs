using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileFactory : MonoBehaviour
{
    public static TileFactory Instance { get; private set; }

    public delegate void Harvest(TileData tile);

    public static Harvest harvest;
    

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
        ClassifyOrder(order);
    }

    private void ClassifyOrder(Order order)
    {
        if (order.GetTile().growPoint == GrowPoint.Seed)
        {
            Grant(order);
        }
        else if (order.GetTile().growPoint == GrowPoint.Growth)
        {
            Process(order);
        }
    }

    private void Grant(Order order)
    {
        Element element = order.GetTile().GetElement();
        TetrisUnit unit = order.GetUnit();

        element.SetElementType(unit.GetElement().GetElementType());
        order.GetTile().growPoint = GrowPoint.Growth;
    }

    private void Process(Order order)
    {
        Element element = order.GetTile().GetElement();
        TetrisUnit unit = order.GetUnit();

        switch (element.GetElementRelation(unit.GetElement()))
        {
            case ElementRelation.Irrelevant:
                Irrelevant(order);
                break;
            case ElementRelation.Equal:
                Equal(order);
                break;
            case ElementRelation.Disadvantage:
                DisAdvantage(order);
                break;
        }
        
        FinishOrder(order);
    }
    
    // 확률 추가할 경우 이 밑 함수들 만지면 될 듯
    private void Irrelevant(Order order)
    {
        order.GetTile().growPoint = GrowPoint.Harvest;
    }

    private void Equal(Order order)
    {
        //GridManager.Instance.GetSetting().equal.growPoint;
        order.GetTile().growPoint = GrowPoint.Harvest;
    }

    private void DisAdvantage(Order order)
    {
        order.GetTile().InitTile();
    }

    private void FinishOrder(Order order)
    {
        if (order.GetTile().growPoint == GrowPoint.Harvest)
        {
            harvest(order.GetTile());
            order.GetTile().InitTile();
        }
        
    }

}
