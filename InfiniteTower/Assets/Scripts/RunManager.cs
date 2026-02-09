using UnityEngine;

public class RunManager : MonoBehaviour
{
    public static RunManager Instance { get; private set; }
    public RunState CurentRun;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void StartRun(RunState run)
    {
        CurentRun = run;
    }
}
