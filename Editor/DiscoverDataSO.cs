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

    [ContextMenu("Export Data To CSV")]
    void ExportToCSV()
    {
        string path = EditorUtility.SaveFilePanel(
            "Export Discover Data to CSV",
            "Assets",
            $"{name}.csv",
            "csv");

        if (string.IsNullOrEmpty(path)) return;

        using var writer = new StreamWriter(path, false, new UTF8Encoding(true));
        writer.WriteLine("Name,UniqueKey,Description,SectionsContent");

        foreach (var c in discoverContents)
        {
            string sections = string.Join("\\n",
                c.Sections.ConvertAll(s =>
                    $"{EscapeCsv(s.Name)}:{EscapeCsv(s.Description)}"));

            writer.WriteLine($"{EscapeCsv(c.Name)}," +
                           $"{EscapeCsv(c.UniqueKey)}," +
                           $"{EscapeCsv(c.Description)}," +
                           $"{EscapeCsv(sections)}");
        }

        EditorUtility.DisplayDialog("Export Success",
            $"Exported {discoverContents.Count} entries to:\n{path}", "OK");
    }

    [ContextMenu("Import Data From CSV")]
    void ImportFromCSV()
    {
        string path = EditorUtility.OpenFilePanel(
            "Import Discover Data from CSV", "Assets", "csv");

        if (string.IsNullOrEmpty(path)) return;

        discoverContents.Clear();
        var lines = File.ReadAllLines(path, Encoding.UTF8);

        for (int i = 1; i < lines.Length; i++) // skip header
        {
            var cols = ParseCsvLine(lines[i]);
            if (cols.Length < 4) continue;

            var content = new DiscoverContent
            {
                Name = cols[0],
                UniqueKey = string.IsNullOrEmpty(cols[1]) ? cols[0] : cols[1],
                Description = cols[2],
                Sections = new List<SectionsContent>()
            };

            if (!string.IsNullOrWhiteSpace(cols[3]))
            {
                var pairs = cols[3].Split(new[] { "\\n" }, System.StringSplitOptions.None);
                foreach (var p in pairs)
                {
                    var kv = p.Split(new[] { ':' }, 2);
                    if (kv.Length != 2) continue;
                    content.Sections.Add(new SectionsContent
                    {
                        Name = kv[0],
                        Description = kv[1]
                    });
                }
            }
            discoverContents.Add(content);
        }

        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();

        EditorUtility.DisplayDialog("Import Success",
            $"Imported {discoverContents.Count} entries from:\n{path}", "OK");
    }

    private static string EscapeCsv(string s) =>
        string.IsNullOrEmpty(s) ? "" : "\"" + s.Replace("\"", "\"\"") + "\"";

    private static string[] ParseCsvLine(string line)
    {
        var result = new List<string>();
        var sb = new StringBuilder();
        bool inQuotes = false;

        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];
            if (c == '"')
            {
                if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
                { sb.Append('"'); i++; }
                else inQuotes = !inQuotes;
            }
            else if (c == ',' && !inQuotes)
            { result.Add(sb.ToString()); sb.Clear(); }
            else sb.Append(c);
        }
        result.Add(sb.ToString());
        return result.ToArray();
    }
}
