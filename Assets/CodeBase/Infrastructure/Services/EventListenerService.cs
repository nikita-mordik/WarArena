using System;

namespace CodeBase.Infrastructure.Services
{
    public class EventListenerService : IEventListenerService
    {
        private Action onUlt;
        private Action<int> onHitEnemy;
        private Action onGameEnd;
        private Action onRestartGame;

        public event Action OnUlt
        {
            add => onUlt += value;
            remove => onUlt -= value;
        }
        public event Action<int> OnHitEnemy
        {
            add => onHitEnemy += value;
            remove => onHitEnemy -= value;
        }
        public event Action OnGameEnd
        {
            add => onGameEnd += value;
            remove => onGameEnd -= value;
        }
        public event Action OnRestartGame
        {
            add => onRestartGame += value;
            remove => onRestartGame -= value;
        }

        public void InvokeOnUlt()
        {
            onUlt?.Invoke();
        }

        public void InvokeOnHitEnemy(int point)
        {
            onHitEnemy?.Invoke(point);
        }

        public void InvokeOnGameEnd()
        {
            onGameEnd?.Invoke();
        }

        public void InvokeOnRestartGame()
        {
            onRestartGame?.Invoke();
        }
    }
}