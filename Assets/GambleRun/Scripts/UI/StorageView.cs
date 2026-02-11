using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace GambleRun
{
    public class StorageView : MonoBehaviour
    {
        [SerializeField] UIDocument _uiDocument;
        [SerializeField] string _storagePanelName;

        private VisualElement _storagePanel;
        private Label _storageLabel;
        private SlotContainer _slotContainer;

        public event Action<PointerDownEvent> PointerDownEvent;
        public event Action<PointerUpEvent> PointerUpEvent;

        private void Awake()
        {
            if (_uiDocument != null)
            {
                _storagePanel = _uiDocument.rootVisualElement.Q(_storagePanelName);
                _storageLabel = _storagePanel.Q<Label>();
                _slotContainer = _storagePanel.Q<SlotContainer>();

                _slotContainer.RegisterCallback<PointerDownEvent>(OnPointerDown);
                _slotContainer.RegisterCallback<PointerUpEvent>(OnPointerUp);
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