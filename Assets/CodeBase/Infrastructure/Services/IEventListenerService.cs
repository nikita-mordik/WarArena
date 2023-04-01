using System;

namespace CodeBase.Infrastructure.Services
{
    public interface IEventListenerService
    {
        event Action OnUlt;
        event Action<int> OnHitEnemy;
        event Action OnGameEnd;
        event Action OnRestartGame;
        
        void InvokeOnUlt();
        void InvokeOnHitEnemy(int point);
        void InvokeOnGameEnd();
        void InvokeOnRestartGame();
    }
}