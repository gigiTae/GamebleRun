using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using GambleRun.Manager;
using GambleRun.Items;

namespace GambleRun
{
    public struct SlotInit
    {
        public Sprite Icon;
        public uint ItemCount;
        public int SlotIndex;
        public bool IsIdentified;
        public string Name;
        public string Description;
        public ItemRarity Rarity;

        public SlotInit(Sprite icon, uint itemCount, int slotIndex,
           string name, string description, ItemRarity rarity = ItemRarity.None, bool isIdentified = true)
        {
            Icon = icon;
            ItemCount = itemCount;
            SlotIndex = slotIndex;
            Name = name;
            Description = description;
            IsIdentified = isIdentified;
            Rarity = rarity;
        }
    }

    [UxmlElement]
    public partial class Slot : VisualElement
    {
        private Image _icon;
        private Label _stackLabel;
        private int _slotIndex = -1;
        private string _name;
        private string _description;
        private ItemRarity _rarity;

        public int SlotIndex => _slotIndex;
        public string Name => _name;
        public string Description => _description;

        public Slot()
        {
            _icon = new Image();
            _icon.AddToClassList("slot_icon");
            _icon.pickingMode = PickingMode.Ignore;
            Add(_icon);

            _stackLabel = new Label();
            _stackLabel.AddToClassList("slot_stack_label");
            _stackLabel.pickingMode = PickingMode.Ignore;

            Add(_stackLabel);

            RegisterCallback<GeometryChangedEvent>(evt =>
            {
                // 1. 현재 계산된 가로 너비를 가져옵니다.
                float currentWidth = evt.newRect.width;

                // 2. 가로 너비와 동일하게 세로 높이를 강제로 할당합니다.
                style.height = currentWidth;
            });
        }

        public void Setup(SlotInit data)
        {
            _slotIndex = data.SlotIndex;
            _name = data.Name;
            _description = data.Description;
            _rarity = data.Rarity;
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

        public void SetIconVisibilty(bool isVisible)
        {
            _icon.style.visibility = isVisible ? Visibility.Visible : Visibility.Hidden;
        }

        private void SetStackLabel(uint itemCounts)
        {
            _stackLabel.text = itemCounts.ToString();
            _stackLabel.style.display = itemCounts < 2 ? DisplayStyle.None : DisplayStyle.Flex;
        }

        public bool IsVaildSlot()
        {
            return _icon != null && _name != string.Empty;
        }
    }
}
