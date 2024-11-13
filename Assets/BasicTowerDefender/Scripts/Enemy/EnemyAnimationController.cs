using UnityEngine;

namespace TowerDefender.Enemy
{
    public class EnemyAnimationController : MonoBehaviour
    {
        [SerializeField] private TheEnemy enemy;

        public void SetMovementActive()
        {
            enemy.SetMovementActive();
        }
    }
}