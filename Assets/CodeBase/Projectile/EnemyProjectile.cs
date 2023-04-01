using System.Collections;
using CodeBase.Hero;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Projectile
{
    public class EnemyProjectile : BaseProjectile
    {
        private const float EnergyDamage = -25f;

        private Transform target;
        private Tween tween;

        private void OnEnable()
        {
            MoveProjectile();
        }

        private void OnDisable()
        {
            Reset();
        }

        public override void OnTriggerEnter(Collider other)
        {
            if (isTriggered) return;
            
            base.OnTriggerEnter(other);

            if (other.TryGetComponent<HeroEnergy>(out var heroEnergy))
            {
                heroEnergy.ChangeEnergy(EnergyDamage);
                objectPoolService.BackObjectToPool(gameObject);
                isTriggered = true;
            }
        }

        public override void MoveProjectile(Vector3 direction = default)
        {
            StartCoroutine(MoveRoutine());
        }

        public void SetTarget(Transform follow) => target = follow;

        public bool IsTargetExist() => target != null;

        public override void Reset()
        {
            base.Reset();
            tween.Kill();
            StopAllCoroutines();
        }

        private IEnumerator MoveRoutine()
        {
            yield return new WaitUntil(() => target != null);
            while (!isTriggered)
            {
                tween.Kill();
                tween = transform.DOMove(target.position, speed).SetSpeedBased();
                yield return null;
            }
        }
    }
}