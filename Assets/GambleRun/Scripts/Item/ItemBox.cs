using UnityEngine;

namespace GambleRun
{
    public class ItemBox : MonoBehaviour, IInteractable
    {
        [SerializeField]
        private StorageData _storage;

        [SerializeField]
        private ItemBoxEvent _itemBoxOpenEvent;
        private InteractionButtonView _interactionView;
        void Awake()
        {
            _interactionView = GetComponent<InteractionButtonView>();
        }

        public void Interact()
        {
            _itemBoxOpenEvent.Raise(_storage);
        }

        public bool IsInteractable()
        {
            return true;
        }

        public void OnEnterFocus()
        {
            _interactionView.Show(true);
        }

        public void OnExitFocus()
        {
            _interactionView.Show(false);
        }
    }
}