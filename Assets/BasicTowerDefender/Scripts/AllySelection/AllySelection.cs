using BasicTowerDefender.Ally;
using UnityEngine;

namespace BasicTowerDefender.Manager
{
    public class AllySelection : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer allySprite;
        [SerializeField] private Allies allyPrefab;
        private ISelectionController iSelectionController;
        private LevelManager levelManager;

        [SerializeField] private SpriteRenderer moveableSprite;
        private SpriteRenderer moveableSpriteInstance;
        private bool isAllySelected = false;

        public Allies AllyPrefab => allyPrefab;
        public bool IsAllySelected => isAllySelected;

        public void Initialize(ISelectionController iSelectionController, LevelManager levelManager)
        {
            this.iSelectionController = iSelectionController;
            this.levelManager = levelManager;
        }

        private void OnMouseDown()
        {
            if (levelManager.IsLevelOver)
            {
                return;
            }

            iSelectionController.SelectedAlly(this);
        }

        public void UnpickedAlly()
        {
            allySprite.color = new Color32(109, 109, 109, 255);
            isAllySelected = false;
        }

        public SpriteRenderer PickedAlly()
        {
            allySprite.color = Color.white;
            isAllySelected = true;
            moveableSpriteInstance = Instantiate(moveableSprite, transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity);
            return moveableSpriteInstance;
        }
    }
}