using System;
using TMPro;
using UnityEngine;

namespace TowerDefender.Manager
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI playerLifeText;
        [SerializeField] private TextMeshProUGUI playerHealthText;

        private GameManager gameManager;
        public static UIManager Instance;

        public int CurrentScore { get; private set; } = 500;

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

        private void Start()
        {
            gameManager = GameManager.Instance;
            scoreText.text = CurrentScore.ToString();
            playerLifeText.text = gameManager.CurrentPlayerLife.ToString();
            playerHealthText.text = gameManager.CurrentHealth.ToString();
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