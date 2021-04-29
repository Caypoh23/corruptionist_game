using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public struct HandStruct
{
    public GameObject handGO;
    public Transform initialPosition;
    public Transform target;
    public GameObject cashGO;
}
