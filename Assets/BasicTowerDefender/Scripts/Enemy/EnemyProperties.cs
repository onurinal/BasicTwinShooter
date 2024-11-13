using UnityEngine;

namespace TowerDefender.Enemy
{
    [CreateAssetMenu(fileName = "Enemy 1", menuName = "TowerDefender/Enemy/Create New Enemy Properties")]
    public class EnemyProperties : ScriptableObject
    {
        [SerializeField] private TheEnemy enemyPrefab;
        [SerializeField] [Range(0f, 5f)] private float moveSpeed;
        [SerializeField] private int maxHealth;

        public TheEnemy EnemyPrefab => enemyPrefab;
        public float MoveSpeed => moveSpeed;

        public int MaxHealth => maxHealth;
    }
}