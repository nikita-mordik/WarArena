using TMPro;
using UnityEngine;

namespace CodeBase.UI.Elements
{
    public class PointCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI counterText;

        private int currentPoint;

        private void UpdateCounter(int pointToAdd)
        {
            currentPoint += pointToAdd;
            counterText.text = $"{currentPoint}";
        }
    }
}