using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class Pyramid : InteractableStorageObject
{
    [SerializeField] private PyramidStorage storage;
    public override IStorage iStorage => storage;
    private void Awake()
    {
        foreach (var chieldMesh in storage.root.GetComponentsInChildren<MeshRenderer>())
        {
            chieldMesh.enabled = false;
        }
        storage.SetPositions(storage.root.GetComponentsInChildren<Transform>().ToList().Skip(1).ToList());
    }
}
[System.Serializable]
public class PyramidStorage : BaseStorage
{
    private List<Transform> positions;
    public void SetPositions(List<Transform> pos)
    {
        inventory.limitItems = pos.Count;
        positions = pos;
    }
    public override void UpdatePlaces()
    {
        for (int i = 0; i < inventory.items.Count; i++)
        {
            var view = inventory.items[i].view;
            view.LocalMoveTo(positions[i].localPosition, positions[i].rotation.eulerAngles);
            view.transform.DOScale(positions[i].localScale, view.moveDuration);
        }
    }
}
