using UnityEngine;
using UnityEngine.UI;

/* Scriptable Object for Chest */

[CreateAssetMenu(fileName = "ChestScriptableObject", menuName = "ScriptableObjects/NewChestScriptableObject")]
public class ChestScriptableObject: ScriptableObject
{
    public Vector2Int Chest_Coins_Range;
    public Vector2Int Chest_Gems_Range;
    public int Max_Unlock_Time;
    public int Max_Gems_To_Unlock;
    public Sprite Chest_Sprite;
    public ChestType Chest_Type;
}
