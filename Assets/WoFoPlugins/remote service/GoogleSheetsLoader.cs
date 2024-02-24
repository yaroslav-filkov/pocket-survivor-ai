using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using Object = UnityEngine.Object;

public class GoogleSheetsLoader : MonoBehaviour
{
    public static event Action<string> LoadedStepService;
    public static bool IsLoadedData { get; private set; }

    #region ServiceSettings
    [SerializeField] private double _minPerSpeed = 0.01;
    [SerializeField] private double _maxLoadTime = 5;

    [SerializeField] private string _tableId;
    [SerializeField] private Sheet[] _sheetsLoading;
    [SerializeField] private Object _folderForCsv;

    private const string UrlPattern = "https://docs.google.com/spreadsheets/d/{0}/export?format=csv&gid={1}";
    #endregion

    private bool _isError;
    private int _localVersion;
    private int _remoteVersion;
    public string GetTableId()
    {
        return _tableId;
    }
    public void Sync()
    {
        StopAllCoroutines();
        StartCoroutine(SyncRemote());
    }
    private IEnumerator SyncRemote()
    {
        var dict = new Dictionary<string, UnityWebRequest>();
        var isOnline = Application.internetReachability != NetworkReachability.NotReachable;
        if (isOnline)
        {
           
            var startTime = Time.time;
            foreach (var sheet in _sheetsLoading)
            {
                LoadLocal(sheet);
                var url = string.Format(UrlPattern, _tableId, sheet.Id);

                dict.Add(url, UnityWebRequest.Get(url));
            }
            if(SheetAdapter.Read("general")["value"].TryGetValue("remote_data_version", out string val))
            {
                _localVersion = int.Parse(val);
            }
    

            foreach (var entry in dict)
            {
                var url = entry.Key;
                var request = entry.Value;
                if (!request.isDone)
                    yield return request.SendWebRequest();
        
                var currentTime = Time.time - startTime;

                if (currentTime >= _maxLoadTime)
                {
                    Debug.LogWarning("Download stopped due to long internet connection");
                    _isError = true;
                    break;
                }
               
                if (request.error == null)
                {
                    var sheet = _sheetsLoading.Single(i => url == string.Format(UrlPattern, _tableId, i.Id));
                    SheetAdapter.Data[sheet.Title] = request.downloadHandler.text;
                    PlayerPrefs.SetString(sheet.Title, request.downloadHandler.text);
                    var bytesPerSecond = request.downloadedBytes / request.downloadProgress / 1024;
                    if (bytesPerSecond < _minPerSpeed)
                    {
                        _isError = true;
                        break;
                    }
                    LoadedStepService?.Invoke(sheet.Title);
                    if (SheetAdapter.Read("general")["value"].TryGetValue("remote_data_version", out string valr))
                    {
                        _remoteVersion = int.Parse(valr);
                    }
                    if (_remoteVersion <= _localVersion)
                    {
                        Debug.LogWarning("the local version is no different from the remote one");
                        _isError = true;
                        break;
                    }
                }
                else
                {
                    
                    _isError = true;
                    break;
                }
            }

            if (_isError)
            {
                Debug.LogWarning("Download stopped!");
                foreach (var sheet in _sheetsLoading)
                {
                    LoadLocal(sheet);
                    LoadedStepService?.Invoke(sheet.Title);
                 
                }
            }
        }
        else
        {
            foreach (var sheet in _sheetsLoading)
            {
                LoadLocal(sheet);
                LoadedStepService?.Invoke(sheet.Title);
            }
        }

        IsLoadedData = true;
    }

    private void LoadLocal(in Sheet sheet)
    {
        if (PlayerPrefs.HasKey(sheet.Title))
        {
            SheetAdapter.Data[sheet.Title] = PlayerPrefs.GetString(sheet.Title, _tableId);
        }
        else
        {
            SheetAdapter.Data[sheet.Title] = sheet.TextAsset.text;
            PlayerPrefs.SetString(sheet.Title, sheet.TextAsset.text);
        }
    }


#if UNITY_EDITOR
    public void SyncCsvFiles()
    {
        StartCoroutine(LoadFiles());
    }
    private IEnumerator LoadFiles()
    {
        if (_folderForCsv != null)
        {
            string folderPath = AssetDatabase.GetAssetPath(_folderForCsv.GetInstanceID());

            int totalFiles = _sheetsLoading.Length;
            int filesDownloaded = 0;

            foreach (var sheet in _sheetsLoading)
            {
                var url = string.Format(UrlPattern, _tableId, sheet.Id);
                var request = UnityWebRequest.Get(url);
                if (!request.isDone)
                    yield return request.SendWebRequest();
                while (!request.isDone || request.downloadProgress < 1)
                {
                    float progress = (float)(filesDownloaded + request.downloadProgress) / totalFiles;
                    UpdateProgressUI(progress);
                    yield return null;
                }

                if (request.error == null)
                {
                    string filePath = Path.Combine(folderPath, sheet.Title + ".csv");
                    string fileContent = request.downloadHandler.text;
                    File.WriteAllText(filePath, fileContent);
                    Debug.Log($"Loaded CSV file {filePath}");

                    filesDownloaded++;
                }
                else
                {
                    Debug.LogWarning($"Error loading CSV file {url}: {request.error}");
                }

                float currentProgress = (float)filesDownloaded / totalFiles;
                UpdateProgressUI(currentProgress);
            }

            UpdateProgressUI(1);
        }
    }

    private void UpdateProgressUI(float progress)
    {
        Debug.Log($"Loading progress: {progress:P}");
    }
#endif
}