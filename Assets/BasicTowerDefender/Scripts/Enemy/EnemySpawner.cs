using System.Collections;
using System.Collections.Generic;
using BasicTowerDefender.Manager;
using UnityEngine;

namespace BasicTowerDefender.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private List<EnemyProperties> enemies;

        [SerializeField] private float minSpawnDelay, maxSpawnDelay;
        private IEnumerator spawnEnemiesCoroutine;
        private bool isSpawning = false;


        private LevelManager levelManager;

        public void Initialize(LevelManager levelManager)
        {
            this.levelManager = levelManager;
            StartSpawnEnemy();
        }

        private IEnumerator SpawnEnemy()
        {
            StartCoroutine(SpawnEnemyAtPosition());
            yield return null;
        }

        private IEnumerator SpawnEnemyAtPosition()
        {
            isSpawning = true;
            while (isSpawning)
            {
                yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
                var randomEnemyIndex = Random.Range(0, enemies.Count);
                var enemy = Instantiate(enemies[randomEnemyIndex].EnemyPrefab, transform.position, Quaternion.identity);
                enemy.Initialize(enemies[randomEnemyIndex], levelManager);
                enemy.transform.parent = transform;
                levelManager.CountEnemies();
            }
        }


        private void StartSpawnEnemy()
        {
            StopSpawnEnemy();
            spawnEnemiesCoroutine = SpawnEnemy();
            StartCoroutine(spawnEnemiesCoroutine);
        }

        public void StopSpawnEnemy()
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