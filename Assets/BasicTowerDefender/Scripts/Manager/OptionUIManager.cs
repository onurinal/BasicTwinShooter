using UnityEngine;
using UnityEngine.UI;

namespace BasicTowerDefender.Manager
{
    public class OptionUIManager : MonoBehaviour
    {
        [SerializeField] private Slider volumeSlider;

        private void Start()
        {
            volumeSlider.value = PlayerPrefController.GetMasterVolume();
        }

        private void Update()
        {
            PlayerPrefController.SetMasterVolume(volumeSlider.value);
            var volume = PlayerPrefController.GetMasterVolume();
            AudioManager.Instance.SetMasterVolume();
        }

        public void SetDefaultVolume()
        {
            volumeSlider.value = AudioManager.DefaultMasterVolume;
        }
    }
}