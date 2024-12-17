using BasicTowerDefender.Level;
using TMPro;
using UnityEngine;

namespace BasicTowerDefender.Manager
{
    public class GameplayUIManager : MonoBehaviour
    {
        [SerializeField] private LevelManager levelManager;
        [SerializeField] private GameObject winLevelUI;
        [SerializeField] private GameObject loseLevelUI;
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI playerLifeText;
        [SerializeField] private TextMeshProUGUI playerHealthText;

        private GameManager gameManager;
        public static GameplayUIManager Instance;
        public GameObject WinLevelUI => winLevelUI;
        public GameObject LoseLevelUI => loseLevelUI;

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

        public void Initialize(GameManager gameManager)
        {
            this.gameManager = gameManager;
            UpdateLevelText();
            UpdateScore();
            UpdatePlayerLifeAndHealth();
        }

        private void UpdateLevelText()
        {
            levelText.text = LevelLoader.Instance.GetSceneName();
        }

        private void UpdateScore()
        {
            scoreText.text = levelManager.CurrentPoint.ToString();
        }

        public void AddToScore(int scoreToAdd)
        {
            levelManager.CurrentPoint += scoreToAdd;
            UpdateScore();
        }

        public void SpendScore(int scoreToSpend)
        {
            levelManager.CurrentPoint -= scoreToSpend;
            UpdateScore();
        }

        public void UpdatePlayerLifeAndHealth()
        {
            playerLifeText.text = gameManager.CurrentPlayerLife.ToString();
            playerHealthText.text = gameManager.CurrentHealth.ToString();
        }
    }
}