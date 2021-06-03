using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;

public class PoliceCaughtCounter : MonoBehaviour
{
    public int todayCaughtTimes;
    private int _overallCaughtTimes;

    private void Awake()
    {
        _overallCaughtTimes = DataManager.Instance.LoadPoliceCaughtNumber();
    }

    public void IncrementPoliceCaughtNumber()
    {
        todayCaughtTimes++;
        _overallCaughtTimes++;
        DataManager.Instance.SavePoliceCaughtNumber(_overallCaughtTimes);
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