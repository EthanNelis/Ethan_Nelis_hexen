using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    private Board _board = new Board(PositionHelper.BoardRadius);
    private Engine _engine;
    private BoardView _boardView;
    private Deck _deck;

    private void OnEnable()
    {
        _engine = new Engine(_board);

        _deck = FindObjectOfType<Deck>();
        _boardView = FindObjectOfType<BoardView>();

        _boardView.CardHoveredOverTile += CardHovered;
        _boardView.CardDroppedOnTile += CardDropped;
        _boardView.StopHoveredOverTile += StopHovered;

        var pieceViews = FindObjectsOfType<PieceView>();
        foreach (PieceView pieceView in pieceViews)
        {
            _board.Place(pieceView, PositionHelper.WorldToGridPosition(pieceView.WorldPosition));
        }

        _board.PieceMoved += (s, e) => e.Piece.MoveTo(PositionHelper.GridToWorldPosition(e.ToPosition));
    }

    private void CardHovered(object sender, CardEventArgs cardEventArgs)
    {
        var validPositions = _engine.MoveSets.For(cardEventArgs.CardView.Type).Positions(cardEventArgs.Position);
        _boardView.SetActivePositions(validPositions);
    }

    private void CardDropped(object sender, CardEventArgs cardEventArgs)
    {
        var validPositions = _engine.MoveSets.For(cardEventArgs.CardView.Type).Positions(cardEventArgs.Position);

        if (validPositions.Count > 0)
        {
            if (_engine.PlayCard(_engine.MoveSets.For(cardEventArgs.CardView.Type), cardEventArgs.Position))
            {
                cardEventArgs.CardView.DestroyCard();
                _deck.DrawCard();
            }

            List<Position> emptyList = new List<Position>();
            _boardView.SetActivePositions(emptyList);
        }
    }

    private void StopHovered(object sender, EventArgs e)
    {
        List<Position> emptyList = new List<Position>();
        _boardView.SetActivePositions(emptyList);
    }

}