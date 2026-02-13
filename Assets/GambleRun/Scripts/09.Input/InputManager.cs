using UnityEngine;


namespace GambleRun.Input
{
    [CreateAssetMenu(fileName = "InputManager", menuName = "Manager/InputManager")]
    public class InputManager : ScriptableObject
    {
        private DefaultInputAction _inputActions;

        public DefaultInputAction DefaultInputAction { get { return _inputActions; } }
        public DefaultInputAction.GamePlayActions GamePlayActions => _inputActions.GamePlay;
        public DefaultInputAction.InventoryActions InventoryActions => _inputActions.Inventory;

        private void OnEnable()  
        {
            _inputActions = new DefaultInputAction();
            _inputActions.Enable();
        }

        private void OnDisable()
        {
            _inputActions?.Disable();
        }
    }

}
