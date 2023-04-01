using CodeBase.Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace CodeBase.Hero
{
    [RequireComponent(typeof(HeroHealth))]
    public class HeroDeath : MonoBehaviour
    {
        [SerializeField] private HeroHealth heroHealth;
        [SerializeField] private HeroEnergy heroEnergy;
        [SerializeField] private HeroMovement heroMove;
        [SerializeField] private HeroAttack heroAttack;

        private bool isDead;
        
        private IEventListenerService eventListenerService;
        
        [Inject]
        private void Construct(IEventListenerService eventListenerService)
        {
            this.eventListenerService = eventListenerService;
        }

        private void Start()
        {
            heroHealth.OnHealthChanged += OnHealthChanged;
            eventListenerService.OnRestartGame += OnRestart;
        }

        private void OnDestroy()
        {
            heroHealth.OnHealthChanged -= OnHealthChanged;
            eventListenerService.OnRestartGame -= OnRestart;
        }

        private void OnHealthChanged()
        {
            if (!isDead && heroHealth.CurrentHP <= 0) 
                Die();
        }

        private void OnRestart()
        {
            isDead = false;
            HeroComponentsState(true);
            heroHealth.CurrentHP = heroHealth.MaxHP;
            heroEnergy.CurrentEnergy = heroEnergy.MaxEnergy / 2;
        }

        private void Die()
        {
            eventListenerService.InvokeOnGameEnd();
            isDead = true;
            HeroComponentsState(false);
        }

        private void HeroComponentsState(bool state)
        {
            heroMove.enabled = state;
            heroAttack.enabled = state;
        }
    }
}