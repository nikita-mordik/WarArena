using System.Collections;
using CodeBase.Infrastructure.Services;
using CodeBase.Pool;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace CodeBase.Infrastructure.Spawn
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private float spawnDelay = 5f;
        [SerializeField] private float radius = 5f;

        private const float MinSpawnDelay = 2f;
        private const int ContOfRedEnemy = 4;

        private float spawnTime;
        private bool isCanSpawn;

        private IObjectPoolService poolService;
        private IEventListenerService eventListenerService;
        
        [Inject]
        private void Construct(IObjectPoolService objectPoolService, IEventListenerService eventListenerService)
        {
            poolService = objectPoolService;
            this.eventListenerService = eventListenerService;
        }

        private IEnumerator Start()
        {
            eventListenerService.OnRestartGame += OnRestartGame;
            spawnTime = spawnDelay;
            yield return new WaitUntil(() => poolService.IsInitialize);
            yield return SpawnEnemyRoutine();
        }

        private void OnDestroy()
        {
            eventListenerService.OnRestartGame -= OnRestartGame;
        }

        private void OnRestartGame()
        {
            StopAllCoroutines();
            spawnTime = spawnDelay;
            StartCoroutine(SpawnEnemyRoutine());
        }

        private IEnumerator SpawnEnemyRoutine()
        {
            SpawnEnemy(ObjectType.BlueEnemy);
            for (int i = 0; i < ContOfRedEnemy; i++)
            {
                SpawnEnemy(ObjectType.RedEnemy);
            }

            yield return new WaitForSeconds(spawnTime);
            spawnTime -= Time.deltaTime;
            spawnTime = spawnTime > MinSpawnDelay ? spawnTime : MinSpawnDelay;
            yield return SpawnEnemyRoutine();
        }

        private void SpawnEnemy(ObjectType objectType)
        {
            var randomPosition = Random.insideUnitSphere * radius;
            var yPosition = randomPosition.y;
            if (yPosition <= 0f)
            {
                yPosition = 2f;
            }

            randomPosition = new Vector3(randomPosition.x, yPosition, randomPosition.z);
            GameObject enemy = poolService.GetObjectFromPool(objectType);
            enemy.transform.position = randomPosition;
        }
    }
}