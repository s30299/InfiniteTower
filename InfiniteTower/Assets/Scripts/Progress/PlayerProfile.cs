using System.Collections.Generic;
using UnityEngine;

public class PlayerProfile 
{
    public Dictionary<string, HeroProgress> heroesById = new();
    public Dictionary<string, ItemInstance> itemsById = new();
    public Dictionary<string, RuneInstance> runesById = new();
}
