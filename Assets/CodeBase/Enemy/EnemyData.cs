using CodeBase.Infrastructure.Services;
using CodeBase.Pool;
using UnityEngine;
using Zenject;

namespace CodeBase.Enemy
{
    public class EnemyData : MonoBehaviour, IPoolObject
    {
        [SerializeField] private ObjectType objectType;
        
        public ObjectType Type => objectType;

        private IEventListenerService eventListenerService;
        private IObjectPoolService objectPoolService;

        [Inject]
        private void Construct(IEventListenerService eventListenerService, IObjectPoolService objectPoolService)
        {
            this.eventListenerService = eventListenerService;
            this.objectPoolService = objectPoolService;
        }

        private void OnEnable()
        {
            eventListenerService.OnUlt += OnUlt;
        }

        private void OnDisable()
        {
            eventListenerService.OnUlt -= OnUlt;
        }

        private void OnUlt() => 
            objectPoolService.BackObjectToPool(gameObject);
    }
}