using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefender.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        // just testing
        [SerializeField] private List<Transform> enemySpawnPosition;
        [SerializeField] private List<EnemyProperties> enemyProperties;

        [SerializeField] private float minSpawnDelay, maxSpawnDelay;
        private IEnumerator spawnEnemiesCoroutine;
        private bool isSpawning = false;


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
            StartSpawnEnemy();
        }

        private IEnumerator SpawnEnemy()
        {
            for (var i = 0; i < enemySpawnPosition.Count; i++)
            {
                StartCoroutine(SpawnEnemyAtPosition(i));
            }

            yield return null;
        }

        private IEnumerator SpawnEnemyAtPosition(int enemySpawnPositionIndex)
        {
            isSpawning = true;
            while (isSpawning)
            {
                yield return new WaitForSeconds(Random.Range(1f, 6f));
                var enemy = Instantiate(enemyProperties[0].EnemyPrefab, enemySpawnPosition[enemySpawnPositionIndex].position, Quaternion.identity);
                enemy.Initialize(enemyProperties[0]);
                enemy.transform.parent = enemySpawnPosition[enemySpawnPositionIndex];
            }
        }


        private void StartSpawnEnemy()
        {
            StopSpawnEnemy();
            spawnEnemiesCoroutine = SpawnEnemy();
            StartCoroutine(spawnEnemiesCoroutine);
        }

        private void StopSpawnEnemy()
        {
            if (spawnEnemiesCoroutine != null)
            {
                StopCoroutine(spawnEnemiesCoroutine);
                spawnEnemiesCoroutine = null;
                isSpawning = false;
            }
        }
    }
}