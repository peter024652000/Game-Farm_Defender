using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave Config")]

public class WaveConfig : ScriptableObject {


    [SerializeField]  List<PatternConfig> patternConfigs;
    [SerializeField] int normusNum;
    [SerializeField] int tinusNum;
    [SerializeField] int spikusNum;
    [SerializeField] int splitusNum;
    [SerializeField] int startingPattern = 0;
    [SerializeField] bool looping = false;


    public List<PatternConfig> GetPatternConfig()
    {
        return patternConfigs;
    }

    public int GetNormusNum()
    {
        return normusNum;
    }

    public int GetTinusNum()
    {
        return tinusNum;
    }

    public int GetSpikusNum()
    {
        return spikusNum;
    }

    public int GetSplitusNum()
    {
        return splitusNum;
    }

    public int GetStartingPattern()
    {
        return startingPattern;
    }

    public bool GetLooping()
    {
        return looping;
    }

}
