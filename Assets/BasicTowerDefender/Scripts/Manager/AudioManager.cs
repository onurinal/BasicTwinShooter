using UnityEngine;

namespace BasicTowerDefender.Manager
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] AudioSource audioSource;
        [SerializeField] private AudioClip levelCompleteSound;

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
            }
        }

        public void PlayLevelCompleteSound()
        {
            audioSource.clip = levelCompleteSound;
            audioSource.PlayOneShot(levelCompleteSound);
        }
    }
}