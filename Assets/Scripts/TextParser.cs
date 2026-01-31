using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName ="New parser", menuName ="Maikel/ParseText")]
public class TextParser : ScriptableObject
{
    [System.Serializable]
    public struct TextByKey
    {
        public string key;
        public string text;
    }


    [SerializeField]
    public List<TextByKey> parsedTexts;
    public void ParseText(string text)
    {
        Debug.Log($"TEXT HAS BEEN PARSED: \n{text}");
        parsedTexts = new List<TextByKey>();
        var grid = CSVReader.SplitCsvGrid(text);
        CSVReader.DebugOutputGrid(grid);
        int lines = grid.GetUpperBound(1);
        for(int i = 1; i < lines;i++)
        {
            parsedTexts.Add(new TextByKey()
            {
                key = grid[0,i],
                text = grid[1,i]
            });
        }        
    }

    public string GetTextByKey(string key)
    {
        TextByKey match = parsedTexts.Find(x => x.key == key);
        string toReturn = (match.text != null ? match.text: $"<ERROR: KEY {key} not found>");
        return toReturn;
    }
}
