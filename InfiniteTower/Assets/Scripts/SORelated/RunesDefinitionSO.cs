using System.Buffers.Text;
using UnityEngine;
[CreateAssetMenu(fileName = "New Runes Definition", menuName = "Defs/Runes Definition")]
public class RuneDefinitionSO : ScriptableObject
{
    public string id;
    public string runeName;
    public Sprite runeSprite;
    public RarityType rarity;
    public int basePercentAttackBoost;
    public int basePercentDefenseBoost;
    public int basePercentHealthBoost;
    public int baseLevel = 1;
    
}
