using UnityEngine;
[CreateAssetMenu(fileName = "New Runes Definition", menuName = "Defs/Runes Definition")]
public class RuneDefinitionSO : ScriptableObject
{
    public string id;
    public string runeName;
    public Sprite runeSprite;
    public RarityType rarity;
}
