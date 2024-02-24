using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;


public class Bootstrap : MonoBehaviour
{
    [SerializeField] private string _loadingScene;

    [SerializeField] private LocalizationService _localizationService;
    [SerializeField] private GoogleSheetsLoader _googleSheetsLoader;

    private void Start()
    {
        StartCoroutine(LoadService());
    }


    private void OnEnable()
    {
        GoogleSheetsLoader.LoadedStepService += LoadedStepService;
    }
    private void OnDisable()
    {
        GoogleSheetsLoader.LoadedStepService -= LoadedStepService;
    }

    private void LoadedStepService(string id)
    {
        if (id == GeneralGameConfig.KEY_GENERAL_REMOTE)
        {
            if (GeneralGameConfig.IsLoadedData == false)
            {
                GeneralGameConfig.Load();
            }
        }
        if (id == LocalizationService.ID_LOCALIZATION)
        {
            if (LocalizationService.IsAvailable == false)
            {
                _localizationService.Init(SheetAdapter.Read(LocalizationService.ID_LOCALIZATION));
            }
        }
      
    }

    private IEnumerator LoadService()
    {
        _googleSheetsLoader.Sync();
        yield return new WaitUntil(() => GoogleSheetsLoader.IsLoadedData);

        SaveService.Init();
        SceneManager.LoadScene(_loadingScene);
    }
}
