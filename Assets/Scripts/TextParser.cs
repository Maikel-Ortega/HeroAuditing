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
        public string englishText;
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
                text = grid[2,i],
                englishText = grid[4,i]
            });
        }        
    }

    

    public string GetTextByKey(string key, bool englishText=true)
    {
        TextByKey match = parsedTexts.Find(x => x.key == key);
        if(englishText)
        {
            string toReturn = (match.englishText != null ? match.englishText : $"<ERROR: KEY {key} not found>");
            return toReturn;
        }
        else
        {
        string toReturn = (match.text != null ? match.text: $"<ERROR: KEY {key} not found>");
        return toReturn;

        }
    }
}
