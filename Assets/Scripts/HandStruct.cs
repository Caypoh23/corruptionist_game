using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public struct HandStruct
{
    public GameObject hand;
    public Transform initialPoisition;
    public Transform target;
}
