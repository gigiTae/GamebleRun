using System.Drawing;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace GambleRun
{
    [UxmlElement]
    public partial class StorageView : VisualElement
    {
        private VisualElement _slotContainer;
        public StorageView()
        {
            _slotContainer = new VisualElement();
            _slotContainer.AddToClassList("storage_slotContainer");
            Add(_slotContainer);
        }

        public void ClearContainer()
        {
            _slotContainer.Clear();
        }

        public void AddSlot(SlotViewInit data)
        {
            SlotView slot = new();
            slot.AddToClassList("slot");
            _slotContainer.Add(slot);
            slot.Setup(data);
        }

        public void RefreshSlot(int slotIndex ,SlotViewInit data)
        {
            // 1. 인덱스 유효성 검사 (안전을 위해)
            if (slotIndex < 0 || slotIndex >= _slotContainer.childCount)
            {
                Debug.LogWarning($"[StorageView] Invalid slot index: {slotIndex}");
                return;
            }

            // 2. 해당 인덱스의 자식을 SlotView로 가져오기
            if (_slotContainer[slotIndex] is SlotView slot)
            {
                slot.Setup(data);
            }
        }
    }
}