using UnityEngine;

namespace TowerDefender.Ally
{
    public class SpawnAlly : MonoBehaviour
    {
        private Allies ally;

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

        public void SetSelectedAlly(Allies allySelected)
        {
            ally = allySelected;
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
                    var newCactus = Instantiate(ally, newPosition, Quaternion.identity);
                }
            }
#else
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
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
    }
}