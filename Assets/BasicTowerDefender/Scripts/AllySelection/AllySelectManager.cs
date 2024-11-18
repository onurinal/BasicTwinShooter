using System.Collections.Generic;
using TowerDefender.Ally;
using UnityEngine;

namespace TowerDefender.AllySelection
{
    public class AllySelectManager : MonoBehaviour, ISelectionController
    {
        [SerializeField] private List<AllySelection> allies;
        [SerializeField] private SpawnAlly spawnAlly;

        private void Start()
        {
            foreach (var allySelection in allies)
            {
                allySelection.Initialize(this);
            }
        }

        public void SelectedAlly(AllySelection selection)
        {
            for (var i = 0; i < allies.Count; i++)
            {
                allies[i].UnpickedAlly();
            }

            selection.PickedAlly();
            spawnAlly.SetSelectedAlly(selection.AllyPrefab);
        }
    }
}