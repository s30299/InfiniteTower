using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefsDbSO", menuName = "Defs/DefsDbSO", order = 100)]
public class DefsDbSO : ScriptableObject
{
    [Header("Definitions")]
    public List<HeroDefinitionSO> heroDefinitions = new();
    public List<ItemDefinitionSO> itemDefinitions = new();
    public List<RuneDefinitionSO> runeDefinitions = new();

    // Cache (runtime-only)
    [NonSerialized] private Dictionary<string, HeroDefinitionSO> _heroDefsById;
    [NonSerialized] private Dictionary<string, ItemDefinitionSO> _itemDefsById;
    [NonSerialized] private Dictionary<string, RuneDefinitionSO> _runeDefsById;

    public void BuildCache()
    {
        _heroDefsById = BuildMap(heroDefinitions, h => h != null ? h.heroId : null, "HeroDefinitionSO.heroId");
        _itemDefsById = BuildMap(itemDefinitions, i => i != null ? i.id : null, "ItemDefinitionSO.id");
        _runeDefsById = BuildMap(runeDefinitions, r => r != null ? r.id : null, "RuneDefinitionSO.id");
    }

    public bool TryGetHeroDef(string heroId, out HeroDefinitionSO def)
    {
        EnsureCache();
        return _heroDefsById.TryGetValue(heroId, out def);
    }

    public HeroDefinitionSO GetHeroDef(string heroId)
    {
        EnsureCache();
        if (_heroDefsById.TryGetValue(heroId, out var def)) return def;
        Debug.LogError($"DefsDbSO: Missing HeroDefinitionSO heroId='{heroId}'");
        return null;
    }

    public bool TryGetItemDef(string itemId, out ItemDefinitionSO def)
    {
        EnsureCache();
        return _itemDefsById.TryGetValue(itemId, out def);
    }

    public ItemDefinitionSO GetItemDef(string itemId)
    {
        EnsureCache();
        if (_itemDefsById.TryGetValue(itemId, out var def)) return def;
        Debug.LogError($"DefsDbSO: Missing ItemDefinitionSO itemId='{itemId}'");
        return null;
    }

    public bool TryGetRuneDef(string runeId, out RuneDefinitionSO def)
    {
        EnsureCache();
        return _runeDefsById.TryGetValue(runeId, out def);
    }

    public RuneDefinitionSO GetRuneDef(string runeId)
    {
        EnsureCache();
        if (_runeDefsById.TryGetValue(runeId, out var def)) return def;
        Debug.LogError($"DefsDbSO: Missing RuneDefinitionSO runeId='{runeId}'");
        return null;
    }

    private void EnsureCache()
    {
        if (_heroDefsById == null || _itemDefsById == null || _runeDefsById == null)
            BuildCache();
    }

    private Dictionary<string, T> BuildMap<T>(
        List<T> list,
        Func<T, string> keySelector,
        string keyName
    ) where T : UnityEngine.Object
    {
        // Ordinal = najszybsze i najbezpieczniejsze dla ID (bez kultur/PL znaków)
        var map = new Dictionary<string, T>(StringComparer.Ordinal);

        if (list == null) return map;

        for (int i = 0; i < list.Count; i++)
        {
            var obj = list[i];
            if (obj == null) continue;

            var key = keySelector(obj);

            if (string.IsNullOrWhiteSpace(key))
            {
                Debug.LogError($"DefsDbSO: Empty {keyName} at index {i} (asset '{obj.name}')");
                continue;
            }

            if (map.ContainsKey(key))
            {
                Debug.LogError($"DefsDbSO: Duplicate {keyName}='{key}' (asset '{obj.name}')");
                continue;
            }

            map.Add(key, obj);
        }

        return map;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        // Uwaga: OnValidate odpala się często. Ta wersja jest OK, bo BuildCache jest lekkie.
        try
        {
            BuildCache();
        }
        catch (Exception e)
        {
            Debug.LogError($"DefsDbSO: Error building cache: {e}");
        }
    }
#endif
}
