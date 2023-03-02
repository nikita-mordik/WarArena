using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroAttack : MonoBehaviour
    {
        
        //private IInputService inputService;
        

        private void Awake()
        {
            //inputService = AllServices.Container.Single<IInputService>();

        }

        private void Update()
        {
            if (SimpleInput.GetButtonUp("Fire"))
            {
                OnAttack();
            }
            if (SimpleInput.GetButtonUp("Ult"))
            {
                Debug.LogError("ULT");
            }
        }
        
        private void OnAttack()
        {
            Debug.LogError("here attack");
        }
    }
}