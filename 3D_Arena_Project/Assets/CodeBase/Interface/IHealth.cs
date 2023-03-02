using System;

namespace CodeBase.Interface
{
    public interface IHealth
    {
        float MaxHP { get; }
        float CurrentHP { get; set; }
        event Action OnHealthChanged;
        void TakeDamage(float damage);
    }
}