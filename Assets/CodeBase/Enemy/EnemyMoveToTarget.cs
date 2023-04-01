using System.Collections;
using CodeBase.Interface;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class EnemyMoveToTarget : Follow
    {
        [SerializeField] private EnemyHealth enemyHealth;
        [SerializeField] private Rigidbody enemyBody;
        [SerializeField] private Collider enemyCollider;
        [SerializeField] private float upDuration = 2f;
        [SerializeField] private float toTargetSpeed = 7f;
        [SerializeField] private float delay = 2f;
        [SerializeField] private float height = 5f;

        private const float Damage = 15f;
        private const float SelfDamage = 100f;

        private bool isCollide;
        private Tween tween;
        private Coroutine moveCoroutine;

        private void OnEnable()
        {
            MoveToTarget();
        }

        private void OnDisable()
        {
            Reset();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (isCollide) return;

            if (collision.transform.TryGetComponent<IHealth>(out var heroHealth))
            {
                heroHealth.TakeDamage(Damage);
                enemyHealth.TakeDamage(SelfDamage);
                isCollide = true;
            }
        }

        private void MoveToTarget() => 
            StartCoroutine(MoveRoutine());

        private IEnumerator MoveRoutine()
        {
            transform.LookAt(heroTransform);
            yield return new WaitForSeconds(delay);
            EnemyComponentsState(true);
            var upMove = transform.position + transform.up * height;
            tween = enemyBody.DOMove(upMove, upDuration)
                .SetSpeedBased()
                .SetEase(Ease.Linear);
            yield return new WaitForSeconds(delay);
            while (!isCollide)
            {
                transform.LookAt(heroTransform);
                tween.Kill();
                tween = enemyBody.DOMove(heroTransform.position, toTargetSpeed).SetSpeedBased();
                yield return null;
            }
        }

        private void Reset()
        {
            tween.Kill();
            isCollide = false;
            EnemyComponentsState(false);
            StopAllCoroutines();
        }

        private void EnemyComponentsState(bool state)
        {
            enemyBody.isKinematic = state;
            enemyCollider.isTrigger = state;
        }
    }
}