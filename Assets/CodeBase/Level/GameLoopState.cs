using CodeBase.Infrastructure.State;
using CodeBase.Pool;
using UnityEngine;

namespace CodeBase.Level
{
    public class GameLoopState : IState
    {
        private readonly IGameStateMachine stateMachine;
        private readonly IObjectPoolService poolService;

        public GameLoopState(IGameStateMachine stateMachine, IObjectPoolService poolService)
        {
            this.stateMachine = stateMachine;
            this.poolService = poolService;
        }
        
        public void Enter()
        {
            Debug.LogError("game loop state");
            Debug.LogError(stateMachine!=null);
        }

        public void Exit()
        {
            
        }
    }
}