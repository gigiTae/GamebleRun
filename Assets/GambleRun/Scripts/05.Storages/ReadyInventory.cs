using GambleRun;
using GambleRun.Event;
using GambleRun.Input;
using GambleRun.Storages;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

/// <summary>
/// 준비화면 전용 인벤토리
/// </summary>
public class ReadyInventory : MonoBehaviour
{
    [SerializeField] private ReadyInputReader _inputReader;
    private ReadyInputAction _inputActions;

    private UIDocument _uiDocument;

    [SerializeField] private Storage _storeStorage;     // 창고
    [SerializeField] private Storage _backpackStorage;  // 가방
    [SerializeField] private Storage _equipmentStorage; // 장비창

    [SerializeField] private InventoryEvent _inventoryCloseEvent;
    [SerializeField] private InventoryEvent _inventoryOpenEvent;

    void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();

        if (_uiDocument != null)
        {
            _uiDocument.rootVisualElement.style.display = DisplayStyle.None;
        }

        if (_inputReader != null)
        {
            _inputActions = _inputReader.ReadyInputAction;
            BindInputAction();
        }
    }
    private void OnDestroy()
    {
        ReleaseInputAction();
    }

    private void BindInputAction()
    {
        _inputActions.Ready.OpenInventory.performed += OnOpenInventoryContext;
        _inputActions.Inventory.CloseInventory.performed += OnCloseInventoryContext;
    }

    private void ReleaseInputAction()
    {
        _inputActions.Ready.OpenInventory.performed -= OnOpenInventoryContext;
        _inputActions.Inventory.CloseInventory.performed -= OnCloseInventoryContext;
    }

    private void OnOpenInventoryContext(InputAction.CallbackContext context)
    {
        OpenInventory();
    }

    private void OnCloseInventoryContext(InputAction.CallbackContext context)
    {
        CloseInventory();
    }

    private void OpenInventory()
    {
        _uiDocument.rootVisualElement.style.display = DisplayStyle.Flex;
        _inventoryOpenEvent.Raise();
    }


    private void CloseInventory()
    {
        _uiDocument.rootVisualElement.style.display = DisplayStyle.None;
        _inventoryCloseEvent.Raise();
    }


}
