using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace GambleRun.UI
{
    public class StorageView : MonoBehaviour
    {
        [SerializeField] UIDocument _uiDocument;
        [SerializeField] string _storagePanelName;
        [SerializeField] string _tooltipPanelName;

        private VisualElement _storagePanel;
        private Label _storageLabel;
        private SlotContainer _slotContainer;

        private VisualElement _tooltipPanel;
        private Label _tooltipTitle;
        private Label _tooltipDetail;


        public event Action<PointerDownEvent> PointerDownEvent;
        public event Action<PointerUpEvent> PointerUpEvent;

        private void OnEnable()
        {
            if (_uiDocument != null)
            {
                _storagePanel = _uiDocument.rootVisualElement.Q(_storagePanelName);
                _storageLabel = _storagePanel.Q<Label>();
                _slotContainer = _storagePanel.Q<SlotContainer>();

                _tooltipPanel = _uiDocument.rootVisualElement.Q(_tooltipPanelName);
                if (_tooltipPanel != null)
                {
                    _tooltipTitle = _tooltipPanel.Q<Label>(_tooltipPanelName + "_title_label");
                    _tooltipDetail = _tooltipPanel.Q<Label>(_tooltipPanelName + "_detail_label");
                }
                else
                {
                    Debug.LogWarning("tooltip_panel을 찾을 수 없습니다");
                }

                if (_slotContainer != null)
                {
                    _slotContainer.RegisterCallback<PointerDownEvent>(OnPointerDown);
                    _slotContainer.RegisterCallback<PointerUpEvent>(OnPointerUp);
                    _slotContainer.RegisterCallback<PointerEnterEvent>(OnPointerEnterSlot);
                    _slotContainer.RegisterCallback<PointerLeaveEvent>(OnPointerLeaveSlot);
                }

            }
        }

        private void OnDisable()
        {
            if (_slotContainer != null)
            {
                _slotContainer.UnregisterCallback<PointerDownEvent>(OnPointerDown);
                _slotContainer.UnregisterCallback<PointerUpEvent>(OnPointerUp);
                _slotContainer.UnregisterCallback<PointerEnterEvent>(OnPointerEnterSlot);
                _slotContainer.UnregisterCallback<PointerLeaveEvent>(OnPointerLeaveSlot);
            }
        }

        private void OnPointerDown(PointerDownEvent evt)
        {
            PointerDownEvent?.Invoke(evt);
        }

        private void OnPointerUp(PointerUpEvent evt)
        {
            PointerUpEvent?.Invoke(evt);
        }

        private void OnPointerEnterSlot(PointerEnterEvent evt)
        {

            if (_tooltipPanel != null && evt.target is Slot slot)
            {
                Debug.Log("PointerEnter");
                _tooltipPanel.style.display = DisplayStyle.Flex;
                _tooltipPanel.style.left = slot.style.right;
                _tooltipPanel.style.top = slot.style.top;
            }

        }
        private void OnPointerLeaveSlot(PointerLeaveEvent evt)
        {
            if (_tooltipPanel != null && evt.target is Slot slot)
            {
                Debug.Log("PointerLeave");
                _tooltipPanel.style.display = DisplayStyle.None;
            }
        }

        public void SetVisible(bool isVisible)
        {
            _storagePanel.visible = isVisible;
        }

        public void AddSlot(SlotInit data)
        {
            _slotContainer.AddSlot(data);
        }

        public void RefreshSlot(int slotIndex, SlotInit data)
        {
            _slotContainer.RefreshSlot(slotIndex, data);
        }

        public void ClearContainer()
        {
            _slotContainer.ClearContainer();
        }

    }
}