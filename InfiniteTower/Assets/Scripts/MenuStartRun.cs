using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuStartRun : MonoBehaviour
{
    [SerializeField] private DefsDbSO defsDb;

    public List<string> partyHeroIds = new();

    private void Start()
    {
        AddTestHeroes();
    }

    public void StartBattleRun()
    {   
        Debug.Log($"MenuStartRun: defsDb={(defsDb != null ? "OK" : "NULL")} playerProfile={(ProfileManager.Instance.Profile != null ? "OK" : "NULL")} partyHeroIds.Count={partyHeroIds.Count}");
        if(partyHeroIds.Count == 0)
        {
            Debug.LogError("MenuStartRun: Cannot start run with empty partyHeroIds.");
            return;
        }
        var runFactory = new RunFactory(defsDb, ProfileManager.Instance.Profile);
        var run = runFactory.CreateNewRun(partyHeroIds);
        RunManager.Instance.StartRun(run);
        SceneManager.LoadScene("BattleModeScene");
    }

    private void AddTestHeroes()
    {
        partyHeroIds.Clear();
        if(defsDb.TryGetHeroDef("warrior", out var knightDef) && knightDef != null)
        {
            partyHeroIds.Add("warrior");
        }
        else
        {
            Debug.LogWarning("MenuStartRun: Test hero 'warrior' not found in defsDb. Cannot add to partyHeroIds.");
        }
        if(defsDb.TryGetHeroDef("archer", out var archerDef) && archerDef != null)
        {
            partyHeroIds.Add("archer");
        }
        else
        {
            Debug.LogWarning("MenuStartRun: Test hero 'archer' not found in defsDb. Cannot add to partyHeroIds.");
        }
    }
}
