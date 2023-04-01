using System;
using CodeBase.Pool;
using UnityEngine;
using Zenject;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(EnemyHealth))]
    public class EnemyDeath : MonoBehaviour
    {
        [SerializeField] private EnemyHealth enemyHealth;

        public event Action OnEnemyDeath;

        private IObjectPoolService objectPoolService;
        
        [Inject]
        private void Construct(IObjectPoolService objectPoolService)
        {
            this.objectPoolService = objectPoolService;
        }
        
        private void OnEnable()
        {
            enemyHealth.OnHealthChanged += OnOnHealthChanged;
        }

        private void OnDisable()
        {
            enemyHealth.OnHealthChanged -= OnOnHealthChanged;
        }

        private void OnOnHealthChanged()
        {
            if (enemyHealth.CurrentHP <= 0)
                Die();
        }

        private void Die()
        {
            objectPoolService.BackObjectToPool(gameObject);
            OnEnemyDeath?.Invoke();
        }
    }
}