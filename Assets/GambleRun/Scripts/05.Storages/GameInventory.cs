using GambleRun;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using GambleRun.Event;
using GambleRun.Input;

namespace GambleRun.Storages
{
    enum GameInventoryState
    {
        Close, // 닫힘
        FullyOpen, // 전리품,장비,가방 
        PlayerPartOpen, // 장비, 가방 
    }

    /// <summary>
    /// 게임 전용 인벤토리 클래스
    /// </summary>
    public class GameInventory : MonoBehaviour
    {
        // Input
        [SerializeField] private GameInputReader _inputReader;
        private GameInputAction _inputActions;

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

        GameInventoryState _state = GameInventoryState.Close;

        void Awake()
        {
            SubscribeEvent();

            _uiDocument = GetComponent<UIDocument>();

            if (_uiDocument != null)
            {
                _uiDocument.rootVisualElement.style.display = DisplayStyle.None;
            }

            if (_inputReader != null)
            {
                _inputActions = _inputReader.InputAction;
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
            _state = GameInventoryState.FullyOpen;
            _looStorage.SetVisible(true);

            _uiDocument.rootVisualElement.style.display = DisplayStyle.Flex;
            _inventoryOpenEvent.Raise();
        }

        private void OpenPlayerPartInventory()
        {
            _state = GameInventoryState.PlayerPartOpen;
            _looStorage.SetVisible(false);

            _uiDocument.rootVisualElement.style.display = DisplayStyle.Flex;
            _inventoryOpenEvent.Raise();
        }

        private void CloseInventory()
        {
            if (_state == GameInventoryState.FullyOpen)
            {
                _lootCloseEvent.Raise(_looStorage.GetStorageData());
            }

            _uiDocument.rootVisualElement.style.display = DisplayStyle.None;
            _state = GameInventoryState.Close;
            _inventoryCloseEvent.Raise();
        }
    }

}