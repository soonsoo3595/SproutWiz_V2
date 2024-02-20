using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public int totalScore;    // ���� ���ھ�
    public int bestScore;      // �ְ� ��� ���ھ�

    public int gold;           // ���� ���

    public int harvestCount;   // ��Ȯ�� �� ����
    public int multiHarvestCount;  // ��Ƽ ��Ȯ�� �� ����
    public int achieveGoalCount;   // ��ǥ �޼��� �� ����
    public int feverCount;         // �ǹ� ��� ������ �� Ƚ��
    public int reRollCount;        // ���� Ƚ��

    // ���׷��̵� ����
    public List<int> skillLevels = new List<int>();

    // ȯ�� ����
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
