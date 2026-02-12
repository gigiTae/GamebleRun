
using System.Collections.Generic;
using System.Diagnostics;
using static UnityEditor.Progress;

namespace GambleRun
{
    public class StorageModel
    {
        private StorageData _storageData;
        public StorageData Data => _storageData;

        public List<Item> Items 
        {
            get
            {
                Debug.Assert(_storageData == null);
                return _storageData.Items;  
            }
        }

        public void Bind(StorageData storageData)
        {
            _storageData = storageData;
        }
        public void SetItem(Item item, int index)
        {
            _storageData.Items[index] = item;
        }
    }

}