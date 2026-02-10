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
        public int SlotIndex;
        public bool IsIdentified;

        public SlotViewInit(Sprite icon, uint itemCount, int slotIndex, bool isIdentified = true)
        {
            Icon = icon;
            ItemCount = itemCount;
            SlotIndex = slotIndex;
            IsIdentified = isIdentified;
        }
    }

    [UxmlElement]
    public partial class SlotView : VisualElement
    {
        private Image _icon;
        private Label _stackLabel;
        private int _slotIndex = -1;
        public int SlotIndex => _slotIndex;

        public SlotView()
        {
            _icon = new Image();
            _icon.AddToClassList("slot_icon");
            _icon.pickingMode = PickingMode.Ignore;
            Add(_icon);

            _stackLabel = new Label();
            _stackLabel.AddToClassList("slot_stack_label");
            _stackLabel.pickingMode = PickingMode.Ignore;
            Add(_stackLabel);
        }

        public void Setup(SlotViewInit data)
        {
            _slotIndex = data.SlotIndex;
            SetStackLabel(data.ItemCount);

            if (data.IsIdentified)
            {
                SetIcon(data.Icon);
            }
            else
            {
                SetIcon(ConfigManager.Instance.SlotConfig.SearchIcon);
            }
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
