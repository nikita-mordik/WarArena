using CodeBase.Infrastructure.Services;
using CodeBase.Pool;
using UnityEngine;
using Zenject;

namespace CodeBase.Projectile
{
    public abstract class BaseProjectile : MonoBehaviour, IPoolObject
    {
        [SerializeField] private ObjectType objectType;
        [SerializeField] protected float speed;

        protected GameObject heroObject;
        private int obstacleLayer;
        protected bool isTriggered;

        public ObjectType Type => objectType;

        protected IObjectPoolService objectPoolService;
        private IEventListenerService eventListenerService;

        [Inject]
        private void Construct(IObjectPoolService objectPoolService, IEventListenerService eventListenerService)
        {
            this.objectPoolService = objectPoolService;
            this.eventListenerService = eventListenerService;
        }

        public void Construct(GameObject hero)
        {
            heroObject = hero;
        }

        private void Start()
        {
            obstacleLayer = LayerMask.NameToLayer("Obstacle");
            eventListenerService.OnRestartGame += OnRestart;
        }

        private void OnDestroy()
        {
            eventListenerService.OnRestartGame -= OnRestart;
        }

        public virtual void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == obstacleLayer)
            {
                objectPoolService.BackObjectToPool(gameObject);
                isTriggered = true;
            }
        }

        public abstract void MoveProjectile(Vector3 direction = default);

        public virtual void Reset() => isTriggered = false;

        private void OnRestart() => objectPoolService.BackObjectToPool(gameObject);
    }
}