using GambleRun;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using GambleRun.Event;
using GambleRun.Input;

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
        // Input
        [SerializeField] private InputManager _inputManager;
        private DefaultInputAction _inputActions;

        private UIDocument _uiDocument;

        // Storage
        [SerializeField] private Storage _backpackStorage;
        [SerializeField] private Storage _equipmentStorage;
        [SerializeField] private LootStorage _looStorage;

        // event
        [SerializeField] private LootEvent _lootOpenEvent;
        [SerializeField] private LootEvent _lootCloseEvent;
        [SerializeField] private InventoryEvent _inventoryCloseEvent;
        [SerializeField] private InventoryEvent _inventoryOpenEvent;

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
        private void Start()
        {
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
            _lootOpenEvent.Subscribe(OnOpenLoot);
        }
        private void UnsubscribeEvent()
        {
            _lootOpenEvent.Unsubscribe(OnOpenLoot);
        }

        private void OnOpenLoot(StorageData data)
        {
            _looStorage.BindAndIdentify(data);

            OpenFullInventory();
        }

        private void OpenFullInventory()
        {
            _state = InventoryState.FullyOpen;
            _looStorage.SetVisible(true);

            _uiDocument.rootVisualElement.style.display = DisplayStyle.Flex;
            _inventoryOpenEvent.Raise();
        }

        private void OpenPlayerPartInventory()
        {
            _state = InventoryState.PlayerPartOpen;
            _looStorage.SetVisible(false);

            _uiDocument.rootVisualElement.style.display = DisplayStyle.Flex;
            _inventoryOpenEvent.Raise();
        }

        private void CloseInventory()
        {
            if (_state == InventoryState.FullyOpen)
            {
                _lootCloseEvent.Raise(_looStorage.GetStorageData());
            }

            _uiDocument.rootVisualElement.style.display = DisplayStyle.None;
            _state = InventoryState.Close;
            _inventoryCloseEvent.Raise();
        }
    }

}