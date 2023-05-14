using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public partial class ItemView : MonoBehaviour
{
    public UnityEvent onEndMove = new UnityEvent();
    public float moveDuration { get; private set; } = 0.5f;
    
    public void LocalMoveTo(Vector3 pos, Vector3 rotation)
    {
        transform.DOKill();
        transform.DOLocalRotate(rotation, moveDuration);
        transform.DOLocalMove(pos, moveDuration * 0.5f).OnComplete(() => transform.DOLocalMove(pos, moveDuration * 0.5f)).OnComplete(() => onEndMove?.Invoke());
    }
}
