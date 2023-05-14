using System;
using System.Collections.Generic;
using UnityEngine;

public interface IStorage
{
    [SerializeField] private float _intervalMoveItem => 0.2f;
    public bool isFull => false;
    public bool isEmpty => false;
    public bool Hes(Item item) => true;
    public bool Remove(Item item) => true;
    public bool Add(Item item) => true;
    public virtual bool IsCanAddItem(GameObject objItem) => true;
}

