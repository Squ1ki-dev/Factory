using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseStorage : MonoBehaviour, IStorage
{
    public virtual IStorage iStorage => null;
    public abstract bool Add(ItemInstance item);
    public abstract bool IsCanAddItem(ItemInstance item);
    public abstract bool ItemExistInStorage(ItemConfig item);
    public abstract ItemInstance TakeLast(ItemConfig item);
    public abstract void UpdatePlaces();
    public virtual bool IsEmpty => true;
    public virtual bool IsFull => true;
}
