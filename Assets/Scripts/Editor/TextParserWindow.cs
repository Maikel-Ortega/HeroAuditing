using Unity.Plastic.Antlr3.Runtime;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using static Unity.VisualScripting.Member;

public class TextParserWindow : EditorWindow
{

    public TextAsset source;
    public TextParser textParser;
    [MenuItem("Maikel/TextParserWindow")]
    static void Init()
    {
        var window = GetWindowWithRect<TextParserWindow>(new Rect(0, 0, 165, 100));
        window.Show();
    }

    public void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        source = EditorGUILayout.ObjectField(source, typeof(TextAsset), true) as TextAsset;
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        textParser = EditorGUILayout.ObjectField(textParser, typeof(TextParser), true) as TextParser;
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Parse!"))
        {
            textParser.ParseText(source.text);
            EditorUtility.SetDirty(textParser);
            AssetDatabase.SaveAssets();
        }
    }

}

