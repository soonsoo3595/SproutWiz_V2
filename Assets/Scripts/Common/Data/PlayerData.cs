using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public string userName;   // ���� �̸�
    public int totalScore;    // ���� ���ھ�
    public int bestScore;      // �ְ� ��� ���ھ�

    public int gold;           // ���� ���

    // ���׷��̵� ����
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
