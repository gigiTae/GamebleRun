using System.Collections.Generic;
using UnityEngine;


namespace GambleRun
{
    // ItemDatabase Class
    public static class ItemDatabase
    {
        static Dictionary<SerializableGuid, ItemData> _itemDetailsDictionary;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        static void Initialize()
        {
            _itemDetailsDictionary = new Dictionary<SerializableGuid, ItemData>();

            var itemDetails = Resources.LoadAll<ItemData>("");
            foreach (var item in itemDetails)
            {
                _itemDetailsDictionary.Add(SerializableGuid.NewGuid(), item);
            }
        }

        public static ItemData GetDetailsById(SerializableGuid id)
        {
            try
            {
                return _itemDetailsDictionary[id];
            }
            catch
            {
                Debug.LogError($"Cannot find item details with id {id}");
                return null;
            }
        }
    }
}
