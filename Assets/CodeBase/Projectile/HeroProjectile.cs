using System.Collections;
using CodeBase.Enemy;
using CodeBase.Infrastructure.Services;
using CodeBase.Interface;
using CodeBase.Pool;
using UnityEngine;
using Zenject;

namespace CodeBase.Projectile
{
    public class HeroProjectile : BaseProjectile
    {
        [SerializeField] private float delay;
        [SerializeField] private Rigidbody projectileBody;
        
        private const float Damage = 100f;
        private const int AngleToReflect = 60;

        private int chanceToRicochet = 10;
        private bool isRicocheted;
        private Vector3 moveDirection;

        public int ChanceToRicochet
        {
            get => chanceToRicochet;
            set => chanceToRicochet = value;
        }

        private IEventListenerService eventListenerService;
        
        [Inject]
        private void Construct(IEventListenerService eventListenerService)
        {
            this.eventListenerService = eventListenerService;
        }
        
        private void OnEnable()
        {
            StartCoroutine(ProjectileLiveRoutine());
        }

        private void OnDisable()
        {
            Reset();
        }

        public override void OnTriggerEnter(Collider other)
        {
            if (isTriggered) return;
            
            base.OnTriggerEnter(other);
            
            if (other.TryGetComponent<EnemyHealth>(out var enemyHealth))
            {
                enemyHealth.TakeDamage(Damage);
                var enemyType = enemyHealth.GetComponent<EnemyData>().Type;
                var heroEnergy = heroObject.GetComponent<IEnergy>();
                heroEnergy.ChangeEnergy(Energy(enemyType));
                eventListenerService.InvokeOnHitEnemy(1);

                if (Random.Range(0, 90) <= chanceToRicochet && !isRicocheted)
                {
                    RicochetProjectile(other);
                    isRicocheted = true;
                    return;
                }

                if (isRicocheted)
                {
                    if (Random.value > 0.5f)
                    {
                        heroEnergy.ChangeEnergy(10f);
                    }
                    else
                    {
                        heroObject.GetComponent<IHealth>().AddHP(50f);
                    }
                }
                
                objectPoolService.BackObjectToPool(gameObject);
                isTriggered = true;
            }
        }

        public override void MoveProjectile(Vector3 direction)
        {
            StartCoroutine(MoveRoutine(direction));
        }

        public override void Reset()
        {
            base.Reset();
            
            if (chanceToRicochet >= 90 || isRicocheted)
            {
                chanceToRicochet = 10;
            }
            
            isRicocheted = false;
            projectileBody.velocity = Vector3.zero;
            StopAllCoroutines();
        }

        private void RicochetProjectile(Collider other)
        {
            var toEnemy = other.transform.position - transform.position;
            var randomDirection = Random.insideUnitSphere;
            var angle = Vector3.Angle(toEnemy, randomDirection);
            if (angle < AngleToReflect)
            {
                randomDirection.Normalize();
                var reflectedDirection = Vector3.Reflect(toEnemy.normalized, randomDirection);
                moveDirection = reflectedDirection;
            }
        }

        private IEnumerator MoveRoutine(Vector3 direction)
        {
            moveDirection = direction;
            while (!isTriggered)
            {
                projectileBody.AddForce(moveDirection * speed * Time.deltaTime, ForceMode.Impulse);
                yield return null;
            }
        }

        private float Energy(ObjectType enemyType)
        {
            var energy = 0f;
            switch (enemyType)
            {
                case ObjectType.RedEnemy:
                    energy = 15f;
                    break;
                case ObjectType.BlueEnemy:
                    energy = 50f;
                    break;
            }

            return energy;
        }

        private IEnumerator ProjectileLiveRoutine()
        {
            yield return new WaitForSeconds(delay);
            objectPoolService.BackObjectToPool(gameObject);
        }
    }
}