using TowerDefender.Ally;
using UnityEngine;

namespace TowerDefender.AllySelection
{
    public class AllySelection : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        private ISelectionController iSelectionController;

        [SerializeField] private Allies allyPrefab;

        public Allies AllyPrefab => allyPrefab;

        public void Initialize(ISelectionController iSelectionController)
        {
            this.iSelectionController = iSelectionController;
        }

        private void OnMouseDown()
        {
            iSelectionController.SelectedAlly(this);
        }

        public void UnpickedAlly()
        {
            spriteRenderer.color = new Color32(109, 109, 109, 255);
        }

        public void PickedAlly()
        {
            spriteRenderer.color = Color.white;
        }
    }
}