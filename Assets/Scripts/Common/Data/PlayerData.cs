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
