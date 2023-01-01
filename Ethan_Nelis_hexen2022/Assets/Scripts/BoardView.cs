using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardEventArgs : EventArgs
{
    public Position Position { get; }
    public CardType CardType { get; }

    public CardEventArgs(Position position, CardType cardType)
    {
        Position = position;

        CardType = cardType;
    }
}

public class BoardView : MonoBehaviour
{
    public event EventHandler<CardEventArgs> CardDroppedOnTile;
    public event EventHandler<CardEventArgs> CardHoveredOverTile;
    public event EventHandler StopHoveredOverTile;

    // public event EventHandler<CardEventArgs> CardDropped;


    // public event EventHandler<CardEventArgs> PositionSelected;

    //internal void SetActivePositions()
    //{
    //    throw new NotImplementedException();
    //}


    private readonly Dictionary<Position, TileView> _positions = new Dictionary<Position, TileView>();
    private List<Position> _activatedPositions = new List<Position>();


    private void OnEnable()
    {
        var tileViews = GetComponentsInChildren<TileView>();
        foreach (TileView tileView in tileViews)
        {
            _positions[PositionHelper.WorldToGridPosition(tileView.WorldPosition)] = tileView;
            tileView.CardDropped += OnCardDroppedOnTileView;
            tileView.CardHovered += OnCardHoveredOverTileView;
            tileView.StopHovered += OnStopHoveredOverTileView;
        }
    }



    public void SetActivePositions(List<Position> positions)
    {
        foreach(var position in _activatedPositions)
        {
            _positions[position].Deactivate();
        }

        _activatedPositions = positions;

        foreach (var position in positions)
        {
            _positions[position].Activate();
        }
    }


    private void OnCardDroppedOnTileView(object sender, PointerEventData eventData)
    {
        if (sender is TileView tileView)
        {
            var position = PositionHelper.WorldToGridPosition(tileView.WorldPosition);
            CardType cardType = eventData.pointerDrag.GetComponent<CardView>().Type;
            
            OnCardDroppedOnTile(new CardEventArgs(position, cardType));
        }
    }

    private void OnCardHoveredOverTileView(object sender, PointerEventData eventData)
    {
        if (sender is TileView tileView)
        {
            var position = PositionHelper.WorldToGridPosition(tileView.WorldPosition);
            CardType cardType = eventData.pointerDrag.GetComponent<CardView>().Type;

            OnCardHoveredOverTile(new CardEventArgs(position, cardType));
        }
    }

    private void OnStopHoveredOverTileView(object sender, EventArgs e)
    {
        if (sender is TileView tileView)
        {
            OnStopHoveredOverTile(EventArgs.Empty);
        }
    }



    protected virtual void OnCardDroppedOnTile(CardEventArgs cardEventArgs)
    {
        var handler = CardDroppedOnTile;
        handler?.Invoke(this, cardEventArgs);
    }

    protected virtual void OnCardHoveredOverTile(CardEventArgs cardEventArgs)
    {
        var handler = CardHoveredOverTile;
        handler?.Invoke(this, cardEventArgs);
    }

    private void OnStopHoveredOverTile(EventArgs e)
    {
        var handler = StopHoveredOverTile;
        handler?.Invoke(this, e);
    }

}

