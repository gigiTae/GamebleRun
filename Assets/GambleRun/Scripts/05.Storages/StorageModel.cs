
using System.Collections.Generic;
using GambleRun.Items;

namespace GambleRun.Storages
{
    public class StorageModel
    {
        private StorageData _storageData;
        public StorageData Data => _storageData;

        public List<Item> Items => _storageData.Items;
        public void Bind(StorageData storageData)
        {
            _storageData = storageData;
            List<Item> Items = _storageData.Items;

            bool isNew = Items == null || Items.Count == 0;

            if (isNew)
            {
                Items = new List<Item>();
            }
            else
            {
                for (int i = 0; i < Items.Count; i++)
                {
                    if (Items[i] != null && Items[i].Data != null)
                    {
                        Items[i].Data = ItemDatabase.GetDetailsById(Items[i].DataId);
                    }
                }
            }

        }
        public void SetItem(Item item, int index)
        {
            _storageData.Items[index] = item;
        }
    }

}