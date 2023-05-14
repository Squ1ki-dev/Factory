using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StorageVertical : BaseStorage
{
    
    [SerializeField] private float offsetY;

    public override void UpdatePlaces()
    {
        for (int i = 0; i < inventory.items.Count; i++)
        {
            inventory.items[i].view.LocalMoveTo(new Vector3(0, i * offsetY, 0), Vector3.zero);
        }
    }
}
