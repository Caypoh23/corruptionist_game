using System;
using UnityEngine;

namespace Level
{
    [Serializable]
    public struct LevelItemStruct
    {
        public string name;
        public int startLevel;
        public int endLevel;
        public GameObject itemGO;
        //public Tag tag;
        //public ItemStruct[] items;

    }
}
