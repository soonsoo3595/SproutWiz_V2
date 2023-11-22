using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    public delegate void Harvest(TileData tile);
    public static Harvest harvest;

    public delegate void HarvestCount(int count);
    public static HarvestCount harvestCount;

    public delegate void AfterApplyTetris();
    public static AfterApplyTetris afterApplyTetris;

    public delegate void ResetMainGame();
    public static ResetMainGame resetMainGame;
}
