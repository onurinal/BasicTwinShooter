using UnityEngine;

namespace TowerDefender.Enemy
{
    public class TheEnemy : MonoBehaviour
    {
        private EnemyProperties enemyProperties;
        private bool isMoving = false;

        public void Initialize(EnemyProperties enemyProperties)
        {
            this.enemyProperties = enemyProperties;
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
    }
}