using System;
using CodeBase.Interface;
using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private float maxHP;

        private float currentHP;

        public float MaxHP => maxHP;
        public float CurrentHP
        {
            get => currentHP;
            set
            {
                if (currentHP != value)
                {
                    currentHP = value;
                    OnHealthChanged?.Invoke();
                }
            }
        }

        public event Action OnHealthChanged;

        private void Start()
        {
            currentHP = maxHP;
        }

        public void TakeDamage(float damage)
        {
            if (currentHP <= 0) return;

            CurrentHP -= damage;
        }

        public void AddHP(float hpToAdd)
        {
            if (currentHP <= 0) return;

            CurrentHP += hpToAdd;
        }
    }
}