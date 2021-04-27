using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public struct HandStruct
{
    public GameObject handGO;
    public Transform initialPoisition;
    public Transform target;
}
