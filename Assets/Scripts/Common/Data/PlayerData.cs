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
    public List<int> skillLevels = new List<int>();

    // 환경 설정
    public float totalVolume = 1.0f;
    public float bgmVolume = 1.0f;
    public float sfxVolume = 1.0f;

    public bool isCorrectionMode = false;

    public bool isFirstPlay = true;
    
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

        for(int i = 0; i < Enum.GetValues(typeof(SkillType)).Length; i++)
        {
            skillLevels.Add(0);
        }
    }
}
