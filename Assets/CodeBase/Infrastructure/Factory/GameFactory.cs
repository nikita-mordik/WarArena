using CodeBase.Enemy;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Pool;
using CodeBase.Projectile;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider assetProvider;

        private GameObject hero;

        public GameFactory(IAssetProvider assetProvider)
        {
            this.assetProvider = assetProvider;
        }

        public GameObject CreateHero(Vector3 at)
        {
            hero = assetProvider.Instantiate(AssetsPath.Hero, at);
            return hero;
        }

        public GameObject CreateHud() => 
            assetProvider.Instantiate(AssetsPath.Hud);

        public GameObject CreateEnemy(ObjectType type, Transform parent)
        {
            GameObject enemy = null;
            switch (type)
            {
                case ObjectType.RedEnemy:
                    enemy = assetProvider.Instantiate(AssetsPath.RedEnemy);
                    break;
                case ObjectType.BlueEnemy:
                    enemy = assetProvider.Instantiate(AssetsPath.BlueEnemy);
                    break;
            }
            
            enemy.transform.SetParent(parent);
            enemy.GetComponent<Follow>().Construct(hero.transform);
            return enemy;
        }

        public GameObject CreateProjectile(ObjectType type, Transform parent)
        {
            GameObject projectile = null;
            switch (type)
            {
                case ObjectType.HeroProjectile:
                    projectile = assetProvider.Instantiate(AssetsPath.HeroProjectile);
                    break;
                case ObjectType.EnemyProjectile:
                    projectile = assetProvider.Instantiate(AssetsPath.EnemyProjectile);
                    break;
            }

            projectile.transform.SetParent(parent);
            projectile.GetComponent<BaseProjectile>().Construct(hero);
            return projectile;
        }
    }
}