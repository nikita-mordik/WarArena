using CodeBase.Interface;
using UnityEngine;

namespace CodeBase.UI.Elements
{
    public class ActorUI : MonoBehaviour
    {
        [SerializeField] private HpBar hpBar;
        [SerializeField] private UltBar ultBar;

        private IHealth health;
        private IEnergy energy;
        
        public void Construct(IHealth health, IEnergy energy)
        {
            this.health = health;
            this.health.OnHealthChanged += UpdateHpBar;
            this.energy = energy;
            this.energy.OnEnergyChanged += UpdateUltBar;
        }

        private void OnDestroy()
        {
            health.OnHealthChanged -= UpdateHpBar;
            energy.OnEnergyChanged -= UpdateUltBar;
        }

        private void UpdateHpBar() => 
            hpBar.SetValue(health.CurrentHP, health.MaxHP);

        private void UpdateUltBar() => 
            ultBar.SetValue(energy.CurrentEnergy, energy.MaxEnergy);
    }
}