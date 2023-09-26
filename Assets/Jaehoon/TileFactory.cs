using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order
{
    public TileData tile;
    public TetrisUnit unit;

    public Order(TileData tile, TetrisUnit unit)
    {
        this.tile = tile;
        this.unit = unit;
    }
}
public class TileFactory : MonoBehaviour
{
    public static TileFactory Instance { get; private set; }
    public Queue<Order> orderQueue = new Queue<Order>();

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

    private void Start()
    {
    }

    public void EnqueueOrder(TileData tile, TetrisUnit unit)
    {
        Order order = new Order(tile, unit);
        orderQueue.Enqueue(order);
    }
    
    public void SetData(Order order)
    {
        Element element = order.tile.element;
        TetrisUnit unit = order.unit;
        int growPoint = order.tile.growPoint;
        int maxGrowPoint = order.tile.maxGrowPoint;
        
        if (element.IsNone())
        {
            element.SetElementType(unit.GetElement().GetElementType());
            growPoint++;
        }
        else
        {
            switch (element.GetElementRelation(unit.GetElement()))
            {
                case ElementRelation.Advantage:
                    growPoint = Mathf.Clamp(growPoint + unit.GetGrowPoint() * 2, 0, maxGrowPoint);
                    break;
                case ElementRelation.Equal:
                    growPoint = Mathf.Clamp(growPoint + unit.GetGrowPoint(), 0, maxGrowPoint);
                    break;
                case ElementRelation.Disadvantage:
                    growPoint = 0;
                    break;
            }
        }

        order.tile.growPoint = growPoint;
        UpdateTile(order);
    }
    
    public void UpdateTile(Order order)
    {
        int growPoint = order.tile.growPoint;
        if(growPoint == 0)
        {
            InitTile(order);
        }
        else if(growPoint == order.tile.maxGrowPoint)
        {
            InitTile(order);
        }
        
    }
    
    public void InitTile(Order order)
    {
        order.tile.growPoint = 0;
        order.tile.element.Init();
    }
}
