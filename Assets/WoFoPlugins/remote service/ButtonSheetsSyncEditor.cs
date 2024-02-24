#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GoogleSheetsLoader))]
public class ButtonSheetsSyncEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var component = (GoogleSheetsLoader)target;

        GUI.backgroundColor = Color.green;
        if (GUILayout.Button("LOAD GOOGLE SHEETS")) component.SyncCsvFiles();
    }
}
#endif