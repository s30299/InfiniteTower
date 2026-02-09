using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HeroProgress
{
    public string heroId;
    public int level;
    public int experience;
    public Dictionary<ItemType, string> equippedItems = new();
    public List<string> equippedRunes = new(6);

    public void EnusureDefaults()
    {
        equippedItems ??= new Dictionary<ItemType, string>();
        equippedRunes ??= new List<string>(6);
        foreach (ItemType itemType in Enum.GetValues(typeof(ItemType)))
        {
            if (!equippedItems.ContainsKey(itemType))
            {
                equippedItems[itemType] = "";
            }
        }
            while (equippedRunes.Count < 6)
            {
                equippedRunes.Add("");
            }
    }
}
