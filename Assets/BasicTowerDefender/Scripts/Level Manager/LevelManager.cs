using System.Collections.Generic;
using BasicTowerDefender.Enemy;
using UnityEngine;

namespace BasicTowerDefender.Manager
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private UITimeManager uiTimeManager;
        [SerializeField] private List<EnemySpawner> spawners;
        private int numberOfEnemies;

        [Tooltip("Level time in seconds")] [SerializeField]
        private float levelTime;

        private float currentTime;
        private bool isLevelTimeOver;

        private void Start()
        {
            InitializeSpawners();
            SetupNewLevel();
        }

        private void Update()
        {
            PlayLevel();
        }

        private void PlayLevel()
        {
            if (!isLevelTimeOver)
            {
                currentTime += Time.deltaTime;
                uiTimeManager.UpdateLevelTimer(currentTime);
                isLevelTimeOver = currentTime >= levelTime;
                if (isLevelTimeOver)
                {
                    StopEnemySpawn();
                }
            }
        }

        private void SetupNewLevel()
        {
            uiTimeManager.Initialize(levelTime);
            numberOfEnemies = 0;
        }

        private void InitializeSpawners()
        {
            foreach (var spawner in spawners)
            {
                spawner.Initialize(this);
            }
        }

        private void StopEnemySpawn()
        {
            foreach (var spawner in spawners)
            {
                spawner.StopSpawnEnemy();
            }
        }

        public void CountEnemies()
        {
            numberOfEnemies++;
        }

        public void KilledEnemies()
        {
            numberOfEnemies--;
            if (numberOfEnemies <= 0 && isLevelTimeOver)
            {
                LevelWon();
            }
        }

        public void LevelLost()
        {
            if (UIManager.Instance.LoseLevelUI != null)
            {
                UIManager.Instance.LoseLevelUI.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
        }

        private void LevelWon()
        {
            if (UIManager.Instance.WinLevelUI != null)
            {
                UIManager.Instance.WinLevelUI.gameObject.SetActive(true);
            }
        }
    }
}