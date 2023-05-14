using System;
using System.Collections.Generic;
using DG.Tweening;
using Tools;
using UnityEngine;
[System.Serializable]
public class Inventory
{
    [SerializeField] private List<ItemConfig> possibleItems;
    public List<Item> items { get; private set; }
    [SerializeField] private int limitItems = 10;
    public bool IsEmpty { get; private set; }
    public virtual void AddItem() {}
    public virtual void RemoveItem() {}

}
