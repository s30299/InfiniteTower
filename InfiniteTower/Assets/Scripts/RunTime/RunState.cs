using System.Collections.Generic;
using UnityEngine;

public class RunState 
{
    public int seed;
    public int floor;
    public List<RunHero> party = new();
    public List<string> tempRunBonusses = new();

    public RunState(int seed, int floor = 1)
    {
        this.seed = seed;
        this.floor = floor;
    }
}
