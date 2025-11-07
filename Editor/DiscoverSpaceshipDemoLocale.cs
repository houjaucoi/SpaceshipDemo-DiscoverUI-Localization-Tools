using GameplayIngredients;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DiscoverSpaceshipDemoLocale : MonoBehaviour
{
    private const string TARGET_SCENE = "Spaceship";

    // ── Create Default ─────────────────────────────────────
    [MenuItem("Help/Discover Spaceship Demo Locale/Create Default Document Asset", priority = 0)]
    static void CreateDiscoverDefalutDocumentAsset()
    {
        if (SceneManager.GetActiveScene().name != TARGET_SCENE)
        {
            EditorUtility.DisplayDialog("Wrong Scene",
                $"Please open the \"{TARGET_SCENE}\" scene first.", "OK");
            return;
        }

        var asset = ScriptableObject.CreateInstance<DiscoverDataSO>();
        var all = FindObjectsOfType<Discover>();

        foreach (var d in all)
        {
            var c = new DiscoverDataSO.DiscoverContent
            {
                Name = d.Name,
                UniqueKey = d.Name,
                Description = d.Description,
                Sections = new List<DiscoverDataSO.SectionsContent>()
            };

            foreach (var s in d.Sections)
                c.Sections.Add(new DiscoverDataSO.SectionsContent
                {
                    Name = s.SectionName,
                    Description = s.SectionContent
                });

            asset.discoverContents.Add(c);
        }

        string path = AssetDatabase.GenerateUniqueAssetPath("Assets/DiscoverDocumentAsset_en.asset");
        AssetDatabase.CreateAsset(asset, path);
        AssetDatabase.SaveAssets();
        Selection.activeObject = asset;
        EditorUtility.FocusProjectWindow();

        EditorUtility.DisplayDialog("Success",
            $"Default English asset created:\n{path}", "OK");
    }

    // ── Convert to any locale ─────────────────────────────
    private static void ConvertToLocale(string localeID)
    {
        string path = $"Assets/DiscoverDocumentAsset_{localeID}.asset";
        var localeAsset = AssetDatabase.LoadAssetAtPath<DiscoverDataSO>(path);

        if (localeAsset == null)
        {
            EditorUtility.DisplayDialog("File Not Found",
                $"Cannot find:\n{path}", "OK");
            return;
        }

        var inScene = FindObjectsOfType<Discover>();
        if (inScene.Length == 0)
        {
            EditorUtility.DisplayDialog("No Discover",
                "No Discover components in the current scene.", "OK");
            return;
        }

        var map = inScene.ToDictionary(d => d.Name, d => d);
        int applied = 0;

        foreach (var c in localeAsset.discoverContents)
        {
            if (!map.TryGetValue(c.UniqueKey, out Discover target))
            {
                Debug.LogWarning($"[Locale] Not found: {c.UniqueKey}");
                continue;
            }

            target.Description = c.Description;
            for (int i = 0; i < c.Sections.Count && i < target.Sections.Length; i++)
            {
                target.Sections[i].SectionName = c.Sections[i].Name;
                target.Sections[i].SectionContent = c.Sections[i].Description;
            }
            EditorUtility.SetDirty(target);
            applied++;
        }

        EditorUtility.DisplayDialog("Success",
            $"Applied {applied}/{localeAsset.discoverContents.Count} entries\n" +
            $"Language: {NiceName(localeID)}\n\n" +
            "Remember to SAVE the scene!", "OK");
    }

    private static string NiceName(string id) => id switch
    {
        "zh-cht" => "Traditional Chinese",
        "en" => "English",
        _ => id
    };

    // ── Menu entries ───────────────────────
    private static bool CheckDefaultExists()
    {
        return File.Exists("Assets/DiscoverDocumentAsset_en.asset");
    }

    [MenuItem("Help/Discover Spaceship Demo Locale/Convert to Traditional Chinese", priority = 11)]
    static void ToChinese() => ConvertToLocale("zh-cht");

    [MenuItem("Help/Discover Spaceship Demo Locale/Convert to Traditional Chinese", validate = true)]
    static bool ToChinese_Validate() => CheckDefaultExists();

    [MenuItem("Help/Discover Spaceship Demo Locale/Convert to English", priority = 12)]
    static void ToEnglish() => ConvertToLocale("en");

    [MenuItem("Help/Discover Spaceship Demo Locale/Convert to English", validate = true)]
    static bool ToEnglish_Validate() => CheckDefaultExists();
}
