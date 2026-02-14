using System.Collections.Generic;
using UnityEngine;


namespace GambleRun.Items
{
    // ItemDatabase Class
    public static class ItemDatabase
    {
        static Dictionary<SerializableGuid, ItemData> _itemDetailsDictionary;

        // TODO : 에셋로드방식변경
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        static void Initialize()
        {
            _itemDetailsDictionary = new Dictionary<SerializableGuid, ItemData>();

            // 폴더경로 Resources/ 안에 있어야함
            // LoadAll 이새끼는 쓰면 안될거 같다
            ItemData[] itemDatas = Resources.LoadAll<ItemData>("");
            foreach (var item in itemDatas)
            {
                _itemDetailsDictionary.Add(item.Id, item);
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
