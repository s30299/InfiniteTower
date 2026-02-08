using UnityEngine;

[CreateAssetMenu(fileName = "New Item Definition", menuName = "Defs/Item Definition")]
public class ItemDefinitionSO : ScriptableObject
{
    public string id;
    public string itemName;
    public Sprite itemSprite;
    public ItemType itemType;
    public RarityType rarity;
}
