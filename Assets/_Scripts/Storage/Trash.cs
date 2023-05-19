using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : VerticalStorageObject
{
    [SerializeField] private float destroyTime;
    private void Awake()
    {
        StartCoroutine(Destroing());
    }
    IEnumerator Destroing()
    {
        var destroyWaiter = new WaitForSeconds(destroyTime);
        while (true)
        {
            if (storage.Count() > 0)
            {
                var item = RemoveAndGetLast();
                OnDestroyTrash(item);
                Destroy(item.view.gameObject);
                yield return destroyWaiter;
            }
            else yield return null;
        }
    }
    public virtual void OnDestroyTrash(ItemInstance item)
    {

    }
}
