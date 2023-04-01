using CodeBase.Extensions;
using CodeBase.Infrastructure.Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UI.Elements
{
    public class PausePanel : MonoBehaviour
    {
        [SerializeField] private CanvasGroup pauseCanvas;
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button restartButton;

        private IEventListenerService eventListenerService;

        [Inject]
        private void Construct(IEventListenerService eventListenerService)
        {
            this.eventListenerService = eventListenerService;
        }

        private void Start()
        {
            resumeButton.onClick.AddListener(ResumeGame);
            restartButton.onClick.AddListener(RestartGame);
        }

        private void ResumeGame()
        {
            pauseCanvas.State(false);
            GameTimeScale();
        }

        private void RestartGame()
        {
            GameTimeScale();
            eventListenerService?.InvokeOnRestartGame();
            eventListenerService?.InvokeOnUlt();
            pauseCanvas.State(false);
        }

        private static void GameTimeScale() => 
            Time.timeScale = 1f;
    }
}