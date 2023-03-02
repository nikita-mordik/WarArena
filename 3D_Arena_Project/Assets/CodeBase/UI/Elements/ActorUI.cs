using CodeBase.Interface;
using UnityEngine;

namespace CodeBase.UI.Elements
{
    public class ActorUI : MonoBehaviour
    {
        [SerializeField] private HpBar hpBar;

        private IHealth health;

        public void Construct(IHealth health)
        {
            this.health = health;
            this.health.OnHealthChanged += UpdateHpBar;
        }

        private void Start()
        {
            IHealth health = GetComponent<IHealth>();

            if (health != null)
                Construct(health);
        }

        private void OnDestroy()
        {
            health.OnHealthChanged -= UpdateHpBar;
        }

        private void UpdateHpBar()
        {
            hpBar.SetValue(health.CurrentHP, health.MaxHP);
        }
    }
}