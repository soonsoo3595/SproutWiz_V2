using System.Collections;
using System.Collections.Generic;
using UGS;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static Dictionary<string, DefaultTable.Data> dataMap = new Dictionary<string, DefaultTable.Data>();

    private void Awake()
    {
        UnityGoogleSheet.LoadAllData();
    }

    void Start()
    {
        foreach (var value in DefaultTable.Data.DataList)
        {
            dataMap.Add(value.strValue, value);
        }
        // var dataFromMap = DefaultTable.Data.DataMap[0];
        // Debug.Log("dataFromMap : " + dataFromMap.index + ", " + dataFromMap.level1 + "," + dataFromMap.level2);
    }

}
