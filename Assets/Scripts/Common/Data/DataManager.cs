using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class DataManager
{
    public static PlayerData playerData;
    public static SkillLibrary skillLibrary = ScriptableObject.CreateInstance<SkillLibrary>();

    public static void LoadGameData()
    {
        skillLibrary = Resources.Load<SkillLibrary>("SkillLibrary");

        Debug.Log("GameData Load Complete");
    }

    public static void LoadPlayerData()
    {
        Debug.Log("데이터 로드");
        var jsonData = File.ReadAllText(Application.persistentDataPath + "/PlayerData.json");
        playerData = JsonConvert.DeserializeObject<PlayerData>(jsonData);

        Debug.Log(jsonData);
    }

    public static void SavePlayerData()
    {
        Debug.Log("데이터 저장");
        var jsonString = JsonConvert.SerializeObject(playerData);
        File.WriteAllText(Application.persistentDataPath + "/PlayerData.json", jsonString);
    }

}
