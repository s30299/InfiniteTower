using UnityEngine;

[CreateAssetMenu(fileName = "New Item Definition", menuName = "Defs/Item Definition")]
public class ItemDefinitionSO : ScriptableObject
{
    public string id;
    public string itemName;
    public Sprite itemSprite;
    public ItemType itemType;
    public RarityType rarity;
    public int baseAttackValue;
    public int baseDefenseValue;
    public int baseHealthValue;
    public int baseLevel = 1;
    public string description; 
}
