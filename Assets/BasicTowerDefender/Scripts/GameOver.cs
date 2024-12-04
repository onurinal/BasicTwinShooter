using BasicTowerDefender.Enemy;
using BasicTowerDefender.Manager;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        var enemy = other.GetComponentInParent<TheEnemy>();
        if (enemy != null)
        {
            var damage = enemy.GetDamageToPlayer();
            GameManager.Instance.PlayerTakeDamage(damage);
            UIManager.Instance.UpdatePlayerLifeAndHealth();
            Destroy(enemy.gameObject);
        }
    }
}