using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Receipt", menuName = "Configs/Receipt")]
public class Receipt : ScriptableObject
{
    public List<ItemConfig> put;
    public ItemConfig get;
}