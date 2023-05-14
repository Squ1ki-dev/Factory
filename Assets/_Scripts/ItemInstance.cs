using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInstance
{
    public ItemInstance(ItemConfig config, Transform parent = null)
    {
        itemView = Object.Instantiate(config.itemViewPrefab, parent);
    }
    public ItemConfig config { private set; get; }
    public Item itemView { private set; get; }
}
