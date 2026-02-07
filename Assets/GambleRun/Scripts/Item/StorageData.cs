using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

[CreateAssetMenu(fileName = "StorageData", menuName = "Scriptable Objects/StorageData")]
public class StorageData : ScriptableObject
{
    [SerializeField] private List<ItemData> _items = new();
    public IReadOnlyList<ItemData> Items => _items;

    // 아이템을 추가합니다 
    public bool AddItem(ItemData item)
    {
        int index = GetEmptyIndex();

        if (index == -1)
        {
            Debug.Log("Storage is full");
            return false;
        }
        _items[index] = item;
        return true;
    }

    public void SetItem(ItemData data, int index)
    {
        _items[index] = data;
    }
    // 저장공간의 인덱스의 아이템을 비웁니다
    public void Empty(int index)
    {
        if (_items.Count > index)
        {
            _items[index] = null;
        }
    }

    // 저장공간이 가득찬지 확인합니다
    bool IsFull()
    {
        foreach (ItemData data in _items)
        {
            if (data == null) return false;
        }

        return true;
    }

    // 가장가까운 빈공간의 인덱스를 반환합니다
    public int GetEmptyIndex()
    {
        for (int i = 0; i < _items.Count; i++)
        {
            if (_items[i] == null) return i;
        }
        return -1;
    }

    public void ResetStorage(int size)
    {
        _items = new(size);
    }
}
