using System.Collections.Generic;
using UnityEngine;

namespace BasicTowerDefender.Manager
{
    public class AllySelectManager : MonoBehaviour, ISelectionController
    {
        [SerializeField] private List<AllySelection> allies;
        [SerializeField] private SpawnAllyManager spawnAllyManager;
        [SerializeField] private LevelManager levelManager;

        private SpriteRenderer moveableSelectedSprite;

        public bool IsAllyReadyToCreate { get; set; }

        private Camera mainCamera;

        public List<AllySelection> Allies => allies;

        public void Initialize()
        {
            if (mainCamera == null)
            {
                mainCamera = Camera.main;
            }

            levelManager.Initialize(this);
            foreach (var allySelection in allies)
            {
                allySelection.Initialize(this, levelManager);
            }
        }

        private void Update()
        {
            DragSelectedAlly();
        }

        public void SelectedAlly(AllySelection selection)
        {
            // if player choose already selected defender then deselect it
            if (selection.IsAllySelected)
            {
                DeselectedAlly();
                IsAllyReadyToCreate = false;
                return;
            }

            // if player choose other than selected defender then deselect other defenders
            DeselectedAlly();

            moveableSelectedSprite = selection.PickedAlly();
            IsAllyReadyToCreate = true;
            spawnAllyManager.SetSelectedAlly(selection.AllyPrefab, moveableSelectedSprite, this);
        }

        public void DeselectedAlly()
        {
            if (moveableSelectedSprite != null)
            {
                Destroy(moveableSelectedSprite.gameObject);
            }

            for (var i = 0; i < allies.Count; i++)
            {
                allies[i].UnpickedAlly();
            }
        }

        private void DragSelectedAlly()
        {
            if (!moveableSelectedSprite)
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