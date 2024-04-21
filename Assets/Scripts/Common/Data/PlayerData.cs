using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public string userID;     // ���� ���� ID
    public string userName;   // ���� �г���

    public int totalScore;    // ���� ���ھ�
    public int bestScore;      // �ְ� ��� ���ھ�

    public int gold;           // ���� ���

    // ���׷��̵� ����
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
