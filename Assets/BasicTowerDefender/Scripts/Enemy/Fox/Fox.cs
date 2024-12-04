using BasicTowerDefender.Ally;
using BasicTowerDefender.Enemy;
using TowerDefender.Manager;
using UnityEngine;

namespace TowerDefender.Enemy.Fox
{
    public class Fox : MonoBehaviour
    {
        [SerializeField] private TheEnemy enemy;
        [SerializeField] private Animator animator;
        private readonly int isJumping = Animator.StringToHash("isJumping");

        private void OnTriggerEnter2D(Collider2D other)
        {
            var gravestone = other.GetComponentInParent<Gravestone>();
            if (gravestone)
            {
                animator.SetTrigger(isJumping);
            }
            else
            {
                var allies = other.GetComponentInParent<Allies>();
                if (allies != null)
                {
                    enemy.StartAttack(allies);
                }
            }
        }
    }
}