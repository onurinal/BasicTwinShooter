using System;
using UnityEngine;

namespace TowerDefender.Enemy
{
    public class TheEnemy : MonoBehaviour
    {
        private EnemyProperties enemyProperties;
        
        public void Initialize(EnemyProperties enemyProperties)
        {
            this.enemyProperties = enemyProperties;
        }

        private void Update()
        {
            transform.Translate(Vector2.left * (enemyProperties.MoveSpeed * Time.deltaTime));
        }
    }
}