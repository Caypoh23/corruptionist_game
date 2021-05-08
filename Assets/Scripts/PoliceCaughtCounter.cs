using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceCaughtCounter : MonoBehaviour
{
    public int todayCaughtTimes;
    private int _overallCaughtTimes;

    public void IncrementPoliceCaughtNumber()
    {
        todayCaughtTimes++;
        _overallCaughtTimes++;
    }
    
    public int GetOverallCaughtNumber()
    {
        return _overallCaughtTimes;
    }
    
    public int GetTodayCaughtNumber()
    {
        return todayCaughtTimes;
    }
}