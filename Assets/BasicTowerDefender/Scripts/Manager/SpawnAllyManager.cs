using TowerDefender.Enemy;
using UnityEngine;

namespace TowerDefender.Manager
{
    public class SpawnAllyManager : MonoBehaviour
    {
        [SerializeField] private Transform enemySpawner;
        private Camera mainCamera;
        private Allies ally;
        private AllySelectManager allySelectManager;
        private SpriteRenderer selectedAllySprite;
        private bool isAllySelected;


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
            AttemptToCreateAlly();
        }

        public void SetSelectedAlly(Allies allySelected, SpriteRenderer selectedAllySprite, bool isAllySelected, AllySelectManager allySelectManager)
        {
            ally = allySelected;
            this.selectedAllySprite = selectedAllySprite;
            this.isAllySelected = isAllySelected;
            this.allySelectManager = allySelectManager;
        }

        private void AttemptToCreateAlly()
        {
            var currentScore = UIManager.Instance.CurrentScore;

            if (ally == null)
            {
                return;
            }

            if (currentScore >= ally.PointCost && isAllySelected && !CheckOverlap())
            {
                CreateAlly();
                Destroy(selectedAllySprite.gameObject);
                isAllySelected = false;
                CleanSelectedAfterCreation();
                UIManager.Instance.SpendScore(ally.PointCost);
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
                if (ally != null)
                {
                    var theAlly = Instantiate(ally, newPosition, Quaternion.identity);
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
                var newCactus = Instantiate(ally, touchPosition, Quaternion.identity);
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
         then we can understand that this number is above the center of the square, otherwise if
         the number is bigger than halfScaleFactor then this position is below the center of square
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
            var hit = Physics2D.Raycast(currentMousePosition, Vector2.zero);
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