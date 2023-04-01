using System;

namespace CodeBase.Interface
{
    public interface IEnergy
    {
        float MaxEnergy { get; }
        float CurrentEnergy { get; set; }
        bool IsEnergyFull { get; }
        event Action OnEnergyChanged;
        void ChangeEnergy(float value);
        void RestEnergy();
    }
}