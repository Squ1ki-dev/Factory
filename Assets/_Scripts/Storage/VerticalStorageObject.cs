using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalStorageObject : InteractableStorageObject
{
    [SerializeField] protected StorageVertical storage;
    public override IStorage iStorage => storage;
}
