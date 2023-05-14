using UnityEngine;

[System.Serializable]
public class StorageVertical : IStorage
{
    [SerializeField] private Transform posForFirstItem;
    [SerializeField] private float offsetY;
    [SerializeField] private Inventory inventory;
    public bool Add(Item item)
    {
        if (inventory.IsPossibleItem(item))
        {
            item.transform.SetParent(posForFirstItem);
            UpdatePlaces();
            return inventory.AddItem(item);
        }
        return false;
    }
    public bool ItemExistInStorage(ItemConfig item) => inventory.ItemExistInInventory(item);
    public bool IsCanAddItem(Item item) => inventory.IsPossibleItem(item);
    public bool Remove(ItemConfig item)
    {
        if (inventory.ItemExistInInventory(item))
        {
            UpdatePlaces();
            return inventory.RemoveItem(item);
        }
        return false;
    }

    public void UpdatePlaces()
    {
        for (int i = 0; i < inventory.items.Count; i++)
        {
            inventory.items[i].LocalMoveTo(new Vector3(0, i * offsetY, 0), Vector3.zero);
        }
    }

    public bool IsEmpty => inventory.IsEmpty;
    public bool IsFull => inventory.IsFull;
}
