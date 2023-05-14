using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    [SerializeField] private BaseStorage storage;
    [SerializeField] private ItemConfig generateItem;
    [SerializeField] private float craftTime;
    private void Awake()
    {
        StartCoroutine(HandleGenerator());
    }

    private IEnumerator HandleGenerator()
    {
        while (true)
        {
            if (!storage.IsFull)
            {
                var itemView = Instantiate(generateItem.itemViewPrefab);
                storage.Add(itemView);
                yield return new WaitForSeconds(craftTime);
            }
            else yield return null;
        }
    }
}
