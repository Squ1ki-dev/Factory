using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Tools;
using UnityEngine;
[System.Serializable]
public class Inventory
{
    [SerializeField] private List<ItemConfig> possibleItems;
    public List<ItemInstance> items { get; private set; } = new List<ItemInstance>();
    [SerializeField] private int limitItems = 10;
    public bool IsEmpty => items.Count == 0;
    public bool IsFull => items.Count >= limitItems;
    public virtual bool AddItem(ItemInstance item)
    {
        if (IsPossibleItem(item)) 
        {
            items.Add(item);
            return true;
        }
        return false;
    }
    public virtual ItemInstance RemoveItem(ItemConfig item) => items.Remove(i => i.config == item);
    public virtual bool IsPossibleItem(ItemInstance item) => possibleItems.Any(pItem => pItem == item.config);
    public virtual bool ItemExistInInventory(ItemConfig item) => items.Find(i => i.config == item) != null;

}
