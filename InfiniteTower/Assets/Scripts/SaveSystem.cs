using System.IO;
using Newtonsoft.Json;
using UnityEngine;
public static class SaveSystem
{
    private static string PathProfile => System.IO.Path.Combine(Application.persistentDataPath, "profile.json");

    public static void SaveProfile(PlayerProfile profile)
    {
        var json = JsonConvert.SerializeObject(profile, Formatting.Indented);
        File.WriteAllText(PathProfile, json);
    }

    public static PlayerProfile LoadProfile()
    {
        if (!File.Exists(PathProfile))
        {
            Debug.LogWarning("No profile found. Returning a new profile.");
            return new PlayerProfile();
        }

        var json = File.ReadAllText(PathProfile);
        return JsonConvert.DeserializeObject<PlayerProfile>(json) ?? new PlayerProfile();
    }
    
}
