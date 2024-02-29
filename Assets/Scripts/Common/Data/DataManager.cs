using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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

    public static PlayerData playerData;
    public static SkillLibrary skillLibrary = ScriptableObject.CreateInstance<SkillLibrary>();

    public static void LoadGameData()
    {
        gameData = Resources.Load<GameData>("GameData");
        skillLibrary = Resources.Load<SkillLibrary>("SkillLibrary");

        Debug.Log("GameData Load Complete");
    }

    public static void LoadPlayerData()
    {
        Debug.Log("������ �ε�");
        var jsonData = File.ReadAllText(Application.persistentDataPath + "/PlayerData.json");
        playerData = JsonConvert.DeserializeObject<PlayerData>(jsonData);

        Debug.Log(jsonData);
    }

    public static void SavePlayerData()
    {
        Debug.Log("������ ����");
        var jsonString = JsonConvert.SerializeObject(playerData);
        File.WriteAllText(Application.persistentDataPath + "/PlayerData.json", jsonString);
    }

}
