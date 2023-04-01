namespace CodeBase.Infrastructure.State
{
    public interface IStateFactory
    {
        TState Create<TState>() where TState : IExitableState;
    }
}