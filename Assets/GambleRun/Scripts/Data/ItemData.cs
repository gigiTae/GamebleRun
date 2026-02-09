using UnityEngine;


namespace GambleRun
{
    // 아이템 등급
    public enum ItemRarity
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary
    }
    public enum ItemType
    {
        Backpack,      // 가방
        Accessory,     // 장신구
        Consumable,    // 소비 아이템
        Material,      // 제작 재료
        Etc            // 잡탬 
    }

    // 아이템 기본적인 데이터를 설정
    [CreateAssetMenu(fileName = "ItemData", menuName = "GameData/ItemData")]
    public class ItemData : ScriptableObject
    {
        [Header("Base")]
        public string ItemName = "Empty";
        public int Price = 1;
        public int Weight = 0;

        [Header("Stacking")]
        public bool IsStackable = true; // 중첩 가능 여부
        public uint Stack = 1;          // 현재 갯수
        public uint MaxStack = 99;      // 최대 중첩 수

        [Header("Visual & Type")]
        public Sprite Icon = null;
        public ItemRarity Rarity = ItemRarity.Common;
        public ItemType Type = ItemType.Etc;
        public bool IsIdentified = true; 
        
        public ItemData Clone()
        {
            ItemData data = CreateInstance<ItemData>();
            data.ItemName = ItemName;
            data.Price = Price;
            data.Weight = Weight;
            data.IsStackable = IsStackable;
            data.Stack = Stack; 
            data.MaxStack = MaxStack;
            data.Icon = Icon;
            data.Rarity = Rarity;
            data.Type = Type;
            data.IsIdentified = IsIdentified;
            return data;
        }
    }
}
