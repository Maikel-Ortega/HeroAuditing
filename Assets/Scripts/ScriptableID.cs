using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableID", menuName = "Scriptable Objects/ScriptableID")]
public class ScriptableID : ScriptableObject
{
    public string entityDisplayName = "";
    public override string ToString()
    {
        return entityDisplayName;
    }
}
