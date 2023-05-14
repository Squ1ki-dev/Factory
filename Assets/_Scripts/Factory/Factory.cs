using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Factory : MonoBehaviour
{
    public enum ReasonStop
    {
        NoResource,
        FullStorage
    }
    [Header("Storage")]
    [SerializeField] private IStorage _inputItems = null;
    [SerializeField] private IStorage _outputItems = null;

    [Header("Process create")]
    [SerializeField] private Vector3 _removedPosItem = Vector3.zero;
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
            if (!_inputItems.isEmpty && !_outputItems.isFull)
            {
                if (!receipt.put.Any(item => !_inputItems.Hes(item.itemView)))
                {
                    receipt.put.ForEach(item => _inputItems.Remove(item.itemView));
                    _outputItems.Add(receipt.get.itemView);
                }
                yield return new WaitForSeconds(timeForOneCreate);
            }
            else yield return null;
        }
    }
}
