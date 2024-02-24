using System;
using UnityEngine;


public static class SaveService
{
    private const string PLAYER_DATA = "PLAYER_DATA";
    public static string Password;
    public static event Action LoadedData;
    public static event Action SavedData;
    public static void Init()
    {
        LoadFrom(PlayerPrefs.GetString(PLAYER_DATA));
    }

    public static void SaveProgress()
    {
        SaveUpdate();

    }

    private static void SaveUpdate()
    {
        var data = new SaveData();
        var jsonData = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(PLAYER_DATA, jsonData);
        PlayerPrefs.Save();
        SavedData?.Invoke();
    }

    private static void LoadFrom(string jsonData)
    {
        Load(jsonData);
    }

    private static void Load(string data)
    {
        if (string.IsNullOrEmpty(data))
        {
            return;
        }
        var getData = JsonUtility.FromJson<SaveData>(data); ;

        LoadedData?.Invoke();
    }
}

