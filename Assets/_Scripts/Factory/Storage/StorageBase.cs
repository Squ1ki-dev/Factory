using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class StorageBase : MonoBehaviour
{
    [field: SerializeField] public Inventory Inventory {get; private set;}
    [SerializeField] private float _intervalMoveItem = 0.2f;

    private float _timerMoveItem = 0f;
    protected Inventory _inventoryInTrigger = null;

    public virtual bool IsCanAddItem(GameObject objItem)
    {
        return Inventory.IsCanAddItem(objItem);
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;
        Inventory inventory = obj.GetComponent<Player>()?.inventory;

        if (inventory != null) 
            _inventoryInTrigger = inventory;
    }

    private void OnTriggerExit(Collider other) 
    {
        GameObject obj = other.gameObject;
        Inventory inventory = obj.GetComponent<Player>().inventory;

        if (inventory != null && _inventoryInTrigger == inventory) 
            _inventoryInTrigger = null;
    }

    private void FixedUpdate() 
    {
        if (_inventoryInTrigger != null) 
        {
            while(_timerMoveItem >= _intervalMoveItem) 
            {
                MoveItem();
                _timerMoveItem -= _intervalMoveItem;
            }
            _timerMoveItem += Time.fixedDeltaTime;
        }
        else
            _timerMoveItem = 0f;
    }

    protected abstract void MoveItem();
}

