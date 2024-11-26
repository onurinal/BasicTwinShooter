using System;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefender.Manager
{
    public class AllySelectManager : MonoBehaviour, ISelectionController
    {
        [SerializeField] private List<AllySelection> allies;
        [SerializeField] private SpawnAllyManager spawnAllyManager;

        private SpriteRenderer moveableSelectedSprite;
        private Camera mainCamera;

        public List<AllySelection> Allies => allies;

        private void Start()
        {
            if (mainCamera == null)
            {
                mainCamera = Camera.main;
            }

            foreach (var allySelection in allies)
            {
                allySelection.Initialize(this);
            }
        }

        private void Update()
        {
            DragSelectedAlly();
        }

        public void SelectedAlly(AllySelection selection)
        {
            for (var i = 0; i < allies.Count; i++)
            {
                allies[i].UnpickedAlly();
            }

            moveableSelectedSprite = selection.PickedAlly();
            spawnAllyManager.SetSelectedAlly(selection.AllyPrefab, moveableSelectedSprite, selection.IsAllySelected, this);
        }

        private void DragSelectedAlly()
        {
            if (moveableSelectedSprite == null)
            {
                return;
            }
#if UNITY_EDITOR
            var currentMousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            currentMousePosition.z = 0;

            moveableSelectedSprite.transform.position = currentMousePosition + new Vector3(0, 0.5f, 0);
#else
            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);
                var touchPosition = mainCamera.ScreenToWorldPoint(touch.position);
                touchPosition.z = 0;
                if (touch.phase == TouchPhase.Moved)
                {
                    moveableSelectedSprite.transform.position = touchPosition + new Vector3(0, 0.5f, 0);
                }
            }
#endif
        }
    }
}