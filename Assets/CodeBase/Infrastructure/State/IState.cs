﻿namespace CodeBase.Infrastructure.State
{
    public interface IState : IExitableState
    {
        void Enter();
    }

    public interface IExitableState
    {
        void Exit();
    }
    
    public interface IPayloadedState<TPayload> : IExitableState
    {
        void Enter(TPayload payload);
    }
}