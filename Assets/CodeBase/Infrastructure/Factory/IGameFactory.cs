using CodeBase.Pool;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface IGameFactory
    {
        GameObject CreateHero(Vector3 at);
        GameObject CreateHud();
        GameObject CreateEnemy(ObjectType type, Transform parent);
        GameObject CreateProjectile(ObjectType type, Transform parent);
    }
}