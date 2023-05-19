using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public partial class ItemView : MonoBehaviour
{
    [HideInInspector] public UnityEvent onEndMove = new UnityEvent();
    public float moveDuration { get; private set; } = 0.5f;
    public Vector3 baseScale { get; private set; }
    private void Awake()
    {
        baseScale = transform.localScale;
    }
    public void LocalMoveTo(Vector3 pos, Vector3 rotation)
    {
        transform.DOKill();
        transform.DOLocalRotate(rotation, moveDuration / 2);
        transform.DOLocalMove(pos, moveDuration * 0.5f).OnComplete(() => transform.DOLocalMove(pos, moveDuration * 0.5f)).OnComplete(() => onEndMove?.Invoke());
    }
}
