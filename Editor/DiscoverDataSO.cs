using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public class DiscoverDataSO : ScriptableObject
{
    public List<DiscoverContent> discoverContents = new List<DiscoverContent>();

    [System.Serializable]
    public class DiscoverContent
    {
        public string Name;
        public string UniqueKey;
        [TextArea(5, 50)] public string Description;
        public List<SectionsContent> Sections = new List<SectionsContent>();
    }

    [System.Serializable]
    public class SectionsContent
    {
        public string Name;
        [TextArea(5, 50)] public string Description;
    }

    // ────────────────────────
    // Right-click → Export to JSON
    // ────────────────────────
    [ContextMenu("Export Data To JSON")]
    void ExportToJson()
    {
        string path = EditorUtility.SaveFilePanel(
            "Export Discover Data to JSON",
            "Assets",
            $"{name}.json",
            "json");

        if (string.IsNullOrEmpty(path)) return;

        // 直接序列化整個 SO
        string json = JsonUtility.ToJson(this, prettyPrint: true);
        File.WriteAllText(path, json, System.Text.Encoding.UTF8);

        EditorUtility.DisplayDialog("Export Success",
            $"Exported {discoverContents.Count} entries to:\n{path}", "OK");

        Debug.Log($"[Discover] JSON exported: {path}");
    }

    // ────────────────────────
    // Right-click → Import from JSON
    // ────────────────────────
    [ContextMenu("Import Data From JSON")]
    void ImportFromJson()
    {
        string path = EditorUtility.OpenFilePanel(
            "Import Discover Data from JSON", "Assets", "json");

        if (string.IsNullOrEmpty(path)) return;

        if (!File.Exists(path))
        {
            EditorUtility.DisplayDialog("Error", "File not found!", "OK");
            return;
        }

        string json = File.ReadAllText(path, System.Text.Encoding.UTF8);

        try
        {
            // 直接反序列化到當前 SO
            JsonUtility.FromJsonOverwrite(json, this);

            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();

            EditorUtility.DisplayDialog("Import Success",
                $"Imported {discoverContents.Count} entries from:\n{path}", "OK");

            Debug.Log($"[Discover] JSON imported: {path}");
        }
        catch (System.Exception e)
        {
            EditorUtility.DisplayDialog("Import Failed",
                $"JSON format error:\n{e.Message}", "OK");
            Debug.LogError($"[Discover] Import failed: {e}");
        }
    }
}
