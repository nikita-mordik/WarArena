using System;
using UnityEngine;

namespace CodeBase.Pool
{
    [Serializable]
    public struct ObjectInfo
    {
        public ObjectType ObjectType;
        public GameObject ObjectPrefabs;
        public int ObjectCount;
    }
}