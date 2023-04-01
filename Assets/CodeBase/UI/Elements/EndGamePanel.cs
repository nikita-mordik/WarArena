using CodeBase.Extensions;
using CodeBase.Infrastructure.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UI.Elements
{
    public class EndGamePanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI resultText;
        [SerializeField] private Button restartButton;
        [SerializeField] private CanvasGroup endGamePanel;
        
        private int totalScorePoint;
        
        private IEventListenerService eventListenerService;

        [Inject]
        private void Construct(IEventListenerService eventListenerService)
        {
            this.eventListenerService = eventListenerService;
        }

        private void OnEnable()
        {
            eventListenerService.OnHitEnemy += Result;
            restartButton.onClick.AddListener(RestartGame);
        }

        private void OnDisable()
        {
            eventListenerService.OnHitEnemy -= Result;
        }

        private void Result(int value)
        {
            totalScorePoint += value;
            resultText.text = totalScorePoint.ToString();
        }

        private void RestartGame()
        {
            totalScorePoint = 0;
            GameTimeScale();
            eventListenerService?.InvokeOnRestartGame();
            eventListenerService?.InvokeOnUlt();
            endGamePanel.State(false);
        }

        private static void GameTimeScale() => 
            Time.timeScale = 1f;
    }
}