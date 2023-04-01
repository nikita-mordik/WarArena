using CodeBase.Pool;
using CodeBase.Projectile;
using UnityEngine;
using Zenject;

namespace CodeBase.Enemy
{
    public class Attack : MonoBehaviour
    {
        [SerializeField] private Follow follow;
        [SerializeField] private float delay = 3f;
        [SerializeField] private float visibleDistance;
        [SerializeField] private float fieldOfView;

        private const ObjectType EnemyProjectile = ObjectType.EnemyProjectile;

        private float nextShootRate;

        private IObjectPoolService objectPoolService;

        [Inject]
        private void Construct(IObjectPoolService objectPoolService)
        {
            this.objectPoolService = objectPoolService;
        }

        private void Update()
        {
            if (nextShootRate >= delay && IsHeroVisible())
            {
                Shoot();
                nextShootRate = 0f;
            }

            nextShootRate += Time.deltaTime;
        }

        private bool IsHeroVisible()
        {
            var direction = follow.HeroTransform.position - transform.position;
            var distanceToHero = direction.magnitude;
            var angle = Vector3.Angle(direction, transform.forward);

            return distanceToHero <= visibleDistance && angle <= fieldOfView;
        }

        private void Shoot()
        {
            GameObject projectileObject = objectPoolService.GetObjectFromPool(EnemyProjectile);
            projectileObject.transform.position = transform.position;
            var projectile = projectileObject.GetComponent<EnemyProjectile>();
            if (projectile.IsTargetExist()) return;
            projectile.SetTarget(follow.HeroTransform);
        }
    }
}