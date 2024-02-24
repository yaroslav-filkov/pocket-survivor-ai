using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public static class SheetAdapter
{
    public static Dictionary<string, string> Data = new ();
    private static string GetTypeParam(Dictionary<string, Dictionary<string, string>> dictionary, string id_param)
    {
        if (!dictionary.ContainsKey(id_param))
        {
            Debug.LogError("Weapon not found: " + id_param);
            throw new Exception($"Param not found: ({id_param}).");
        }

        return dictionary[id_param]["type"];
    }
    public static Dictionary<string, Dictionary<string, string>> Read(string fileName, Dictionary<string, string> customData = null)
    {
        string allText =  (customData ?? Data)[fileName];

        Dictionary<string, Dictionary<string, string>> dictionary = new();
        var text = allText.Replace("\r\n", "\n").Replace("\"\"", "[_quote_]");
        var matches = Regex.Matches(text, "\"[\\s\\S]+?\"");

        foreach (Match match in matches)
            text = text.Replace(match.Value,
                match.Value.Replace("\"", null).Replace(",", "[_comma_]").Replace("\n", "[_newline_]"));
        text = text.Replace("。", "。 ").Replace("、", "、 ").Replace("：", "： ").Replace("！", "！ ").Replace("（", " （")
            .Replace("）", "） ").Trim();

        var lines = text.Split('\n').Where(i => i != "").ToList();
        var languages = lines[0].Split(',').Select(i => i.Trim()).ToList();

        for (var i = 1; i < languages.Count; i++)
            if (!dictionary.ContainsKey(languages[i]))
                dictionary.Add(languages[i], new Dictionary<string, string>());

        for (var i = 1; i < lines.Count; i++)
        {
            var columns = lines[i].Split(',').Select(j => j.Trim()).Select(j =>
                j.Replace("[_quote_]", "\"").Replace("[_comma_]", ",").Replace("[_newline_]", "\n")).ToList();
            var key = columns[0];

            if (key == "") continue;

            for (var j = 1; j < languages.Count; j++)
            {
                dictionary[languages[j]].Add(key, columns[j]);
            }
        }

        return dictionary;
    }

}