using System.Collections.Generic;
using UnityEngine;

namespace TowerDefender.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        // just testing
        [SerializeField] private Transform enemySpawnPoint;
        [SerializeField] private List<EnemyProperties> enemyProperties;
        
        
        public static EnemySpawner Instance;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        private void Start()
        {
            // just testing
            CreateEnemy();
        }

        private void CreateEnemy()
        {
            var enemy = Instantiate(enemyProperties[0].EnemyPrefab,enemySpawnPoint.position, Quaternion.identity);
            enemy.Initialize(enemyProperties[0]);
        }
    }
}