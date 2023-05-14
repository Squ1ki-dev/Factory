using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum InteractType
{
    Put,
    Take
}
public class InteractableStorageObject : BaseStorageObject
{
    public override bool Add(ItemInstance item) => iStorage.Add(item);
    public override bool IsCanAddItem(ItemInstance item) => iStorage.IsCanAddItem(item);
    public override bool ItemExistInStorage(ItemConfig item) => iStorage.ItemExistInStorage(item);
    public override ItemInstance TakeLast(ItemConfig item) => iStorage.TakeLast(item);
    public override void UpdatePlaces() => iStorage.UpdatePlaces();
    public override bool IsEmpty => iStorage.IsEmpty;
    public override bool IsFull => iStorage.IsFull;
    public InteractType interactType;

    Coroutine playerCoroutine;
    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();
        if (player)
        {
            if (playerCoroutine != null) StopCoroutine(playerCoroutine);
            if (interactType == InteractType.Put)
                playerCoroutine = StartCoroutine(MoveItemsToPlayer(player));
            else
                playerCoroutine = StartCoroutine(MoveItemsFromPlayer(player));
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
    IEnumerator MoveItemsFromPlayer(Player player)
    {
        while (!iStorage.IsFull)
        {
            if (!player.storage.IsEmpty)
            {
                player.storage.possibleItems.ForEach(item =>
                {
                    if (iStorage.Add(player.storage.TakeLast(item))) return;
                });
                yield return new WaitForSeconds(player.takeTime);
            }
            else yield return null;
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
                    if (player.storage.Add(iStorage.TakeLast(item))) return;
                });
                yield return new WaitForSeconds(player.takeTime);
            }
            else yield return null;
        }
    }
}
