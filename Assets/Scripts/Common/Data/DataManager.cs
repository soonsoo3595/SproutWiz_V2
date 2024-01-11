using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataManager
{
    private static GameData gameData;

    public static GameData GameData
    {
        get
        {
            return gameData;
        }
    }

    public static PlayerData playerData = new PlayerData();

    public static void LoadData()
    {
        gameData = Resources.Load<GameData>("GameData");
    }
}
