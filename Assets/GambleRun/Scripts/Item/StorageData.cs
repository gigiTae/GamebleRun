using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

[CreateAssetMenu(fileName = "StorageData", menuName = "Scriptable Objects/StorageData")]
public class StorageData : ScriptableObject
{
    [SerializeField] private List<ItemData> _items = new();

    public IReadOnlyList<ItemData> Items => _items;
    public int MaxSlot => _items.Count;

    public bool AddItem(ItemData item)
    {
        _items.Add(item);
        return true;
    }
}
