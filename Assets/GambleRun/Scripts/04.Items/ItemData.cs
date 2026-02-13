using UnityEngine;


namespace GambleRun.Items
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

    [CreateAssetMenu(fileName = "ItemData", menuName = "GameData/ItemData")]
    public class ItemData : ScriptableObject
    {
        public SerializableGuid Id = SerializableGuid.NewGuid();

        [Header("Base")]
        public string ItemName;
        public int Price = 1;
        public float Weight = 0;
        public string Descrition;

        [Header("Stacking")]
        public uint MaxStack = 99;      // 최대 중첩 수

        public bool IsStackable => MaxStack > 1;

        [Header("Visual & Type")]
        public Sprite Icon;
        public ItemRarity Rarity = ItemRarity.Common;
        public ItemType Type = ItemType.Etc;

        public Item Create(uint quantity)
        {
            return new Item(this, quantity);
        }
    }
}
