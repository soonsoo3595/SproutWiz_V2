using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileFactory : MonoBehaviour
{
    public static TileFactory Instance { get; private set; }

    public delegate void Harvest(TileData tile);

    public static Harvest harvest;

    private bool isHarvested = false;

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
            isHarvested = true;

            harvest(order.GetTile());

            //order.GetTile().InitTile();
            StartCoroutine(InitTileDelayed(order, 0.35f));
        }
    }

    private IEnumerator InitTileDelayed(Order order, float delay)
    {
        yield return new WaitForSeconds(delay);

        order.GetTile().InitTile();
        LevelData.changeTileData(order.GetUnit().GetGridPosition());
    }
}
