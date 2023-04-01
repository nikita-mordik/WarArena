using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Pool;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.ZenjectInstallers
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private ObjectPoolService objectPoolService;
    
        private AssetProvider assetProvider;

        public override void InstallBindings()
        {
            BindAssetProviderService();
            BindGameFactory();
            BindObjectPoolService();
        }
    
        private void BindAssetProviderService()
        {
            assetProvider = new AssetProvider(Container);
            Container.Bind<IAssetProvider>()
                .To<AssetProvider>()
                .FromInstance(assetProvider)
                .AsSingle()
                .NonLazy();
        }

        private void BindGameFactory()
        {
            var gameFactory = new GameFactory(assetProvider);
            Container.Bind<IGameFactory>()
                .To<GameFactory>()
                .FromInstance(gameFactory)
                .AsSingle()
                .NonLazy();
        }

        private void BindObjectPoolService()
        {
            Container.Bind<IObjectPoolService>()
                .To<ObjectPoolService>()
                .FromComponentInNewPrefab(objectPoolService)
                .AsSingle()
                .NonLazy();
        }
    }
}