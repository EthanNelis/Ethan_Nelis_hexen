using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    private Board _board = new Board(PositionHelper.BoardRadius);
    private Engine _engine;
    private BoardView _boardView;

    private void OnEnable()
    {
        _engine = new Engine(_board);

        _boardView = FindObjectOfType<BoardView>();
        _boardView.CardDroppedOnTile += CardDropped;
        _boardView.CardHoveredOverTile += CardHovered;
        _boardView.StopHoveredOverTile += StopHovered;

        var pieceViews = FindObjectsOfType<PieceView>();
        foreach (PieceView pieceView in pieceViews)
        {
            _board.Place(pieceView, PositionHelper.WorldToGridPosition(pieceView.WorldPosition));
        }

        _board.PieceMoved += (s, e) => e.Piece.MoveTo(PositionHelper.GridToWorldPosition(e.ToPosition));
    }

    private void StopHovered(object sender, EventArgs e)
    {
        List<Position> emptyList = new List<Position>();
        _boardView.SetActivePositions(emptyList);
    }

    private void CardHovered(object sender, CardEventArgs cardEventArgs)
    {
        var validPositions = _engine.MoveSets.MoveSet(cardEventArgs.CardType).Positions(cardEventArgs.Position);
        _boardView.SetActivePositions(validPositions);
    }


    private void CardDropped(object sender, CardEventArgs cardEventArgs)
    {
        var validPositions = _engine.MoveSets.MoveSet(cardEventArgs.CardType).Positions(cardEventArgs.Position);

        if(validPositions.Count > 0)
        {
            _engine.PlayCard(_engine.MoveSets.MoveSet(cardEventArgs.CardType), cardEventArgs.Position);

            List<Position> emptyList = new List<Position>();
            _boardView.SetActivePositions(emptyList);
        }  
    }

}