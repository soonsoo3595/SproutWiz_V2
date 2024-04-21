using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public string userID;     // 유저 고유 ID
    public string userName;   // 유저 닉네임

    public int totalScore;    // 누적 스코어
    public int bestScore;      // 최고 기록 스코어

    public int gold;           // 보유 골드

    // 업그레이드 레벨
    public List<int> skillLevels = new List<int>();

    public bool isFirstPlay = true;
    public bool isCorrectionMode = false;

    public PlayerData()
    {
        userID = "";
        userName = "";

        totalScore = 0;
        bestScore = 0;

        gold = 0;

        PlayerPrefs.SetFloat("SFXVolume", 1f);
        PlayerPrefs.SetFloat("BGMVolume", 1f);

        for(int i = 0; i < Enum.GetValues(typeof(SkillType)).Length; i++)
        {
            skillLevels.Add(0);
        }
    }

    public void SetInitInfo(string playerID, string playerName)
    {
        userID = playerID;
        userName = playerName;
    } 
}
