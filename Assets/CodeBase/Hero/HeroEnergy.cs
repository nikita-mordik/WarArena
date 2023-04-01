using System;
using CodeBase.Interface;
using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroEnergy : MonoBehaviour, IEnergy
    {
        [SerializeField] private float maxEnergy;

        private float currentEnergy;

        public float MaxEnergy => maxEnergy;
        public float CurrentEnergy
        {
            get => currentEnergy;
            set
            {
                if (currentEnergy != value)
                {
                    currentEnergy = value;
                    OnEnergyChanged?.Invoke();
                }
            }
        }
        public bool IsEnergyFull => currentEnergy >= maxEnergy;

        public event Action OnEnergyChanged;

        private void Start()
        {
            currentEnergy = maxEnergy / 2f;
        }

        public void ChangeEnergy(float value)
        {
            CurrentEnergy += value;
        }

        public void RestEnergy()
        {
            CurrentEnergy = 0;
        }
    }
}