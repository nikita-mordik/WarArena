using System.Collections.Generic;
using CodeBase.Infrastructure.Factory;
using UnityEngine;
using Zenject;

namespace CodeBase.Pool
{
    public class ObjectPoolService : MonoBehaviour, IObjectPoolService
    {
        [SerializeField] private List<ObjectInfo> objectInfos;

        private Dictionary<ObjectType, Pool> pools;

        private IGameFactory gameFactory;
        
        [Inject]
        private void Construct(IGameFactory gameFactory)
        {
            this.gameFactory = gameFactory;
        }

        public bool IsInitialize { get; private set; }

        public GameObject GetObjectFromPool(ObjectType type)
        {
            GameObject obj = pools[type].Objects.Count > 0
                ? pools[type].Objects.Dequeue()
                : InstantiateObject(type, pools[type].Container);
            obj.SetActive(true);
            return obj;
        }

        public void BackObjectToPool(GameObject gameObject)
        {
            ObjectType objectType = gameObject.GetComponent<IPoolObject>().Type;
            pools[objectType].Objects.Enqueue(gameObject);
            gameObject.transform.position = pools[objectType].Container.position;
            gameObject.transform.SetParent(pools[objectType].Container);
            gameObject.SetActive(false);
        }

        public void InitializePool()
        {
            IsInitialize = false;
            pools = new Dictionary<ObjectType, Pool>();
            
            var emptyGO = new GameObject();
            emptyGO.transform.SetParent(transform);

            foreach (var objectInfo in objectInfos)
            {
                var container = Instantiate(emptyGO, transform);
                container.name = objectInfo.ObjectType.ToString();

                pools[objectInfo.ObjectType] = new Pool(container.transform);

                for (int i = 0; i < objectInfo.ObjectCount; i++)
                {
                    var go = InstantiateObject(objectInfo.ObjectType, container.transform);
                    pools[objectInfo.ObjectType].Objects.Enqueue(go);
                }
            }
            
            Destroy(emptyGO);
            IsInitialize = true;
        }

        private GameObject InstantiateObject(ObjectType objectType, Transform parent)
        {
            GameObject go = null;
            switch (objectType)
            {
                case ObjectType.RedEnemy:
                case ObjectType.BlueEnemy:
                    go = gameFactory.CreateEnemy(objectType, parent);
                    break;
                case ObjectType.HeroProjectile:
                case ObjectType.EnemyProjectile:
                    go = gameFactory.CreateProjectile(objectType, parent);
                    break;
            }

            return go;
        }
    }
}