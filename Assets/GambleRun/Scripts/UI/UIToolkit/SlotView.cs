using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace GambleRun
{
    public struct SlotViewInit
    {
        public Sprite Icon;
        public uint ItemCount;
        public SlotViewInit(Sprite icon, uint itemCount)
        {
            Icon = icon;
            ItemCount = itemCount;
        }
    }

    [UxmlElement]
    public partial class SlotView : VisualElement
    {
        private Image _icon;
        private Label _stackLabel;

        public SlotView()
        {
            _icon = new Image();
            _icon.AddToClassList("slot_icon");
            Add(_icon);

            _stackLabel = new Label();
            _stackLabel.AddToClassList("slot_stack_label");
            Add(_stackLabel);

            RegisterCallback<PointerDownEvent>(OnPointerDown);
        }
        private void OnPointerDown(PointerDownEvent evt)
        {
        }

        public void Setup(SlotViewInit data)
        {
            SetIcon(data.Icon);
            SetStackLabel(data.ItemCount);
        }

        public void ClearSlot()
        {
            SetStackLabel(0);
            SetIcon(null);
        }

        private void SetIcon(Sprite icon)
        {
            _icon.sprite = icon;
            _icon.style.display = icon == null ? DisplayStyle.None : DisplayStyle.Flex;
        }

        private void SetStackLabel(uint itemCounts)
        {
            _stackLabel.text = itemCounts.ToString();
            _stackLabel.style.display = itemCounts < 2 ? DisplayStyle.None : DisplayStyle.Flex;
        }
    }
}
