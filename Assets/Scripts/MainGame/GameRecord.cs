using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRecord
{
    public int harvestCount;
    public int multiHarvestCount;
    public int achieveGoalCount;
    public int feverCount;
    public int rerollCount;

    public GameRecord()
    {
        harvestCount = 0;
        multiHarvestCount = 0;
        achieveGoalCount = 0;
        feverCount = 0;
        rerollCount = 0;
    }

    public List<int> GetRecord()
    {
        List<int> record = new List<int>();

        record.Add(harvestCount);
        record.Add(multiHarvestCount);
        record.Add(achieveGoalCount);
        record.Add(feverCount);
        record.Add(rerollCount);

        return record;
    }

    public void InitRecord()
    {
        harvestCount = 0;
        multiHarvestCount = 0;
        achieveGoalCount = 0;
        feverCount = 0;
        rerollCount = 0;
    }
}
