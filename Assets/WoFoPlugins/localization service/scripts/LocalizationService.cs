using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

[Serializable]
public class LanguageGame
{
    public SystemLanguage SystemLanguage;
    public TMP_FontAsset FontAsset;
    public Font Font;
    public string MainId;
}


public class LocalizationService : MonoBehaviour
{

    public static event Action LoadedService;
    private static LocalizationService Instance;

    public static string ID_LOCALIZATION = "localization";
    private static string defaultLanguage = "en";

    [SerializeField] private string KEY_TYPE = "type";
    [SerializeField] private LanguageGame DEFAULT_LANGUAGE;

    [SerializeField] private bool _useInspectorLanguageDefault;
    [SerializeField] private List<LanguageGame> languageGames;


    public static List<LanguageGame> LanguageEnableGames;
    public static LanguageGame CurrentLanguage { get; private set; }

    public static event Action EventChangeLanguage;

    private static Dictionary<string, Dictionary<string, string>> dictionaryLocalization = new();
    public static bool IsAvailable { get; private set; }

    private static LanguageGame FindLanguage(SystemLanguage systemLanguageValue)
    {
        LanguageGame l = null;
        if (LanguageEnableGames != null)
            l = LanguageEnableGames.FirstOrDefault(x => x.SystemLanguage == systemLanguageValue);
        else
            l = Instance.languageGames.FirstOrDefault(x => x.SystemLanguage == systemLanguageValue);
        return l == null ? Instance.DEFAULT_LANGUAGE : l;
    }

    private static SystemLanguage GetLanguageById(string id)
    {
        var lang = LanguageEnableGames.FirstOrDefault(x => x.MainId == id);
        return lang == null ? Instance.DEFAULT_LANGUAGE.SystemLanguage : lang.SystemLanguage;
    }


    private static bool HasThereTranslation(SystemLanguage language)
    {
        return bool.Parse(GetTextTranslation(Instance.KEY_TYPE, language));
    }

    public static void Load(Dictionary<string, Dictionary<string, string>> data)
    {
        dictionaryLocalization = data;

        LanguageEnableGames = Instance.languageGames.Where(x => HasThereTranslation(x.SystemLanguage)).ToList();

        var lang = defaultLanguage;

        Instance.DEFAULT_LANGUAGE.MainId = lang;
        Instance.DEFAULT_LANGUAGE.SystemLanguage = GetLanguageById(lang);

        SystemLanguage systemLanguage = Instance._useInspectorLanguageDefault ? Instance.DEFAULT_LANGUAGE.SystemLanguage : Application.systemLanguage;
        CurrentLanguage = FindLanguage(systemLanguage) ?? FindLanguage(SystemLanguage.English);

        EventChangeLanguage?.Invoke();
        LoadedService?.Invoke();
        IsAvailable = true;
    }
  

    public static void ChangeLanguage(SystemLanguage newLanguage)
    {
        CurrentLanguage = FindLanguage(newLanguage);
    }
    public static string GetTextTranslation(string localizationKey, SystemLanguage? systemLanguage = null, params object[] args)
    {
        var languageKey = systemLanguage == null ? CurrentLanguage.MainId : FindLanguage(systemLanguage.Value).MainId;

        if (!dictionaryLocalization.ContainsKey(languageKey))
        {
            Debug.LogError("Language not found: " + languageKey);
            return null;
        }

        var translation = dictionaryLocalization[languageKey].ContainsKey(localizationKey)
            ? dictionaryLocalization[languageKey][localizationKey]
            : string.Empty;

        if (string.IsNullOrEmpty(translation))
        {
            Debug.LogWarning($"Translation not found: {localizationKey} ({languageKey}).");

            if (dictionaryLocalization[Instance.DEFAULT_LANGUAGE.MainId].ContainsKey(localizationKey))
                translation = dictionaryLocalization[Instance.DEFAULT_LANGUAGE.MainId][localizationKey];
            else
                translation = localizationKey;
        }

       return args == null || args.Length == 0 ? translation : string.Format(translation, args);
        
    }


    public void Init(Dictionary<string, Dictionary<string, string>> data)
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        Load(data);
    }
}