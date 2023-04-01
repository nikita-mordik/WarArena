using CodeBase.Extensions;
using CodeBase.Infrastructure.Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UI
{
    public class GameCanvas : MonoBehaviour
    {
        [SerializeField] private CanvasGroup pauseCanvas;
        [SerializeField] private CanvasGroup gameOverCanvas;
        [SerializeField] private Button pauseButton;

        private IEventListenerService eventListenerService;
        
        [Inject]
        private void Construct(IEventListenerService eventListenerService)
        {
            this.eventListenerService = eventListenerService;
        }

        private void OnEnable()
        {
            pauseButton.onClick.AddListener(OnPause);
            eventListenerService.OnGameEnd += OnGameEnd;
        }

        private void OnDisable()
        {
            eventListenerService.OnGameEnd -= OnGameEnd;
        }

        private void OnPause()
        {
            pauseCanvas.State(true);
            PauseTimeScale();
        }

        private void OnGameEnd()
        {
            gameOverCanvas.State(true);
            PauseTimeScale();
        }

        private static void PauseTimeScale() => 
            Time.timeScale = 0f;
    }
}