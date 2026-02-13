using GambleRun.Event;
using UnityEngine;

namespace GambleRun.Input
{

    public class ReadyInputStateController : MonoBehaviour
    {
        [SerializeField] private ReadyInputReader _inputReader;
        private ReadyInputAction _inputActions;

        // Event
        [SerializeField] private InventoryEvent _inventoryCloseEvent;
        [SerializeField] private InventoryEvent _inventoryOpenEvent;

        // 게임의 가능한 입력 상태들을 정의
        public enum InputState
        {
            Ready,           // 평상시 
            Inventory,       // 메뉴/인벤토리
        }

        private InputState _currentState = InputState.Ready;

        private void Awake()
        {
            _inputActions = _inputReader?.ReadyInputAction;

            _inventoryOpenEvent.Subscribe(OnOpenInventoryBox);
            _inventoryCloseEvent.Subscribe(OnCloseInventory);
        }

        private void OnDestroy()
        {
            _inventoryOpenEvent?.Unsubscribe(OnOpenInventoryBox);
            _inventoryCloseEvent?.Unsubscribe(OnCloseInventory);
        }

        private void OnOpenInventoryBox()
        {
            ChangeState(InputState.Inventory);
        }

        private void OnCloseInventory()
        {
            ChangeState(InputState.Ready);
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
                case InputState.Ready:
                    _inputActions.Ready.Enable();
                    break;

                case InputState.Inventory:
                    _inputActions.Inventory.Enable();
                    break;
            }
        }
    }

}