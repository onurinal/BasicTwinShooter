using UnityEngine;

namespace TowerDefender.Ally
{
    public class ShootingController : MonoBehaviour
    {
        [SerializeField] private Cactus cactus;

        public void CreateProjectile()
        {
            cactus.CreateProjectile();
        }
    }
}