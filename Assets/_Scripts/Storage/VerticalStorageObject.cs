using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalStorageObject : InteractableStorageObject
{
    [SerializeField] private StorageVertical storage;
    public override IStorage iStorage => storage;
}
