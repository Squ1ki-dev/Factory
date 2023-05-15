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
            var result = inventory.AddItem(item);
            if (result)
                item.view.transform.SetParent(root);
            UpdatePlaces();
            return result;
        }
        return false;
    }
    public bool ItemExistInStorage(ItemConfig item) => inventory.ItemExistInInventory(item);
    public bool IsCanAddItem(ItemInstance item) => inventory.IsPossibleItem(item);
    public ItemInstance RemoveAndGetLast(ItemConfig item)
    {
        if (inventory.ItemExistInInventory(item))
        {
            var removed = inventory.RemoveLast(item);
            UpdatePlaces();
            return removed;
        }
        return null;
    }
    public ItemInstance GetLast(ItemConfig item)
    {
        if (inventory.ItemExistInInventory(item))
        {
            return inventory.GetLast(item);
        }
        return null;
    }

    public abstract void UpdatePlaces();
    public int Count(ItemConfig item) => inventory.Count(item);

    public bool IsEmpty => inventory.IsEmpty;
    public bool IsFull => inventory.IsFull;
}
