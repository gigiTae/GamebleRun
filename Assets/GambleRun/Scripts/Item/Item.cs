
using UnityEngine;


namespace GambleRun
{
    public class Item
    {
        [field: SerializeField] public SerializableGuid Id;
        [field: SerializeField] public SerializableGuid DataId;
        public ItemData Data;
        public uint Quantity;
        public bool IsIdentified;

        public Item(ItemData data, uint quantity, bool isIdentified = true)
        {
            Debug.Assert(data != null);

            Id = SerializableGuid.NewGuid();
            
            Data = data;
            DataId = data.Id;
            Quantity = quantity;
            IsIdentified = isIdentified;
        }
    }

}