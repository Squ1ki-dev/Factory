using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Factory : MonoBehaviour
{
    [SerializeField] private BaseStorageObject _inputItems = null;
    [SerializeField] private BaseStorageObject _outputItems = null;
    [SerializeField] private Receipt receipt;

    private void Awake()
    {
        StartCoroutine(HandleFactory());
        _inputItems.iStorage.possibleItems.Clear();
        _inputItems.iStorage.possibleItems.AddRange(receipt.put.Select(p => p.item));

        _outputItems.iStorage.possibleItems.Clear();
        _outputItems.iStorage.possibleItems.Add(receipt.get);
    }

    private IEnumerator HandleFactory()
    {
        while (true)
        {
            if (!_inputItems.IsEmpty && !_outputItems.IsFull && receipt.CanCraftFrom(_inputItems))
            {
                yield return new WaitForSeconds(receipt.timeForCraft);
                receipt.put.ForEach(receiptItem =>
                {
                    for (int i = 0; i < receiptItem.count; i++)
                    {
                        _inputItems.RemoveAndGetLast(receiptItem.item);
                    }
                });
                _outputItems.Add(new ItemInstance(receipt.get, transform));

            }
            else yield return null;
        }
    }
}
