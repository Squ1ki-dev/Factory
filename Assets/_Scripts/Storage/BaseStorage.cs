using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStorage : MonoBehaviour, IStorage
{
    public virtual bool Add(Item item)
    {
        throw new System.NotImplementedException();
    }

    public virtual bool IsCanAddItem(Item item)
    {
        throw new System.NotImplementedException();
    }

    public virtual bool ItemExistInStorage(ItemConfig item)
    {
        throw new System.NotImplementedException();
    }

    public virtual bool Remove(ItemConfig item)
    {
        throw new System.NotImplementedException();
    }

    public virtual void UpdatePlaces()
    {
        throw new System.NotImplementedException();
    }
    public virtual bool IsEmpty => true;
    public virtual bool IsFull => true;
}
