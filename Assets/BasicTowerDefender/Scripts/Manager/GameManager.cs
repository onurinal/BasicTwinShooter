﻿using UnityEngine;

namespace BasicTowerDefender.Manager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private LevelManager levelManager;
        [SerializeField] private int maxPlayerLife = 3;
        [SerializeField] private int maxHealth = 100;

        private int currentPlayerLife;
        private int currentHealth;

        public int CurrentPlayerLife => currentPlayerLife;
        public int CurrentHealth => currentHealth;

        public static GameManager Instance;

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
            ResetGame();
            UIManager.Instance.Initialize(this);
        }

        private void ResetGame()
        {
            currentPlayerLife = maxPlayerLife;
            currentHealth = maxHealth;
        }

        private void RemoveLife()
        {
            currentPlayerLife--;

            if (currentPlayerLife <= 0)
            {
                Debug.Log("Game Over");
                levelManager.LevelLost();
            }

            currentHealth = maxHealth;
        }

        public void PlayerTakeDamage(int damage)
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                RemoveLife();
            }
        }
    }
}