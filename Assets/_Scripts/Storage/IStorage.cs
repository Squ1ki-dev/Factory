using System;
using System.Collections.Generic;
using UnityEngine;

public interface IStorage
{
    [SerializeField] private float intervalMoveItem => 0.2f;
    public List<ItemConfig> possibleItems => null;
    public bool IsFull => false;
    public bool IsEmpty => false;
    public bool ItemExistInStorage(ItemConfig item);
    public ItemInstance TakeLast(ItemConfig item);
    public bool Add(ItemInstance item);
    public bool IsCanAddItem(ItemInstance item);
    public void UpdatePlaces();
}

