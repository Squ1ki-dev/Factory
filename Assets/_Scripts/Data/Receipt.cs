using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Receipt", menuName = "Configs/Receipt")]
public class Receipt : ScriptableObject
{
    public List<ReceiptItem> put;
    public ItemConfig get;
    public float timeForCraft;
    public bool CanCraftFrom(IStorage storage)
    {
        int trueItems = 0;
        put.ForEach(ri => trueItems += ri.count <= storage.Count(ri.item) ? 1 : 0);
        return trueItems == put.Count;
    }
}
[System.Serializable]
public class ReceiptItem
{
    public ItemConfig item;
    public int count;
}