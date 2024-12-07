using System;
using System.Collections;
using BasicTowerDefender.Level;
using UnityEngine;

namespace BasicTowerDefender.Manager
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] AudioSource audioSource;
        [SerializeField] private AudioClip levelCompleteSound;
        [SerializeField] private AudioClip gameplayMusic;

        private LevelLoader levelLoader;

        public static AudioManager Instance;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
        }

        private void Start()
        {
            levelLoader = LevelLoader.Instance;
            StartCoroutine(PlayGameplayMusic());
        }

        public void PlayLevelCompleteSound()
        {
            audioSource.clip = levelCompleteSound;
            audioSource.PlayOneShot(levelCompleteSound);
        }

        private IEnumerator PlayGameplayMusic()
        {
            yield return new WaitForSeconds(levelLoader.WaitSplashScreenTime);
            audioSource.PlayOneShot(gameplayMusic);
        }
    }
}