using System;
using TowerDefender.Manager;
using UnityEngine;

namespace TowerDefender.Enemy
{
    public class TheEnemy : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        private EnemyProperties enemyProperties;
        private bool isMoving = false;
        private int currentHealth;
        private readonly int isAttackingHash = Animator.StringToHash("isAttacking");
        private Allies currentTarget;


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

            UpdateAttackAnimation();
        }

        private void Move()
        {
            transform.Translate(Vector2.left * (enemyProperties.MoveSpeed * Time.deltaTime));
        }

        public void SetMovementActive()
        {
            isMoving = true;
        }

        public void SetMovementInactive()
        {
            isMoving = false;
        }

        private void UpdateAttackAnimation()
        {
            if (!currentTarget)
            {
                animator.SetBool(isAttackingHash, false);
            }
        }

        public void StartAttack(Allies target)
        {
            currentTarget = target;
            animator.SetBool(isAttackingHash, true);
        }

        public void DealDamageToAlly()
        {
            if (!currentTarget)
            {
                return;
            }

            currentTarget.TakeDamage(enemyProperties.DamageToAlly);
        }

        public int GetDamageToPlayer()
        {
            return enemyProperties.DamageToPlayerHealth;
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