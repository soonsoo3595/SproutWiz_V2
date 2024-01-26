using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRecord : MonoBehaviour
{
    [HideInInspector] public List<int> records;

    void Start()
    {
        Assign();
    }

    public List<int> GetRecord()
    {
        return records;
    }

    public void AddRecord(RecordType type, int count = 1)
    {
        records[(int)type] += count;
    }

    public int GetRecord(RecordType type)
    {
        return records[(int)type];
    }

    private void Init()
    {
        for(int i = 0; i < records.Count; i++)
        {
            records[i] = 0;
        }
    }

    private void Assign()
    {
        int size = Enum.GetNames(typeof(RecordType)).Length;

        for(int i = 0; i < size; i++)
        {
            records.Add(0);
        }

        EventManager.resetMainGame += Init;
    }
}
