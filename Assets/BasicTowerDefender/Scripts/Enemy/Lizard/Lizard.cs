using BasicTowerDefender.Ally;
using BasicTowerDefender.Enemy;
using UnityEngine;

namespace TowerDefender.Enemy.Lizard
{
    public class Lizard : MonoBehaviour
    {
        [SerializeField] private TheEnemy enemy;

        private void OnTriggerEnter2D(Collider2D other)
        {
            var allies = other.GetComponentInParent<Allies>();

            if (allies != null)
            {
                enemy.StartAttack(allies);
            }
        }
    }
}