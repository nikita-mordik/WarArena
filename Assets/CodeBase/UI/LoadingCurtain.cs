using System.Collections;
using CodeBase.Extensions;
using UnityEngine;

namespace CodeBase.UI
{
    public class LoadingCurtain : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void Show() => canvasGroup.State(true);

        public void Hide() => StartCoroutine(HideRoutine());

        private IEnumerator HideRoutine()
        {
            while (canvasGroup.alpha > 0f)
            {
                canvasGroup.alpha -= 0.05f;
                yield return null;
            }

            canvasGroup.State(false);
        }
    }
}