using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class RunFactory
{
    private readonly DefsDbSO _defsDb;
    private readonly PlayerProfile _playerProfile;

    public RunFactory(DefsDbSO defsDb, PlayerProfile playerProfile)
    {
        _defsDb = defsDb;
        _playerProfile = playerProfile;
    }

    public RunState CreateNewRun(List<string> PartyHeroIds)
    {
        if(_defsDb == null)
        {
            Debug.LogError("RunFactory: DefsDbSO reference is null. Cannot create run.");
            return null;
        }
        if(_playerProfile == null)
        {
            Debug.LogError("RunFactory: PlayerProfile reference is null. Cannot create run.");
            return null;
        }
        if(PartyHeroIds == null || PartyHeroIds.Count == 0)
        {
            Debug.LogError("RunFactory: PartyHeroIds is null or empty. Cannot create run without heroes.");
            return null;
        }
        if(_playerProfile.heroesById == null)
        {
            Debug.LogWarning("RunFactory: PlayerProfile.heroesById is null. Initializing to empty dictionary.");
            _playerProfile.heroesById = new Dictionary<string, HeroProgress>();
        }


        var run = new RunState(seed: Random.Range(int.MinValue, int.MaxValue));
        foreach (var heroId in PartyHeroIds)
        {
            var heroDef = _defsDb.TryGetHeroDef(heroId, out var def) ? def : null;
            if (heroDef == null)
            {
                Debug.LogError($"RunFactory: Invalid heroId='{heroId}' in CreateNewRun. Skipping.");
                continue;
            }
            var prog = _playerProfile.heroesById.TryGetValue(heroId, out var heroProg) ? heroProg : null;
            if (prog == null)            {
                Debug.LogWarning($"RunFactory: No progress found for heroId='{heroId}' in CreateNewRun. Using default progress.");
                prog = new HeroProgress();
            }
            var rh = new RunHero();
            rh.heroId = heroId;
            ApplyBase(def, rh.runtimeStats);
            ApplyLevel(prog, rh.runtimeStats);
            ApplyEquipment(prog, rh.runtimeStats);
            ApplyRunes(prog, rh.runtimeStats);
            rh.runtimeStats.currentHealth = rh.runtimeStats.maxHealth;
            run.party.Add(rh);
            Debug.Log($"RunFactory: Added heroId='{heroId}' Name={def.name} to new run with stats: Health={rh.runtimeStats.maxHealth}, Attack={rh.runtimeStats.attack}, Defense={rh.runtimeStats.defense}");

            
        }
        return run;
    }

    private void ApplyBase(HeroDefinitionSO def, RuntimeStats stats)
    {
        stats.maxHealth = def.baseHealth;
        stats.attack = def.baseAttackPower;
        stats.defense = def.baseDefense;
    }
    private void ApplyLevel(HeroProgress prog, RuntimeStats stats)
    {
        stats.maxHealth += prog.level * 10;
        stats.attack += prog.level * 2;
        stats.defense += prog.level * 2;
    }
    private void ApplyEquipment(HeroProgress prog, RuntimeStats stats)
    {
        // prog.equippedItems ??= new Dictionary<ItemType, string>();
        // prog.equippedRunes ??= new List<string>();
        // if (prog.equippedItems.TryGetValue(ItemType.Weapon, out var weaponUid) && !string.IsNullOrEmpty(weaponUid))
        // {
        //     if(!_playerProfile.itemsById.TryGetValue(weaponUid, out var weaponInstance))
        //     {
        //         Debug.LogWarning($"RunFactory: Equipped weapon with uid='{weaponUid}' not found in player profile. Skipping.");
        //     }
        //     else if (!_defsDb.TryGetItemDef(weaponInstance.itemId, out var weaponDef))
        //     {
        //         Debug.LogWarning($"RunFactory: Item definition for equipped weaponId='{weaponInstance.itemId}' not found. Skipping.");
        //     }
        //     else
        //     {
        //         stats.attack += weaponDef.baseAttackValue;
        //         stats.defense += weaponDef.baseDefenseValue;
        //         stats.maxHealth += weaponDef.baseHealthValue;
        //     }
        // }
        prog.EnusureDefaults();
        ApplySlot(ItemType.Weapon);
        ApplySlot(ItemType.Helmet);
        ApplySlot(ItemType.Chestplate);
        ApplySlot(ItemType.Boots);
        ApplySlot(ItemType.Gloves);

        void ApplySlot(ItemType slot)
        {
            var uid = prog.equippedItems[slot];
            if (string.IsNullOrEmpty(uid)) return;
            if(!_playerProfile.itemsByUid.TryGetValue(uid, out var itemInstance) || itemInstance == null)
            {
                Debug.LogWarning($"RunFactory: Missing ItemInstance, Equipped item with uid='{uid}' not found in player profile . Skipping.");
                prog.equippedItems[slot] = ""; // Clear invalid uid to prevent repeated warnings
                return;
            }
            if (!_defsDb.TryGetItemDef(itemInstance.itemId, out var itemDef) || itemDef == null)
            {
                Debug.LogWarning($"RunFactory: Missing ItemDefinition, Item definition for equipped itemId='{itemInstance.itemId}' not found. Skipping.");
                return;
            }
            stats.attack += itemDef.baseAttackValue;
            stats.defense += itemDef.baseDefenseValue;
            stats.maxHealth += itemDef.baseHealthValue;
        }        
    }
    private void ApplyRunes(HeroProgress prog, RuntimeStats stats)
    {
        prog.EnusureDefaults();
        for( int slot = 0; slot < 6; slot++)
        {
            var uid = prog.equippedRunes[slot];
            if (string.IsNullOrEmpty(uid)) continue;
            if(!_playerProfile.runesByUid.TryGetValue(uid, out var runeInstance) || runeInstance == null)
            {
                Debug.LogWarning($"RunFactory: Missing RuneInstance, Equipped rune with uid='{uid}' not found in player profile . Skipping.");
                prog.equippedRunes[slot] = ""; // Clear invalid uid to prevent repeated warnings
                continue;
            }
            if (!_defsDb.TryGetRuneDef(runeInstance.runeId, out var runeDef) || runeDef == null)
            {
                Debug.LogWarning($"RunFactory: Missing RuneDefinition, Rune definition for equipped runeId='{runeInstance.runeId}' not found. Skipping.");
                continue;
            }
            stats.attack += stats.attack * runeDef.basePercentAttackBoost / 100;
            stats.defense += stats.defense * runeDef.basePercentDefenseBoost / 100;
            stats.maxHealth += stats.maxHealth * runeDef.basePercentHealthBoost / 100;
        }   
    }
}