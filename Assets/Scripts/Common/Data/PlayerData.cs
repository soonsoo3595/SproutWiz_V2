using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{

    public int totalScore;    // 누적 스코어
    public int bestScore;      // 최고 기록 스코어

    public int gold;           // 보유 골드

    public int harvestCount;   // 수확한 총 갯수
    public int multiHarvestCount;  // 멀티 수확한 총 갯수
    public int achieveGoalCount;   // 목표 달성한 총 갯수
    public int feverCount;         // 피버 모드 진입한 총 횟수
    public int reRollCount;        // 리롤 횟수

    // 업그레이드 레벨
    public int level_HarvestScore;
    public int level_MultiHarvestScore;
    public int level_BonusGoalScore;
    public int level_ReduceCastingCancel;
    public int level_ReduceManaCollectorMaxMana;
    public int level_SunshineMagicTime;
    public int level_SunshineMagicEffect;

    public PlayerData()
    {
        totalScore = 0;
        bestScore = 0;

        gold = 0;

        harvestCount = 0;
        multiHarvestCount = 0;
        achieveGoalCount = 0;
        feverCount = 0;
        reRollCount = 0;

        level_HarvestScore = 0;
        level_MultiHarvestScore = 0;
        level_BonusGoalScore = 0;
        level_ReduceCastingCancel = 0;
        level_ReduceManaCollectorMaxMana = 0;
        level_SunshineMagicTime = 0;
        level_SunshineMagicEffect = 0;
    }
}
