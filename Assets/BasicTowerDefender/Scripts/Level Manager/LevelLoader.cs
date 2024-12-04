using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BasicTowerDefender.Level
{
    public class LevelLoader : MonoBehaviour
    {
        private int currentScene;

        public static LevelLoader Instance;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        private void Start()
        {
            currentScene = SceneManager.GetActiveScene().buildIndex;
            if (currentScene == 0)
            {
                StartCoroutine(WaitForSplashSceneLoad());
            }
        }

        public void LoadNextScene()
        {
            SceneManager.LoadScene(currentScene + 1);
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadScene("Main Menu");
            Time.timeScale = 1;
        }

        public void LoadSameScene()
        {
            SceneManager.LoadScene(currentScene);
            Time.timeScale = 1;
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        private IEnumerator WaitForSplashSceneLoad()
        {
            yield return new WaitForSeconds(3f);
            LoadNextScene();
        }
    }
}