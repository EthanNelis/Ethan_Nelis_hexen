using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PositionView : MonoBehaviour , IPointerClickHandler
{
    public Position GridPosition => PositionHelper.WorldToGridPosition(transform.position);

    private BoardView _parent;

    private void Start()
    {
        _parent = GetComponentInParent<BoardView>();
    }


    public void OnPointerClick(PointerEventData pointerEventData)
    => _parent.ChildClicked(this);
}
