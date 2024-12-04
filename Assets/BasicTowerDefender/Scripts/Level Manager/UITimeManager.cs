using UnityEngine;
using UnityEngine.UI;

namespace BasicTowerDefender.Manager
{
    public class UITimeManager : MonoBehaviour
    {
        private LevelManager levelManager;
        [SerializeField] private Slider levelTimer;
        private float levelTime;

        public void Initialize(float levelTime)
        {
            this.levelTime = levelTime;
        }

        public void UpdateLevelTimer(float currentTime)
        {
            levelTimer.value = currentTime / levelTime;
        }
    }
}