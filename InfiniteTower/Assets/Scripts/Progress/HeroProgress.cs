using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HeroProgress
{
    public string heroId;
    public int level;
    public int experience;
    public Dictionary<ItemType, string> equippedItems;
    public List<string> equippedRunes;
}
