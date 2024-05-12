using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using UnityEngine;

public static class DataManager
{
    public static PlayerData playerData = new PlayerData();
    public static SkillLibrary skillLibrary = ScriptableObject.CreateInstance<SkillLibrary>();

    [Header("LeaderBoard")]
    public const string TopLeaderboardId = "TopScore";
    public const string TotalLeaderboardId = "TotalScores";

    public static void LoadGameData()
    {
        skillLibrary = Resources.Load<SkillLibrary>("SkillLibrary");
    }

    public async static Task SaveData()
    {
        string writeLock = await SaveObjectData(AuthenticationService.Instance.PlayerId, playerData);
    }

    public async static Task LoadData()
    {
        playerData = await RetrieveSpecificData<PlayerData>(AuthenticationService.Instance.PlayerId);
    }   

    private async static Task<string> SaveObjectData(string key, PlayerData value)
    {
        try
        {
            Dictionary<string, object> oneElement = new Dictionary<string, object>
            {
               { key, value }
            };

            // Saving data without write lock validation by passing the data as an object instead of a SaveItem
            Dictionary<string, string> result = await CloudSaveService.Instance.Data.Player.SaveAsync(oneElement);
            string writeLock = result[key];

            return writeLock;
        }
        catch (CloudSaveValidationException e)
        {
            Debug.LogError(e);
        }
        catch (CloudSaveRateLimitedException e)
        {
            Debug.LogError(e);
        }
        catch (CloudSaveException e)
        {
            Debug.LogError(e);
        }

        return null;
    }

    private async static Task<T> RetrieveSpecificData<T>(string key)
    {
        try
        {
            var results = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> { key });

            if (results.TryGetValue(key, out var item))
            {
                Debug.Log(playerData.ToString());
                return item.Value.GetAs<T>();
            }
            else
            {
                Debug.Log($"There is no such key as {key}!");
            }
        }
        catch (CloudSaveValidationException e)
        {
            Debug.LogError(e);
        }
        catch (CloudSaveRateLimitedException e)
        {
            Debug.LogError(e);
        }
        catch (CloudSaveException e)
        {
            Debug.LogError(e);
        }

        return default;
    }
}
