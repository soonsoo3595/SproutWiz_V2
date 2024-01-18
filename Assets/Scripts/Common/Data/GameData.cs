using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData", order = 1)]
public class GameData : ScriptableObject
{
    [Header("Main Game")]
    public float TimeLimit = 80f;

    [Header("Harvest")]
    public int HarvestScore = 100;
    public List<int> MultiHarvestScore = new List<int> { 0, 0, 150, 400, 700 };

    [Header("Goal")]
    public int GoalScore = 50;
    public int GoalGold = 100;

    [Header("Mana Collector")]
    public int ManaCollectorMaxMana = 100;
    public int ManaCollectorHarvestMana = 2;
    public List<int> ManaCollectorMultiHarvestMana = new List<int> { 0, 0, 2, 4, 6 };

    [Header("Sunshine Magic")]
    public float SunshineMagicTime = 10f;
    public float SunshineMagicEffect = 2f;

    [Header("Casting Cancel")]
    public float CastingCancelChargeTime = 15f;
}
