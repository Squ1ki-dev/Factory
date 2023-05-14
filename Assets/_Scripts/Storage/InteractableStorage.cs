using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableStorage : BaseStorage
{
    Coroutine playerCoroutine;
    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();
        if (player)
        {
            if (playerCoroutine != null) StopCoroutine(playerCoroutine);
            playerCoroutine = StartCoroutine(MoveItemsToPlayer(player));
        }
    }
    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Player>();
        if (player)
        {
            if (playerCoroutine != null) StopCoroutine(playerCoroutine);
        }
    }
    IEnumerator MoveItemsToPlayer(Player player)
    {
        while (!player.storage.IsFull)
        {
            if (!iStorage.IsEmpty)
            {
                iStorage.possibleItems.ForEach(item =>
                {
                    if(player.storage.Add(iStorage.TakeLast(item))) return;
                });
                yield return new WaitForSeconds(player.takeTime);
            }
            else yield return null;
        }
    }
}
