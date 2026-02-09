using System.Collections.Generic;
using UnityEngine;

public class PlayerProfile 
{
    public Dictionary<string, HeroProgress> heroesById = new();
    public Dictionary<string, ItemInstance> itemsByUid = new();
    public Dictionary<string, RuneInstance> runesByUid = new();
}
