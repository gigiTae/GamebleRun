
using System;
using UnityEngine;


namespace GambleRun
{
    [Serializable]
    public class Item
    {
        [field: SerializeField] public SerializableGuid Id;
        [field: SerializeField] public SerializableGuid DataId;
        public ItemData Data;
        public uint Quantity;
        public bool IsIdentified;

        public Item() { }

        public Item(ItemData data, uint quantity, bool isIdentified = true)
        {
            Debug.Assert(data != null);

            Id = SerializableGuid.NewGuid();

            Data = data;
            DataId = data.Id;
            Quantity = quantity;
            IsIdentified = isIdentified;
        }

        /// <summary>
        /// 유효한 아이템인지 반환합니다
        /// </summary>
        public static bool IsValid(Item item)
        {
            // JsonUtility로 객체를 로드하면 null -> new()객체로 생성되기때문에
            return (item != null && item.Data != null);
        }
    }
}