using UnityEngine;

namespace CodeBase.Hero
{
    [RequireComponent(typeof(HeroHealth))]
    public class HeroDeath : MonoBehaviour
    {
        [SerializeField] private HeroHealth heroHealth;
        [SerializeField] private HeroMovement heroMove;
        //[SerializeField] private HeroAttack heroAttack;

        private bool isDead;

        private void Start()
        {
            heroHealth.OnHealthChanged += OnHealthChanged;
        }

        private void OnDestroy()
        {
            heroHealth.OnHealthChanged -= OnHealthChanged;
        }

        private void OnHealthChanged()
        {
            if (!isDead && heroHealth.CurrentHP <= 0) 
                Die();
        }

        private void Die()
        {
            isDead = true;
            heroMove.enabled = false;
            //heroAttack.enabled = false;
        }
    }
}