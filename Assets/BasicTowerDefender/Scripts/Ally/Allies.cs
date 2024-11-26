using TowerDefender.Manager;
using UnityEngine;

namespace TowerDefender.Manager
{
    public class Allies : MonoBehaviour
    {
        [SerializeField] private int pointCost;

        public int PointCost => pointCost;

        public void AddPointToScore(int points)
        {
            UIManager.Instance.AddToScore(points);
        }
    }
}