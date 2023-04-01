using System;
using System.Collections.Generic;

namespace CodeBase.Infrastructure.State
{
    public class GameStateMachine : IGameStateMachine
    {
        //private readonly Dictionary<Type, IExitableState> states;
        private readonly IStateFactory stateFactory;
        
        private IExitableState activeState;

        public GameStateMachine(IStateFactory stateFactory)
        {
            this.stateFactory = stateFactory;
        }
        
        public void Enter<TState>() where TState : class, IState
        {
            // var currentState = ChangeState<TState>();
            // currentState.Enter();
            
            activeState = ChangeState<TState>();
            ((TState) activeState).Enter();
        }
        
        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            // var currentState = ChangeState<TState>();
            // currentState.Enter(payload);

            activeState = ChangeState<TState>();
            ((TState) activeState).Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            // activeState?.Exit();
            // TState currentState = GetState<TState>();
            // activeState = currentState;
            // return currentState;
            
            activeState?.Exit();
            var currentState = stateFactory.Create<TState>();
            return currentState;
        }

        // private TState GetState<TState>() where TState : class, IExitableState => 
        //     states[typeof(TState)] as TState;
    }
}