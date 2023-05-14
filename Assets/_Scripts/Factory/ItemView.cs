using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class ItemView : MonoBehaviour
{
    public UnityEvent onEndMove = new UnityEvent();

    public void LocalMoveTo(Vector3 pos, Vector3 rotation) //=> MoveTo(pos, Quaternion.Euler(rotation));
    {
        float duration = 0.5f;
        transform.DOKill();
        transform.DOLocalRotate(rotation, duration);
        transform.DOLocalMove(pos, duration * 0.5f).OnComplete(() => transform.DOLocalMove(pos, duration * 0.5f)).OnComplete(() => onEndMove?.Invoke());
    }
}
