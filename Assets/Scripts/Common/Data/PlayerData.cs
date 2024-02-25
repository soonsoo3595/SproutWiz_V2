using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public string userName;   // 유저 이름
    public int totalScore;    // 누적 스코어
    public int bestScore;      // 최고 기록 스코어

    public int gold;           // 보유 골드

    // 업그레이드 레벨
    public List<int> skillLevels = new List<int>();

    public bool isCorrectionMode = false;

    public PlayerData(string nickname)
    {
        userName = nickname;

        totalScore = 0;
        bestScore = 0;

        gold = 0;

        for(int i = 0; i < Enum.GetValues(typeof(SkillType)).Length; i++)
        {
            skillLevels.Add(0);
        }
    }
}
