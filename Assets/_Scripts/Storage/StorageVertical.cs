using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[System.Serializable]
public class StorageVertical : BaseStorage
{

    [SerializeField] private float offsetY;
    [SerializeField] private float scaleOffset = 1;
    public override void UpdatePlaces()
    {
        for (int i = 0; i < items.Count; i++)
        {
            var view = items[i].view;
            view.LocalMoveTo(new Vector3(0, i * offsetY, 0), Vector3.zero);
            view.transform.DOScale(view.baseScale * scaleOffset, view.moveDuration);
        }
    }
}
