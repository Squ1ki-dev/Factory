using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Factory : MonoBehaviour
{
    [SerializeField] private BaseStorageObject _inputItems = null;
    [SerializeField] private BaseStorageObject _outputItems = null;
    [SerializeField] private float timeForOneCreate = 2f;
    [SerializeField] private Receipt receipt;

    private void Awake()
    {
        StartCoroutine(HandleFactory());
    }

    private IEnumerator HandleFactory()
    {
        while (true)
        {
            if (!_inputItems.IsEmpty && !_outputItems.IsFull && !receipt.put.Any(item => !_inputItems.ItemExistInStorage(item)))
            {
                ItemInstance takedItem = null;
                receipt.put.ForEach(item => takedItem = _inputItems.TakeLast(item));
                _outputItems.Add(takedItem);

                yield return new WaitForSeconds(timeForOneCreate);
            }
            else yield return null;
        }
    }
}
