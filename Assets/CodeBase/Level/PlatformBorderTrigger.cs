using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Level
{
    public class PlatformBorderTrigger : MonoBehaviour
    {
        [SerializeField] private float safeDistance;
        [SerializeField] private float safeZoneRadius;
        [SerializeField] private SphereCollider sphereCollider;
        
        private int enemyLayer;
        private int playerLayer;
        private Collider[] enemiesColliders;
        private bool isTriggered;

        private void Start()
        {
            playerLayer = LayerMask.NameToLayer("Player");
            enemyLayer = LayerMask.NameToLayer("Enemy");
        }

        private void OnTriggerExit(Collider other)
        {
            if (isTriggered) return;
            
            if (other.gameObject.layer == playerLayer)
            {
                isTriggered = true;
                
                enemiesColliders = Physics.OverlapSphere(other.transform.position, safeZoneRadius);
                foreach (Collider enemy in enemiesColliders)
                {
                    if (enemy != null && enemy.gameObject.layer == enemyLayer)
                    {
                        Vector3 randomPoint = GetRandomPointAwayFromEnemies(safeDistance);
                        other.transform.position = randomPoint;
                    }
                }
                
                isTriggered = false;
            }
        }
        
        private Vector3 GetRandomPointAwayFromEnemies(float minDistance)
        {
            var points = new List<Vector3>();
            var angle = Random.Range(0f, 2f * Mathf.PI);
            var halfWidth = sphereCollider.radius * Mathf.Cos(angle);
            var halfLength = sphereCollider.radius * Mathf.Sin(angle);
            Vector3 point = Vector3.zero;

            for (float x = -halfWidth; x <= halfWidth; x++)
            {
                for (float z = -halfLength; z <= halfLength; z++)
                {
                    point = new Vector3(x, 0, z);
                    if (!Physics.CheckSphere(point, 0.1f, enemyLayer))
                    {
                        points.Add(point);
                    }
                }
            }
            
            while (points.Count > 0)
            {
                int randomIndex = Random.Range(0, points.Count);
                point = points[randomIndex];
                points.RemoveAt(randomIndex);

                if (IsFarEnoughFromEnemies(point, minDistance))
                {
                    Debug.LogError(point);
                    return point;
                }
            }

            return new Vector3(Random.Range(-halfWidth, halfWidth), 0, Random.Range(-halfLength, halfLength));
        }
        
        private bool IsFarEnoughFromEnemies(Vector3 point, float minDistance)
        {
            for (int i = 0; i < enemiesColliders.Length; i++)
            {
                var distance = Vector3.Distance(point, enemiesColliders[i].transform.position);
                if (distance < minDistance)
                {
                    return false;
                }
            }

            return true;
        }
    }
}