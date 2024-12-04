using BasicTowerDefender.Ally;
using UnityEngine;

namespace BasicTowerDefender.Manager
{
    public class AllySelection : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer allySprite;
        private ISelectionController iSelectionController;
        [SerializeField] private Allies allyPrefab;

        [SerializeField] private SpriteRenderer moveableSprite;
        private SpriteRenderer moveableSpriteInstance;
        private bool isAllySelected = false;

        public Allies AllyPrefab => allyPrefab;
        public bool IsAllySelected => isAllySelected;

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
            if (moveableSpriteInstance != null)
            {
                Destroy(moveableSpriteInstance.gameObject);
            }


            allySprite.color = new Color32(109, 109, 109, 255);
            isAllySelected = false;
        }

        public SpriteRenderer PickedAlly()
        {
            allySprite.color = Color.white;
            isAllySelected = true;
            moveableSpriteInstance = Instantiate(moveableSprite, transform.position, Quaternion.identity);
            return moveableSpriteInstance;
        }
    }
}