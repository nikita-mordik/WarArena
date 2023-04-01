using CodeBase.Infrastructure.State;
using Zenject;

namespace CodeBase.Infrastructure.ZenjectInstallers
{
    public class StateMachineInstaller : Installer<StateMachineInstaller>
    {
        public override void InstallBindings()
        {
            BindGameStateMachine();
            BindStateFactory();
        }

        private void BindGameStateMachine()
        {
            Container.Bind<IGameStateMachine>()
                .To<GameStateMachine>()
                .AsSingle()
                .NonLazy();
        }

        private void BindStateFactory()
        {
            Container.Bind<IStateFactory>()
                .To<StateFactory>()
                .AsSingle()
                .NonLazy();
        }
    }
}