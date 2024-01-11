using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData", order = 1)]
public class GameData : ScriptableObject
{
    [Header("Main Game")]
    public float timeLimit = 80f;

    [Header("Harvest")]
    public int harvestScore = 100;
    public List<int> multiHarvestScore = new List<int> { 0, 0, 150, 400, 700 };

    [Header("Goal")]
    public int goalScore = 50;
    public int goalGold = 100;

    [Header("Mana Collector")]
    public int manaCollectorMaxMana = 100;
    public int manaCollectorHarvestMana = 2;
    public List<int> manaCollectorMultiHarvestMana = new List<int> { 0, 0, 2, 4, 6 };

    [Header("Sunshine Magic")]
    public float sunshineMagicTime = 10f;
    public float sunshineMagicEffect = 2f;

    [Header("Casting Cancel")]
    public float castingCancelChargeTime = 15f;

    [Header("Harvest Upgrade")]
    public List<int> upgrade_HarvestScore = new List<int> { 0, 25, 50, 80, 110, 150 };
    public List<int> cost_HarvestScore = new List<int> { 0, 700, 1000, 1300, 1600, 2000 };
    public List<int> upgrade_DoubleHarvestScore = new List<int> { 0, 10, 25, 40, 55, 70 };
    public List<int> upgrade_TripleHarvestScore = new List<int> { 0, 40, 75, 110, 150, 200 };
    public List<int> upgrade_QuadraHarvestScore = new List<int> { 0, 200, 240, 280, 320, 360 };
    public List<int> cost_MultiHarvestScore = new List<int> { 0, 800, 1000, 1300, 1700, 2000 };

    [Header("Goal Upgrade")]
    public List<float> bonusGoalScore = new List<float> { 0, 0.05f, 0.07f, 0.1f, 0.14f, 0.2f };
    public List<int> cost_bonusGoalScore = new List<int> { 0, 1000, 1300, 1600, 2000, 2500 };

    [Header("CastingCancel Upgrade")]
    public List<float> reduce_CastingCancel = new List<float> { 0, 1f, 2f, 3f, 4f, 5f };
    public List<int> cost_reduce_CastingCancel = new List<int> { 0, 600, 800, 1000, 1200, 1500 };

    [Header("Mana Collector Upgrade")]
    public List<int> reduce_ManaCollectorMaxMana = new List<int> { 0, 2, 3, 4, 5, 6 };
    public List<int> cost_reduce_ManaCollectorMaxMana = new List<int> { 0, 600, 700, 800, 900, 1000 };

    [Header("SunshineMagic Upgrade")]
    public List<float> upgrade_sunshineMagicTime = new List<float> { 0, 1f, 2f, 3f, 4f, 5f };
    public List<int> cost_sunshineMagicTime = new List<int> { 0, 700, 1000, 1400, 1800, 2300 };
    public List<float> upgrade_sunshineMagicEffect = new List<float> { 0, 0.2f, 0.4f, 0.6f, 0.8f, 1f };
    public List<int> cost_sunshineMagicEffect = new List<int> { 0, 800, 1200, 1600, 2000, 2400 };

}
