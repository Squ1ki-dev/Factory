using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalStorageObject : InteractableStorageObject
{
    [SerializeField] private StorageVertical storage;
    public override IStorage iStorage => storage;
    public override bool Add(ItemInstance item) => storage.Add(item);
    public override ItemInstance TakeLast(ItemConfig item) => storage.TakeLast(item);
    public override bool IsEmpty => storage.IsEmpty;
    public override bool IsFull => storage.IsFull;
    public override bool IsCanAddItem(ItemInstance item) => storage.IsCanAddItem(item);
    public override bool ItemExistInStorage(ItemConfig item) => storage.ItemExistInStorage(item);
    public override void UpdatePlaces() => storage.UpdatePlaces(); 
}
