using UnityEngine;

namespace TowerDefender.Ally
{
    public class SpawnAlly : MonoBehaviour
    {
        [SerializeField] private Cactus cactusPrefab;

        // for snap to grid
        private const float ScaleFactor = 1.92f;
        private const float HalfScaleFactor = ScaleFactor / 2.0f;


        private Camera mainCamera;

        private void Start()
        {
            mainCamera = Camera.main;
        }

        private void OnMouseDown()
        {
            CreateAlly();
        }

        private void CreateAlly()
        {
#if UNITY_EDITOR
            var currentMousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            currentMousePosition.z = 0f;
            var newPosition = SnapDefenderToGrid(currentMousePosition);
            if (Input.GetMouseButtonDown(0))
            {
                var newCactus = Instantiate(cactusPrefab, newPosition, Quaternion.identity);
            }
#else
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                var touchPosition = mainCamera.ScreenToWorldPoint(touch.position);
                touchPosition.z = 0f;
                var newCactus = Instantiate(cactusPrefab, touchPosition, Quaternion.identity);
            }
#endif
        }

        private Vector3 SnapDefenderToGrid(Vector3 mousePosition)
        {
            var newPosition = mousePosition;
            newPosition.x = SnapPositionToGrid(mousePosition.x);
            newPosition.y = SnapPositionToGrid(mousePosition.y);
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
    }
}