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
    }
}