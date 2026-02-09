using UnityEngine;


namespace GambleRun
{
    // 아이템 등급
    public enum ItemRarity
    {
        None,
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary
    }
    public enum ItemType
    {
        None,          // 기본값 (설정되지 않음)
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
        public string ItemName = "Empty";
        public int Price = 1;
        public int Weight = 0;
        public uint Count = 1;
        public Sprite Icon = null;
        public ItemRarity Rarity = ItemRarity.None;

        public ItemData Clone()
        {
            ItemData data = CreateInstance<ItemData>();
            data.ItemName = ItemName;
            data.Price = Price;
            data.Weight = Weight;
            data.Count = Count; 
            data.Icon = Icon;
            data.Rarity = Rarity;

            return data;
        }
    }
}
