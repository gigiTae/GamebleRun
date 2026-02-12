using System.Drawing;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace GambleRun
{
    [UxmlElement]
    public partial class SlotContainer : VisualElement
    {
        private VisualElement _slotContainer;
        public SlotContainer()
        {
            _slotContainer = new VisualElement();
            _slotContainer.AddToClassList("storage_slotContainer");
            Add(_slotContainer);

            // UI Toolkit Build 전용
            //for (int i = 0; i < 10; ++i)
            //{
            //    AddSlot(new SlotInit(null, 0, 1, true));
            //}
        }
      
        public void ClearContainer()
        {
            _slotContainer.Clear();
        }

        public void AddSlot(SlotInit data)
        {
            Slot slot = new();
            slot.AddToClassList("slot");
            _slotContainer.Add(slot);
            slot.Setup(data);
        }

        public void RefreshSlot(int slotIndex, SlotInit data)
        {
            // 1. 인덱스 유효성 검사 (안전을 위해)
            if (slotIndex < 0 || slotIndex >= _slotContainer.childCount)
            {
                Debug.LogWarning($"[StorageView] Invalid slot index: {slotIndex}");
                return;
            }

            // 2. 해당 인덱스의 자식을 SlotView로 가져오기
            if (_slotContainer[slotIndex] is Slot slot)
            {
                slot.Setup(data);
            }
        }
    }
}