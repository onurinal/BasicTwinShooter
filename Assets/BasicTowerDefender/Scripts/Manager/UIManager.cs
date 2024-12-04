using TMPro;
using UnityEngine;

namespace BasicTowerDefender.Manager
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject winLevelUI;
        [SerializeField] private GameObject loseLevelUI;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI playerLifeText;
        [SerializeField] private TextMeshProUGUI playerHealthText;

        private GameManager gameManager;
        public static UIManager Instance;

        public int CurrentScore { get; private set; } = 500;
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
            UpdateScore();
            UpdatePlayerLifeAndHealth();
        }

        private void UpdateScore()
        {
            scoreText.text = CurrentScore.ToString();
        }

        public void AddToScore(int scoreToAdd)
        {
            CurrentScore += scoreToAdd;
            UpdateScore();
        }

        public void SpendScore(int scoreToSpend)
        {
            CurrentScore -= scoreToSpend;
            UpdateScore();
        }

        public void UpdatePlayerLifeAndHealth()
        {
            playerLifeText.text = gameManager.CurrentPlayerLife.ToString();
            playerHealthText.text = gameManager.CurrentHealth.ToString();
        }
    }
}