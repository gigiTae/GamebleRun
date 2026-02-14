using UnityEngine;


namespace GambleRun.Input
{
    /// <summary>
    /// GameInput
    /// </summary>
    [CreateAssetMenu(fileName = "GameInputReader", menuName = "Input/GameInputReader")]
    public class GameInputReader : ScriptableObject
    {
        private GameInputAction _inputActions;
        public GameInputAction InputAction { get { return _inputActions; } }
        public GameInputAction.GamePlayActions GamePlayActions => _inputActions.GamePlay;
        public GameInputAction.InventoryActions InventoryActions => _inputActions.Inventory;

        private void OnEnable()  
        {
            _inputActions = new GameInputAction();
            _inputActions.Enable();
        }

        private void OnDisable()
        {
            _inputActions?.Disable();

        }
    }

}
