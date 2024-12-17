using BasicTowerDefender.Enemy;
using UnityEngine;

namespace TowerDefender.Manager
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] [Range(0f, 10f)] private float projectileSpeed;
        [SerializeField] private int projectileDamage;

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            transform.Translate(Vector2.right * (projectileSpeed * Time.deltaTime));
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var enemy = collision.GetComponentInParent<TheEnemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(projectileDamage);
                Destroy(gameObject);
            }
        }
    }
}