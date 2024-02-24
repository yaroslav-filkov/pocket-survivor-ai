using System;
using System.Collections.Generic;
using UnityEngine;

public class GeneralGameConfig : IRemoteConfigService
{
    private static Dictionary<string, Dictionary<string, string>> dictionaryGeneral = new();

    public static event Action LoadedService;
    public static bool IsLoadedData { get; private set; }
 
    public const string KEY_GENERAL_REMOTE = "general";
    private const string KEY_GENERAL_VALUE = "value";
    public static void Load()
    {
        dictionaryGeneral = SheetAdapter.Read(KEY_GENERAL_REMOTE);
        LoadedService?.Invoke();
        IsLoadedData = true;
    }
    public static T GetConfigRemoteDto<T>(string id_field)
    {
        var value = dictionaryGeneral[KEY_GENERAL_VALUE].ContainsKey(id_field)
            ? dictionaryGeneral[KEY_GENERAL_VALUE][id_field]
            : string.Empty;

        if (string.IsNullOrEmpty(value))
        {
            Debug.LogError($"Param not found: {id_field} (value).");
            return default(T);
        }
 

        return (T)Convert.ChangeType(value, typeof(T));
    }
    public static T GetConfigRemoteDto<T>(Dictionary<string, Dictionary<string, string>> dictionaryDto, string id, string param)
    {
        if (!dictionaryDto.ContainsKey(param))
        {
            Debug.LogError("Weapon not found: " + param);
            return default(T);
        }

        var value = dictionaryDto[param].ContainsKey(id)
            ? dictionaryDto[param][id]
            : string.Empty;

        if (string.IsNullOrEmpty(value)) throw new Exception($"Param not found: {id} ({param}).");

        return (T)Convert.ChangeType(value, typeof(T));
    }
}
