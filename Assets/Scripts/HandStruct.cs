using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class HandStruct
{
    public GameObject handGO;
    public Transform initialPosition;
    public Transform target;

    public GameObject bribeGO;
}
