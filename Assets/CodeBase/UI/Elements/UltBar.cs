using System;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements
{
    public class UltBar : MonoBehaviour
    {
        [SerializeField] private Image ultBar;
        [SerializeField] private Button ultButton;
        
        public void SetValue(float currentEnergy, float maxEnergy)
        {
            ultBar.fillAmount = currentEnergy / maxEnergy;
            ultButton.interactable = Math.Abs(ultBar.fillAmount - 1) < 0.01f;
        }
    }
}