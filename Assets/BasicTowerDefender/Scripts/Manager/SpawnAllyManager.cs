using BasicTowerDefender.Ally;
using BasicTowerDefender.Ally.Defender;
using UnityEngine;

namespace BasicTowerDefender.Manager
{
    public class SpawnAllyManager : MonoBehaviour
    {
        [SerializeField] private Transform enemySpawner;
        [SerializeField] private LevelManager levelManager;
        private Camera mainCamera;
        private Allies allyPrefab;
        private AllySelectManager allySelectManager;
        private SpriteRenderer selectedAllySprite;


        // for snap to grid
        private const float ScaleFactor = 1.92f;
        private const float HalfScaleFactor = ScaleFactor / 2.0f;

        private void Start()
        {
            if (mainCamera == null)
            {
                mainCamera = Camera.main;
            }
        }

        private void OnMouseDown()
        {
            if (levelManager.IsLevelOver)
            {
                return;
            }

            AttemptToCreateAlly();
        }

        public void SetSelectedAlly(Allies allySelectedPrefab, SpriteRenderer selectedAllySprite, AllySelectManager allySelectManager)
        {
            allyPrefab = allySelectedPrefab;
            this.selectedAllySprite = selectedAllySprite;
            this.allySelectManager = allySelectManager;
        }

        private void AttemptToCreateAlly()
        {
            var currentPoint = levelManager.CurrentPoint;

            if (allyPrefab == null)
            {
                return;
            }

            if (currentPoint >= allyPrefab.PointCost && allySelectManager.IsAllyReadyToCreate && !CheckOverlap())
            {
                CreateAlly();
                Destroy(selectedAllySprite.gameObject);
                allySelectManager.IsAllyReadyToCreate = false;
                CleanSelectedAfterCreation();
                GameplayUIManager.Instance.SpendScore(allyPrefab.PointCost);
            }
        }

        private void CreateAlly()
        {
#if UNITY_EDITOR
            var currentMousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            currentMousePosition.z = 0f;
            var newPosition = SnapDefenderToGrid(currentMousePosition);
            if (Input.GetMouseButtonDown(0))
            {
                if (allyPrefab != null)
                {
                    var theAlly = Instantiate(allyPrefab, newPosition, Quaternion.identity);
                    var defender = theAlly.gameObject.GetComponent<Defender>();
                    if (defender != null)
                    {
                        defender.Initialize(enemySpawner);
                    }
                }
            }
#else
            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);
                var touchPosition = mainCamera.ScreenToWorldPoint(touch.position);
                touchPosition.z = 0f;
                var newPosition = SnapDefenderToGrid(touchPosition);
                if (allyPrefab != null)
                {
                    var theAlly = Instantiate(allyPrefab, newPosition, Quaternion.identity);
                    var defender = theAlly.gameObject.GetComponent<Defender>();
                    if (defender != null)
                    {
                        defender.Initialize(enemySpawner);
                    }
                }
            }
#endif
        }

        private Vector3 SnapDefenderToGrid(Vector3 position)
        {
            var newPosition = position;
            newPosition.x = SnapPositionToGrid(position.x);
            newPosition.y = SnapPositionToGrid(position.y);
            return newPosition;
        }

        /* This method is rounding the numbers to center of square
         First get the mod of the number with scaleFactor and if it is smaller than halfScaleFactor
         then we can understand that this position is above the center of the square, otherwise if
         the remain is bigger than halfScaleFactor then this position is below the center of square
         It works the same as Mathf.RoundToInt()
         */
        private float SnapPositionToGrid(float position)
        {
            var newPosition = Mathf.Abs(position); // Get the absolute value of the position
            var remainder = newPosition % ScaleFactor;

            // If the remainder is less than half of the ScaleFactor, round down
            if (remainder < HalfScaleFactor)
            {
                newPosition -= remainder;
            }
            // If the remainder is greater than or equal to half, round up
            else
            {
                var newRemainder = ScaleFactor - remainder;
                newPosition += newRemainder;
            }

            return newPosition * Mathf.Sign(position);
        }

        private void CleanSelectedAfterCreation()
        {
            for (var i = 0; i < allySelectManager.Allies.Count; i++)
            {
                allySelectManager.Allies[i].UnpickedAlly();
            }
        }

        private bool CheckOverlap()
        {
#if UNITY_EDITOR
            var currentMousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            currentMousePosition.z = 0f;
            var newPosition = SnapDefenderToGrid(currentMousePosition);
            var hit = Physics2D.Raycast(newPosition, Vector2.zero);
#else
            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);
                var touchPosition = mainCamera.ScreenToWorldPoint(touch.position);
                touchPosition.z = 0f;
                var hit = Physics2D.Raycast(touchPosition, Vector2.zero);
            }
#endif
            var allies = hit.collider.GetComponentInParent<Allies>();
            if (allies != null)
            {
                return true;
            }

            return false;
        }
    }
}