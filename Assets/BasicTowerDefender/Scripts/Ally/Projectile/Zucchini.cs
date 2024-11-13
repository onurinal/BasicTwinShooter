using System;
using UnityEngine;

namespace TowerDefender.Ally
{
    public class Zucchini : MonoBehaviour
    {
        [SerializeField] [Range(0f, 10f)] private float projectileSpeed;

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            transform.Translate(Vector2.right * (projectileSpeed * Time.deltaTime));
        }
    }
}