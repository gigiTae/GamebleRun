using GambleRun;
using UnityEngine;
using UnityEngine.UIElements;

namespace GambleRun
{
    public class Inventory : MonoBehaviour
    {
        private UIDocument _uiDocument;

        [SerializeField]
        private Storage _backpack;
        [SerializeField]
        private Storage _equipment;
        [SerializeField]
        private Storage _spoils;

        private bool _isOpen;

        // event
        [SerializeField] private ItemBoxEvent _itemBoxOpenEvent;
        [SerializeField] private ItemBoxEvent _itemBoxCloseEvent;

        void Awake()
        {
            SubscribeEvent();

            _uiDocument = GetComponent<UIDocument>();
            if (_uiDocument != null)
            {
                CloseInventory();
            }
        }

        private void OnDestroy()
        {
            UnsubscribeEvent();
        }

        private void SubscribeEvent()
        {
            if (_itemBoxOpenEvent != null)
            {
                _itemBoxOpenEvent.Subscribe(OnOpenItemBox);
            }
            if (_itemBoxCloseEvent != null)
            {
                _itemBoxCloseEvent.Subscribe(OnCloseItemBox);
            }
        }
        private void UnsubscribeEvent()
        {
            if (_itemBoxOpenEvent != null)
            {
                _itemBoxOpenEvent.Unsubscribe(OnOpenItemBox);
            }
            if (_itemBoxCloseEvent != null)
            {
                _itemBoxCloseEvent.Unsubscribe(OnCloseItemBox);
            }
        }

        private void OnOpenItemBox(StorageData data)
        {
            _spoils.RefreshStorage(data);
            OpenFullInventory();    
        }

        private void OnCloseItemBox(StorageData data)
        {
        }

        private void OpenFullInventory()
        {
            _uiDocument.rootVisualElement.style.display = DisplayStyle.Flex;
        }

        private void CloseInventory()
        {
            _uiDocument.rootVisualElement.style.display = DisplayStyle.None;
        }
    }

}