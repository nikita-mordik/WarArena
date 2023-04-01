using UnityEngine;

namespace CodeBase.Pool
{
    public interface IObjectPoolService
    {
        bool IsInitialize { get; }
        GameObject GetObjectFromPool(ObjectType type);
        void BackObjectToPool(GameObject gameObject);
        void InitializePool();
    }
}