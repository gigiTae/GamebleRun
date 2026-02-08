using GambleRun;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace GambleRun
{
    enum InventoryState
    {
        Close, // 닫힘
        FullyOpen, // 전리품,장비,가방 
        PlayerPartOpen, // 장비, 가방 
    }

    public class Inventory : MonoBehaviour
    {
        // input
        [SerializeField] private InputManager _inputManager;
        private DefaultInputAction _inputActions;

        private UIDocument _uiDocument;

        // storage
        [SerializeField] private Storage _backpack;
        [SerializeField] private Storage _equipment;
        [SerializeField] private Storage _spoils;

        // event
        [SerializeField] private ItemBoxEvent _itemBoxOpenEvent;
        [SerializeField] private ItemBoxEvent _itemBoxCloseEvent;
        [SerializeField] private InventoryEvent _inventoryCloseEvent;

        InventoryState _state = InventoryState.Close;

        void Awake()
        {
            SubscribeEvent();

            _uiDocument = GetComponent<UIDocument>();

            if (_uiDocument != null)
            {
                _uiDocument.rootVisualElement.style.display = DisplayStyle.None;
            }

            if (_inputManager != null)
            {
                _inputActions = _inputManager.DefaultInputAction;
                BindInputAction();
            }
        }

        private void OnOpenInventoryContext(InputAction.CallbackContext context)
        {
            OpenPlayerPartInventory();
        }

        private void OnCloseInventoryContext(InputAction.CallbackContext context)
        {
            CloseInventory();
        }

        private void BindInputAction()
        {
            _inputActions.GamePlay.OpenInventory.performed += OnOpenInventoryContext;
            _inputActions.Inventory.CloseInventory.performed += OnCloseInventoryContext;
        }

        private void ReleaseInputAction()
        {
            _inputActions.GamePlay.OpenInventory.performed -= OnOpenInventoryContext;
            _inputActions.Inventory.CloseInventory.performed -= OnCloseInventoryContext;
        }


        private void OnDestroy()
        {
            UnsubscribeEvent();
            ReleaseInputAction();
        }

        private void SubscribeEvent()
        {
            if (_itemBoxOpenEvent != null)
            {
                _itemBoxOpenEvent.Subscribe(OnOpenItemBox);
            }
        }
        private void UnsubscribeEvent()
        {
            if (_itemBoxOpenEvent != null)
            {
                _itemBoxOpenEvent.Unsubscribe(OnOpenItemBox);
            }
        }

        private void OnOpenItemBox(StorageData data)
        {
            _spoils.RefreshStorage(data);
            OpenFullInventory();
        }

        private void OpenFullInventory()
        {
            _state = InventoryState.FullyOpen;

            // UI
            _uiDocument.rootVisualElement.style.display = DisplayStyle.Flex;
        }

        private void OpenPlayerPartInventory()
        {
            _state = InventoryState.PlayerPartOpen;

            // UI
            _uiDocument.rootVisualElement.style.display = DisplayStyle.Flex;
        }

        private void CloseInventory()
        {
            if(_state == InventoryState.FullyOpen)
            {
                _itemBoxCloseEvent.Raise(_spoils.Data);
            }

            _inventoryCloseEvent.Raise();
            Debug.Log("CloseInventoryInInventory");

            // UI
            _uiDocument.rootVisualElement.style.display = DisplayStyle.None;
            _state = InventoryState.Close;
        }
    }

}