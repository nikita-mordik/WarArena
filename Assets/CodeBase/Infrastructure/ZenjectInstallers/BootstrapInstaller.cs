using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.SceneLoad;
using CodeBase.Infrastructure.State;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.ZenjectInstallers
{
    public class BootstrapInstaller : MonoInstaller, ICoroutineRunner
    {
        public override void InstallBindings()
        {
            BindStateMachine();
            BindSceneLoaderService();
            BindInputService();
            BindEventListenerService();
        }

        private void BindStateMachine()
        {
            Container.Bind<IGameStateMachine>()
                .FromSubContainerResolve()
                .ByInstaller<StateMachineInstaller>()
                .AsSingle();
        }

        private void BindSceneLoaderService()
        {
            Container.Bind<ISceneLoaderService>()
                .To<SceneLoaderService>()
                .FromInstance(new SceneLoaderService(this))
                .AsSingle()
                .NonLazy();
        }

        private void BindInputService()
        {
            if (Application.isEditor)
            {
                Container.Bind<IInputService>()
                    .To<StandaloneInputService>()
                    .FromInstance(new StandaloneInputService())
                    .AsSingle()
                    .NonLazy();
            }
            else
            {
                Container.Bind<IInputService>()
                    .To<MobileInputService>()
                    .FromInstance(new MobileInputService())
                    .AsSingle()
                    .NonLazy();
            }
        }

        private void BindEventListenerService()
        {
            Container.Bind<IEventListenerService>()
                .To<EventListenerService>()
                .FromInstance(new EventListenerService())
                .AsSingle()
                .NonLazy();
        }
    }
}