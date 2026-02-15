using GambleRun.Config;
using System;
using System.Linq;
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
        public event Action<PointerUpEvent> RootPointerUpEvent;

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
                    _tooltipTitle = _tooltipPanel.Q<Label>("tooltip_title_label");
                    _tooltipDetail = _tooltipPanel.Q<Label>("tooltip_detail_label");
                }
                else
                {
                    Debug.LogWarning("tooltip_panel을 찾을 수 없습니다");
                }

                if (_slotContainer != null)
                {
                    _slotContainer.RegisterCallback<PointerDownEvent>(OnPointerDown);
                    _slotContainer.RegisterCallback<PointerUpEvent>(OnPointerUp);
                    _slotContainer.RegisterCallback<PointerOverEvent>(OnPointerEnterSlot);
                    _slotContainer.RegisterCallback<PointerOutEvent>(OnPointerLeaveSlot);

                    _uiDocument.rootVisualElement.RegisterCallback<PointerUpEvent>(OnRootPointerUp);
                }

            }
        }

        private void OnDisable()
        {
            if (_slotContainer != null)
            {
                _slotContainer.UnregisterCallback<PointerDownEvent>(OnPointerDown);
                _slotContainer.UnregisterCallback<PointerUpEvent>(OnPointerUp);
                _slotContainer.UnregisterCallback<PointerOverEvent>(OnPointerEnterSlot);
                _slotContainer.UnregisterCallback<PointerOutEvent>(OnPointerLeaveSlot);

                _uiDocument.rootVisualElement.UnregisterCallback<PointerUpEvent>(OnRootPointerUp);
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

        private void OnRootPointerUp(PointerUpEvent evt)
        {
            RootPointerUpEvent?.Invoke(evt);
        }

        private void OnPointerEnterSlot(PointerOverEvent evt)
        {
            if (_tooltipPanel != null && evt.target is Slot slot && slot.IsVaildSlot())
            {
                // 1. 툴팁을 먼저 표시 (표시되어야 위치 계산이 정확해질 수 있음)
                _tooltipPanel.style.display = DisplayStyle.Flex;

                // 2. 슬롯의 오른쪽 경계 좌표(xMax) 가져오기
                // worldBound는 화면상의 절대 좌표를 제공합니다.
                float slotRightWorld = slot.worldBound.xMax;

                // 3. 부모 좌표계로 변환하여 적용
                // 슬롯의 오른쪽 끝에서 약간의 여백(예: 5px)을 주고 싶다면 + 5f를 더합니다.
                Vector2 targetPos = new Vector2(slotRightWorld + 5f, slot.worldBound.yMin);

                // 툴팁의 부모(parent) 공간 기준으로 좌표를 변환합니다.
                Vector2 localPos = _tooltipPanel.parent.WorldToLocal(targetPos);

                _tooltipPanel.style.left = localPos.x;
                _tooltipPanel.style.top = localPos.y;

                // tooltip 설정
                _tooltipTitle.text = slot.Name;
                _tooltipDetail.text = slot.Description;
            }
        }
        private void OnPointerLeaveSlot(PointerOutEvent evt)
        {
            if (_tooltipPanel != null && evt.target is Slot slot)
            {
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