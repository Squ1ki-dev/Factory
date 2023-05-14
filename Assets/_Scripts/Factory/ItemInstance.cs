using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInstance
{
    public ItemInstance(ItemConfig config, Transform parent = null)
    {
        this.config = config;
        view = Object.Instantiate(config.itemViewPrefab, parent);
    }
    public ItemConfig config { private set; get; }
    public ItemView view { private set; get; }
}
