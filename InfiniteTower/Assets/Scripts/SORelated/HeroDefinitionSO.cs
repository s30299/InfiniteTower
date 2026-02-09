using UnityEngine;


[CreateAssetMenu(fileName = "New Hero Definition", menuName = "Defs/Hero Definition")]
public class HeroDefinitionSO : ScriptableObject
{
    public string heroId;
    public string heroName;
    public Sprite heroSprite;
    public int baseHealth;
    public int baseAttackPower;
    public int baseDefense;
    public HeroType heroType;
    public ElementType elementType;
    internal string id;
}
