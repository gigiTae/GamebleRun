using GambleRun.Persistence;
using System;
using System.Collections.Generic;
using UnityEngine;
using GambleRun.Items;
using Unity.Hierarchy;

namespace GambleRun.Storages
{
    public enum StorageType
    {
        None,
        Backpack,   // 가방
        Equipment,  // 장비창
        Store,      // 창고
        Loot        // 전리품 상자
    }

    [Serializable]
    public class StorageData : ISaveable
    {
        [field: SerializeField] public SerializableGuid Id { get; set; }

        public List<Item> Items = new();
        public StorageType Type = StorageType.None;

        public void SetAllItemsIdentification(bool isIdentified)
        {
            foreach (var item in Items)
            {
                if (item != null)
                {
                    item.IsIdentified = isIdentified;
                }
            }
        }

        //public void UpdateItems(List<ItemData> items)
        //{
        //    _items = items;
        //}

        /// <summary>
        // 아이템을 추가합니다 
        /// </summary>
        //public bool AddItem(ItemData item)
        //{
        //    int index = GetCloseEmptyIndex();

        //    if (index == -1)
        //    {
        //        Debug.Log("Storage is full");
        //        return false;
        //    }
        //    _items[index] = item;
        //    return true;
        //}

        /// <summary>
        // 저장공간의 인덱스의 아이템을 비웁니다
        /// </summary>
        //public void Empty(int index)
        //{
        //    if (_items.Count > index)
        //    {
        //        _items[index] = null;
        //    }
        //}

        /// <summary>
        // 저장공간이 가득찬지 확인합니다
        /// </summary>
        //bool IsFull()
        //{
        //    foreach (ItemData data in _items)
        //    {
        //        if (data == null) return false;
        //    }

        //    return true;
        //}

        /// <summary>
        // 가장가까운 빈공간의 인덱스를 반환합니다
        /// </summary>
        //public int GetCloseEmptyIndex()
        //{
        //    for (int i = 0; i < _items.Count; i++)
        //    {
        //        if (_items[i] == null) return i;
        //    }
        //    return -1;
        //}

        /// <summary>
        /// 스토리지의 빈공간 갯수를 반환
        /// </summary>
        //public int EmptySpaceCount
        //{
        //    get
        //    {
        //        int size = 0;
        //        for (int i = 0; i < _items.Count; i++)
        //        {
        //            if (_items[i] == null)
        //            {
        //                ++size;
        //            }
        //        }
        //        return size;
        //    }
        //}


        public void ResetStorage(int size)
        {
            Items.Clear();

            for (int i = 0; i < size; i++)
            {
                Items.Add(null);
            }
        }
    }
}