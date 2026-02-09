using UnityEngine;
using System.Collections.Generic;

public class ProfileManager : MonoBehaviour
{
    public static ProfileManager Instance { get; private set; }
    public PlayerProfile Profile { get; private set; }
    private void Awake()
    {
        // if (Instance == null)
        // {
        //     Destroy(gameObject);
        //     return;
        // }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        Profile = SaveSystem.LoadProfile();
        Profile.heroesById ??= new Dictionary<string, HeroProgress>();
        Profile.itemsByUid ??= new Dictionary<string, ItemInstance>();
        Profile.runesByUid ??= new Dictionary<string, RuneInstance>();
    }

    private void OnApplicationFocus(bool focus)
    {
        SaveSystem.SaveProfile(Profile);
    }
}
