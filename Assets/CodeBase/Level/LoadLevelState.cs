using CodeBase.Infrastructure.Services.SceneLoad;
using CodeBase.Infrastructure.State;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Level
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly IGameStateMachine stateMachine;
        private readonly ISceneLoaderService sceneLoaderService;
        private readonly LoadingCurtain loadingCurtain;

        public LoadLevelState(IGameStateMachine stateMachine, ISceneLoaderService sceneLoaderService)
        {
            this.stateMachine = stateMachine;
            this.sceneLoaderService = sceneLoaderService;
        }
        
        public void Enter(string nextScene)
        {
            //loadingCurtain.Show();
            Debug.LogError("load level state");
            sceneLoaderService.LoadScene(nextScene, OnLoad);
        }

        public void Exit()
        {
            //loadingCurtain.Hide();
        }
        
        private void OnLoad()
        {
            stateMachine.Enter<GameLoopState>();
        }
    }
}