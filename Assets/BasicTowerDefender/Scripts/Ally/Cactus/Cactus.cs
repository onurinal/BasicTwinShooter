using UnityEngine;

namespace TowerDefender.Ally
{
    public class Cactus : MonoBehaviour
    {
        [SerializeField] private Transform projectileSpawn;
        [SerializeField] private GameObject projectile;

        public void CreateProjectile()
        {
            projectile = Instantiate(projectile, projectileSpawn.position, projectileSpawn.rotation);
        }
    }
}