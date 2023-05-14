using UnityEngine;

[System.Serializable]
public class StorageVertical : IStorage
{
    [SerializeField] private Transform posForFirstItem;
    [SerializeField] private float offsetY;
    [SerializeField] private Inventory inventory;
    public bool Add(ItemInstance item)
    {
        if (inventory.IsPossibleItem(item))
        {
            item.view.transform.SetParent(posForFirstItem);
            var result = inventory.AddItem(item);
            UpdatePlaces();
            return result;
        }
        return false;
    }
    public bool ItemExistInStorage(ItemConfig item) => inventory.ItemExistInInventory(item);
    public bool IsCanAddItem(ItemInstance item) => inventory.IsPossibleItem(item);
    public ItemInstance Remove(ItemConfig item)
    {
        if (inventory.ItemExistInInventory(item))
        {
            UpdatePlaces();
            return inventory.RemoveItem(item);
        }
        return null;
    }

    public void UpdatePlaces()
    {
        for (int i = 0; i < inventory.items.Count; i++)
        {
            inventory.items[i].view.LocalMoveTo(new Vector3(0, i * offsetY, 0), Vector3.zero);
        }
    }

    public bool IsEmpty => inventory.IsEmpty;
    public bool IsFull => inventory.IsFull;
}
