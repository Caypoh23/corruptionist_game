using Hand;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct InteractiveTutorialStruct 
{
    public string name;
    public string[] instructions;
    public HandStruct[] hands;
    public float handMovementTime;
}
