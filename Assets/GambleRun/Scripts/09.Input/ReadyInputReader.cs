using UnityEngine;

namespace GambleRun.Input
{
    /// <summary>
    /// 준비화면 Input
    /// </summary>
    [CreateAssetMenu(fileName = "ReadyInputReader", menuName = "Input/ReadyInputReader")]
    public class ReadyInputReader : ScriptableObject
    {
        private ReadyInputAction _inputActions;
        public ReadyInputAction ReadyInputAction { get { return _inputActions; } }
        public ReadyInputAction.ReadyActions ReadyActions => _inputActions.Ready;
        public ReadyInputAction.InventoryActions InventoryActions => _inputActions.Inventory;

        private void OnEnable()
        {
            _inputActions = new ReadyInputAction();
            _inputActions.Enable();
        }

        private void OnDisable()
        {
            _inputActions?.Disable();
        }
    }

}

