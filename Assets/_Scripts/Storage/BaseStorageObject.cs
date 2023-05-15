using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseStorageObject : MonoBehaviour, IStorage
{
    public virtual IStorage iStorage => null;
    public abstract bool Add(ItemInstance item);
    public abstract bool IsCanAddItem(ItemInstance item);
    public abstract bool ItemExistInStorage(ItemConfig item);
    public abstract ItemInstance RemoveAndGetLast(ItemConfig item);
    public abstract void UpdatePlaces();
    public abstract ItemInstance GetLast(ItemConfig item);
    public abstract int Count(ItemConfig item);

    public virtual bool IsEmpty => true;
    public virtual bool IsFull => true;
}
