using UnityEngine;

[CreateAssetMenu(fileName = "ChestScriptableObjectList", menuName = "ScriptableObjects/List/NewChestScriptableObjectList")]
public class ChestScriptableObjectList: ScriptableObject
{
    public ChestScriptableObject[] ChestList;
}
