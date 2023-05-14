using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseStorage : IStorage
{
    [field: SerializeField] public Transform root { get; private set; }
    [SerializeField] protected Inventory inventory;
    public List<ItemConfig> possibleItems => inventory.possibleItems;
    public bool Add(ItemInstance item)
    {
        if (inventory.IsPossibleItem(item) && !inventory.IsFull)
        {
            item.view.transform.SetParent(root);
            var result = inventory.AddItem(item);
            UpdatePlaces();
            return result;
        }
        return false;
    }
    public bool ItemExistInStorage(ItemConfig item) => inventory.ItemExistInInventory(item);
    public bool IsCanAddItem(ItemInstance item) => inventory.IsPossibleItem(item);
    public ItemInstance TakeLast(ItemConfig item)
    {
        if (inventory.ItemExistInInventory(item))
        {
            UpdatePlaces();
            return inventory.RemoveItem(item);
        }
        return null;
    }
    public abstract void UpdatePlaces();
    public bool IsEmpty => inventory.IsEmpty;
    public bool IsFull => inventory.IsFull;
    public int Count => inventory.Count;
}
