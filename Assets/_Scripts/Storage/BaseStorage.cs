using System.Collections;
using System.Collections.Generic;
using Tools.Reactive;
using Tools;
using UnityEngine;
using System.Linq;

public abstract class BaseStorage : IStorage
{
    [field: SerializeField] public Transform root { get; private set; }
    [field: SerializeField] public List<ItemConfig> possibleItems { get; private set; }
    [SerializeField] protected Inventory inventory;
    public List<ItemInstance> items { get; private set; } = new List<ItemInstance>();
    public float limitItems = 10;

    public bool Add(ItemInstance item)
    {
        if (IsCanAddItem(item) && !IsFull)
        {
            var result = AddItem(item);
            item.view.transform.SetParent(root);
            UpdatePlaces();
            return result;
        }
        return false;
    }

    public bool IsEmpty => items.Count == 0;
    public bool IsFull => items.Count >= limitItems;
    public virtual int Count(ItemConfig item) => items.FindAll(i => i.config == item).Count;
    public virtual bool ItemExistInStorage(ItemConfig item) => items.Find(i => i.config == item) != null;
    public ItemInstance GetLast(ItemConfig item) => items.Find(i => i.config == item);
    public bool IsCanAddItem(ItemInstance item) => item != null && possibleItems.Any(pItem => pItem == item.config);
    public abstract void UpdatePlaces();
    public int Count() => items.Count;
    public virtual bool AddItem(ItemInstance item)
    {
        if (IsCanAddItem(item) && !IsFull)
        {
            items.Add(item);
            return true;
        }
        return false;
    }
    public ItemInstance RemoveAndGetLast(ItemConfig item)
    {
        if (ItemExistInStorage(item))
        {
            var removed = items.Remove(i => i.config == item);
            UpdatePlaces();
            return removed;
        }
        return null;
    }

    public ItemInstance RemoveAndGetLast() => RemoveAndGetLast(items.Last().config);
}
