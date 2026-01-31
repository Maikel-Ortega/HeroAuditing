using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableDialog", menuName = "Scriptable Objects/ScriptableDialog")]
public class ScriptableDialog : ScriptableObject
{
    public List<string> dialogIDs;
}
