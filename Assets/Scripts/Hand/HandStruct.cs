using System;
using UnityEngine;

namespace Hand
{
    [Serializable]
    public struct HandStruct
    {
        public GameObject handGO;
        public Transform initialPosition;
        public Transform target;
        public GameObject cashGO;
    }
}
