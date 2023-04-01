using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Pool;
using CodeBase.Projectile;
using UnityEngine;
using Zenject;

namespace CodeBase.Hero
{
    public class HeroAttack : MonoBehaviour
    {
        [SerializeField] private HeroEnergy heroEnergy;
        [SerializeField] private HeroHealth heroHealth;
        [SerializeField] private Transform cameraTransform;
        
        private const float MinimalHpToRicochet = 20f;

        private IObjectPoolService poolService;
        private IInputService inputService;
        private IEventListenerService eventListenerService;

        [Inject]
        private void Construct(IInputService inputService, IEventListenerService eventListenerService, 
            IObjectPoolService objectPoolService)
        {
            this.inputService = inputService;
            this.eventListenerService = eventListenerService;
            poolService = objectPoolService;
        }

        private void Update()
        {
            if (inputService.IsAttackButtonUp())
                Shoot();

            if (inputService.IsUltButtonUp() && heroEnergy.IsEnergyFull)
                UseUlt();
        }

        private void Shoot()
        {
            GameObject projectileObject = poolService.GetObjectFromPool(ObjectType.HeroProjectile);
            projectileObject.transform.position = cameraTransform.position;
            var direction = cameraTransform.forward;
            var projectile = projectileObject.GetComponent<HeroProjectile>();
            
            if (heroHealth.CurrentHP <= MinimalHpToRicochet)
            {
                projectile.ChanceToRicochet = 100;
            }
            else
            {
                projectile.ChanceToRicochet += 10;
            }
           
            projectile.MoveProjectile(direction);
        }

        private void UseUlt()
        {
            eventListenerService?.InvokeOnUlt();
            heroEnergy.RestEnergy();
        }
    }
}