using System;
using CodeBase.Interface;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private float maxHP;
        [SerializeField] private float currentHP;

        public event Action OnHealthChanged;

        public float MaxHP
        {
            get => maxHP;
        }
        public float CurrentHP
        {
            get => currentHP;
            set => currentHP = value;
        }

        private void OnEnable()
        {
            currentHP = maxHP;
        }

        public void TakeDamage(float damage)
        {
            currentHP -= damage;
            OnHealthChanged?.Invoke();
        }
    }
}