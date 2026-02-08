using GambleRun;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GambleRun
{
    public class InputStateController : MonoBehaviour
    {
        [SerializeField] private InputManager _inputManager;
        private DefaultInputAction _inputActions;
        
        // GameEvent
        [SerializeField] private ItemBoxEvent _itemBoxOpenEvent;
        [SerializeField] private InventoryEvent _inventoryCloseEvent;

        // 게임의 가능한 입력 상태들을 정의
        public enum InputState
        {
            Gameplay,        // 평상시 
            Inventory,       // 메뉴/인벤토리 (캐릭터 조작 전체 차단, UI만 가능)
        }

        private InputState _currentState = InputState.Gameplay;

        private void Awake()
        {
            _inputActions = _inputManager?.DefaultInputAction;

            _itemBoxOpenEvent.Subscribe(OnOpenItemBox);
            _inventoryCloseEvent.Subscribe(OnCloseInventory);
        }

        private void OnDestroy()
        {
            _itemBoxOpenEvent?.Unsubscribe(OnOpenItemBox);
            _inventoryCloseEvent?.Unsubscribe(OnCloseInventory);
        }

        private void OnOpenItemBox(StorageData data)
        {
            ChangeState(InputState.Inventory);
        }

        private void OnCloseInventory()
        {
            ChangeState(InputState.Gameplay);
            Debug.Log("CloseInventory In Input");
        }

        private void ChangeState(InputState newState)
        {
            _currentState = newState;
            ApplyConfiguration();
        }

        private void ApplyConfiguration()
        {
            _inputActions.Disable();

            switch (_currentState)
            {
                case InputState.Gameplay:
                    _inputActions.GamePlay.Enable();
                    break;

                case InputState.Inventory:
                    _inputActions.Inventory.Enable();
                    break;
            }
        }
    }
}