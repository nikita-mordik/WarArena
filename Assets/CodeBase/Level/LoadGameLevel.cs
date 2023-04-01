using CodeBase.Infrastructure.Services.SceneLoad;
using CodeBase.Infrastructure.State;
using CodeBase.UI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.Level
{
    public class LoadGameLevel : MonoBehaviour
    {
        [SerializeField] private string nextScene;
        [SerializeField] private LoadingCurtain loadingCurtain;
        [SerializeField] private Button startButton;

        private LoadingCurtain curtain;
        
        private ISceneLoaderService sceneLoaderService;
        private IGameStateMachine stateMachine;

        [Inject]
        private void Construct(ISceneLoaderService sceneLoaderService, IGameStateMachine stateMachine)
        {
            this.sceneLoaderService = sceneLoaderService;
            this.stateMachine = stateMachine;
        }

        private void Start()
        {
            // curtain = Instantiate(loadingCurtain);
            // curtain.Hide();

            startButton.onClick.AddListener(StartGame);
        }

        private void StartGame()
        {
            //var levelState = new LoadLevelState(stateMachine, sceneLoaderService, curtain);

            stateMachine.Enter<LoadLevelState, string>(nextScene);
        }
    }
}
