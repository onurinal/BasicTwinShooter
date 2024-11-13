using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TowerDefender.Level
{
    public class LoadScene : MonoBehaviour
    {
        private int currentScene;

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