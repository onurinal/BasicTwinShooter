using UnityEngine;

namespace TowerDefender.Manager
{
    public class SpaceManager : MonoBehaviour
    {
        [SerializeField] private Transform backgroundModel;

        [SerializeField] private Transform leftTree, rightTree;
        [SerializeField] private Transform leftTreeModel, rightTreeModel;
        private Camera mainCamera;

        private void Start()
        {
            mainCamera = Camera.main;
            if (mainCamera != null)
            {
                ReSizeBackgrounds();
            }
        }

        private void ReSizeBackgrounds()
        {
            float aspectRatio = (float)mainCamera.pixelWidth / mainCamera.pixelHeight;

            // Calculate the camera sizes in world units for 2d orthographic camera
            float cameraHeight = mainCamera.orthographicSize * 2;
            float cameraWidth = cameraHeight * aspectRatio;

            // Calculate the size of background in "world units" based on its original scale
            var spriteRenderer = backgroundModel.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                float backgroundOriginalScaleX = backgroundModel.localScale.x;
                float backgroundOriginalScaleY = backgroundModel.localScale.y;

                float backgroundWidth = spriteRenderer.bounds.size.x / backgroundOriginalScaleX;
                float backgroundHeight = spriteRenderer.bounds.size.y / backgroundOriginalScaleY;

                // Calculate the scaling factor for x and y based on camera size
                float newScaleX = cameraWidth / backgroundWidth;
                float newScaleY = cameraHeight / backgroundHeight;

                backgroundModel.localScale = new Vector3(newScaleX, newScaleY, backgroundModel.localScale.z);
            }

            // Calculate only x scale factor because the game is designed for landscape orientation
            var newLeftTreePositionX = -(cameraWidth / 2) + leftTreeModel.localScale.x / 1.2f;
            var newRightTreePositionX = (cameraWidth / 2) - rightTreeModel.localScale.x / 1.2f;

            leftTree.position = new Vector3(newLeftTreePositionX, leftTree.position.y, leftTree.position.z);
            rightTree.position = new Vector3(newRightTreePositionX, rightTree.position.y, rightTree.position.z);
        }
    }
}