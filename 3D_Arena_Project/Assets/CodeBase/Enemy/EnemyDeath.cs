using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(EnemyHealth))]
    public class EnemyDeath : MonoBehaviour
    {
        [SerializeField] private EnemyHealth enemyHealth;

        public event Action OnEnemyDeath;
        
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
            enemyHealth.OnHealthChanged -= OnOnHealthChanged;

            StartCoroutine(DestroyEnemyRoutine());
            
            OnEnemyDeath?.Invoke();
        }


        private IEnumerator DestroyEnemyRoutine()
        {
            yield return new WaitForSeconds(3f);
            Destroy(gameObject);
        }
    }
}