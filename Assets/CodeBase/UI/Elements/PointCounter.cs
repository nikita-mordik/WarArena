using CodeBase.Infrastructure.Services;
using TMPro;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Elements
{
    public class PointCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI counterText;

        private int currentPoint;

        private IEventListenerService eventListenerService;

        [Inject]
        private void Construct(IEventListenerService eventListenerService)
        {
            this.eventListenerService = eventListenerService;
        }

        private void OnEnable()
        {
            eventListenerService.OnHitEnemy += UpdateCounter;
            eventListenerService.OnRestartGame += OnRestartGame;
        }

        private void OnDisable()
        {
            eventListenerService.OnHitEnemy -= UpdateCounter;
            eventListenerService.OnRestartGame -= OnRestartGame;
        }

        private void UpdateCounter(int pointToAdd)
        {
            currentPoint += pointToAdd;
            counterText.text = $"{currentPoint}";
        }

        private void OnRestartGame()
        {
            currentPoint = 0;
            UpdateCounter(currentPoint);
        }
    }
}