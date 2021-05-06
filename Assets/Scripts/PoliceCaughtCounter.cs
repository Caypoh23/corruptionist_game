using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceCaughtCounter : MonoBehaviour
{
    private int _policeCaughtTimes;

    public void IncrementPoliceCaughtNumber()
    {
        _policeCaughtTimes++;
    }
    
    public int GetPoliceCaughtNumber()
    {
        return _policeCaughtTimes;
    }
}