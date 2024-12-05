using System;
using System.Collections;
using System.Collections.Generic;
using BasicTowerDefender.Enemy;
using BasicTowerDefender.Level;
using UnityEngine;

namespace BasicTowerDefender.Manager
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private UITimeManager uiTimeManager;
        [SerializeField] private List<EnemySpawner> spawners;
        [SerializeField] private float waitWinLosePanel;
        private int numberOfEnemies;

        [Tooltip("Level time in seconds")] [SerializeField]
        private float levelTime;

        [SerializeField] private int point;

        private IEnumerator winCoroutine;
        private IEnumerator loseCoroutine;


        private float currentTime;
        private bool isLevelTimeOver;
        public int CurrentPoint { get; set; }
        private bool isLevelActive;

        public void Initialize()
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
            isLevelActive = true;
            uiTimeManager.Initialize(levelTime);
            numberOfEnemies = 0;
            CurrentPoint = point;
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
                StartLevelWinPanel();
            }
        }

        private IEnumerator LevelLosePanel()
        {
            isLevelActive = false;
            UIManager.Instance.LoseLevelUI.gameObject.SetActive(true);
            Time.timeScale = 0;
            yield return new WaitForSecondsRealtime(waitWinLosePanel);
        }

        private IEnumerator LevelWinPanel()
        {
            isLevelActive = false;
            UIManager.Instance.WinLevelUI.gameObject.SetActive(true);
            AudioManager.Instance.PlayLevelCompleteSound();
            Time.timeScale = 0;
            yield return new WaitForSecondsRealtime(waitWinLosePanel);
            LevelLoader.Instance.LoadNextScene();
        }

        private void StartLevelWinPanel()
        {
            if (!isLevelActive)
            {
                return;
            }

            StopLevelWinPanel();
            winCoroutine = LevelWinPanel();
            StartCoroutine(winCoroutine);
        }

        private void StopLevelWinPanel()
        {
            if (winCoroutine != null)
            {
                StopCoroutine(winCoroutine);
                winCoroutine = null;
            }
        }

        public void StartLevelLosePanel()
        {
            if (!isLevelActive)
            {
                return;
            }

            StopLevelLosePanel();
            loseCoroutine = LevelLosePanel();
            StartCoroutine(loseCoroutine);
        }

        private void StopLevelLosePanel()
        {
            if (loseCoroutine != null)
            {
                StopCoroutine(loseCoroutine);
                loseCoroutine = null;
            }
        }
    }
}