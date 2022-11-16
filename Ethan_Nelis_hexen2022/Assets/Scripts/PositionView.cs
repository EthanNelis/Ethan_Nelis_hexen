using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PositionView : MonoBehaviour , IPointerClickHandler
{
    public Position Position => PositionHelper.WorldToCubePosition(transform.position);

    public event EventHandler Clicked;

    protected virtual void OnClicked(EventArgs e)
    {
        var handler = Clicked;
        handler?.Invoke(this, e);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClicked(EventArgs.Empty);
        Debug.Log(Position);
    }
}
