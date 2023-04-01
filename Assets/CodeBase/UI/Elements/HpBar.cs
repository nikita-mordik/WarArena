using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements
{
    public class HpBar : MonoBehaviour
    {
        [SerializeField] private Image healthBar;

        public void SetValue(float currentHP, float maxHP) => 
            healthBar.fillAmount = currentHP / maxHP;
    }
}