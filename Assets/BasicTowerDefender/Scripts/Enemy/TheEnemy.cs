using TowerDefender.Manager;
using UnityEngine;

namespace TowerDefender.Enemy
{
    public class TheEnemy : MonoBehaviour
    {
        private EnemyProperties enemyProperties;
        private bool isMoving = false;
        private int currentHealth;

        public void Initialize(EnemyProperties enemyProperties)
        {
            this.enemyProperties = enemyProperties;
            currentHealth = this.enemyProperties.MaxHealth;
        }

        private void Update()
        {
            if (isMoving)
            {
                Move();
            }
        }

        private void Move()
        {
            transform.Translate(Vector2.left * (enemyProperties.MoveSpeed * Time.deltaTime));
        }

        public void SetMovementActive()
        {
            isMoving = true;
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}