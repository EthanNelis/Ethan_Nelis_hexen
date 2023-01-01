using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TileView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{
    public event EventHandler<PointerEventData> CardDropped;
    
    public event EventHandler<PointerEventData> CardHovered;

    public event EventHandler StopHovered;

    public Vector3 WorldPosition => transform.position;

    [SerializeField]
    private UnityEvent OnActivation;

    [SerializeField]
    private UnityEvent OnDeactivation;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            OnCardHovered(eventData);
        }
    }

    public void OnDrop(PointerEventData eventData)
    => OnCardDropped(eventData);

    public void OnPointerExit(PointerEventData eventData)
    => OnStopHovered(EventArgs.Empty);

    protected virtual void OnCardHovered(PointerEventData eventData)
    {
        var handler = CardHovered;
        CardHovered?.Invoke(this, eventData);
    }

    protected virtual void OnCardDropped(PointerEventData eventData)
    {
        var handler = CardDropped;
        CardDropped?.Invoke(this, eventData);
    }

    protected virtual void OnStopHovered(EventArgs e)
    {
        var handler = StopHovered;
        handler?.Invoke(this, e);
    }

    internal void Activate()
        => OnActivation?.Invoke();

    internal void Deactivate()
        => OnDeactivation?.Invoke();
}
