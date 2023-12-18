using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileFactory : MonoBehaviour
{
    public static TileFactory Instance { get; private set; }

    private bool isHarvested = false;

    readonly private float harvestInitDelayTime = 0.35f;

    private void Awake()
    {
        Instance = this;
    }

    public bool MakeOrder(TileData tile, TetrisUnit unit)
    {
        isHarvested = false;

        Order order = new Order(tile, unit);
        ClassifyOrder(order);

        return isHarvested;
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
        // order.GetTile().growPoint = GrowPoint.Harvest;
    }

    private void Equal(Order order)
    {
        //GridManager.Instance.GetSetting().equal.growPoint;
        order.GetTile().growPoint = GrowPoint.Harvest;
    }

    private void DisAdvantage(Order order)
    {
        order.GetTile().InitTile();

        GridPosition gridPosition = order.GetUnit().GetGridPosition();

        GridManager.Instance.SetDeployableGrid(gridPosition, false);
        EventManager.changeTileData(gridPosition);
    }

    private void FinishOrder(Order order)
    {
        if (order.GetTile().growPoint == GrowPoint.Harvest)
        {
            isHarvested = true;

            EventManager.tileHarvest(order.GetTile());

            StartCoroutine(InitTileDelayed(order, harvestInitDelayTime));
        }
    }

    private IEnumerator InitTileDelayed(Order order, float delay)
    {
        yield return new WaitForSeconds(delay);

        order.GetTile().InitTile();
        EventManager.changeTileData(order.GetUnit().GetGridPosition());
    }
}
