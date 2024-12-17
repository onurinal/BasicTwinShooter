using System.Collections.Generic;
using UnityEngine;

namespace BasicTowerDefender.Ally.Defender
{
    public class Defender : MonoBehaviour
    {
        [SerializeField] private Transform projectileSpawn;
        [SerializeField] private GameObject projectile;

        private Transform enemySpawner;
        private readonly List<Transform> enemySpawnPositionList = new List<Transform>();
        private Transform myLaneSpawner;

        [SerializeField] private Animator animator;
        private readonly int isAttackingHash = Animator.StringToHash("isAttacking");

        public void Initialize(Transform enemySpawner)
        {
            this.enemySpawner = enemySpawner;
        }

        private void Start()
        {
            SetEnemySpawnList();
            SetLaneSpawner();
        }

        private void Update()
        {
            if (IsEnemyInLane())
            {
                animator.SetBool(isAttackingHash, true);
            }
            else
            {
                animator.SetBool(isAttackingHash, false);
            }
        }


        public void CreateProjectile()
        {
            var newProjectile = Instantiate(projectile, projectileSpawn.position, projectileSpawn.rotation);
        }

        private void SetEnemySpawnList()
        {
            var enemySpawnerChildCount = enemySpawner.childCount;
            for (var i = 0; i < enemySpawnerChildCount; i++)
            {
                enemySpawnPositionList.Add(enemySpawner.GetChild(i));
            }
        }

        private void SetLaneSpawner()
        {
            foreach (var spawner in enemySpawnPositionList)
            {
                bool isCloseEnough = Mathf.Abs(spawner.position.y - transform.position.y) <= Mathf.Epsilon;
                if (isCloseEnough)
                {
                    myLaneSpawner = spawner;
                }
            }
        }

        private bool IsEnemyInLane()
        {
            if (myLaneSpawner.childCount <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}