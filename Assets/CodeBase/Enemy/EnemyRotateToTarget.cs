using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemy
{
    public class EnemyRotateToTarget : Follow
    {
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private float rotateSpeed;
        
        private Vector3 positionToLook;
        
        private const float MinimalDistance = 0.5f;

        private void Update()
        {
            if (IsHeroTransformExist() && HeroNotReached())
            {
                RotateTowardHero();
                agent.destination = heroTransform.position;
            }
        }

        private bool HeroNotReached() => 
            Vector3.Distance(agent.transform.position, heroTransform.position) >= MinimalDistance;

        private void RotateTowardHero()
        {
            UpdatePositionToLookAt();
            transform.rotation = SmoothedRotation(transform.rotation, positionToLook);
        }

        private void UpdatePositionToLookAt() => 
            positionToLook = heroTransform.position - transform.position;

        private Quaternion SmoothedRotation(Quaternion rotation, Vector3 positionToLook) => 
            Quaternion.Lerp(rotation, TargetRotation(positionToLook), SpeedFactory());

        private Quaternion TargetRotation(Vector3 position) => 
            Quaternion.LookRotation(position);

        private float SpeedFactory() => 
            rotateSpeed * Time.deltaTime;
    }
}