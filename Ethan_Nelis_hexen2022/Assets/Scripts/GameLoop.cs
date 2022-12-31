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

        var boardView = FindObjectOfType<BoardView>();
        boardView.CardDroppedOnTile += CardDropped;
        boardView.CardHoveredOverTile += CardHovered;

        var pieceViews = FindObjectsOfType<PieceView>();
        foreach (PieceView pieceView in pieceViews)
        {
            _board.Place(pieceView, PositionHelper.WorldToGridPosition(pieceView.WorldPosition));
        }

        _board.PieceMoved += (s, e) => e.Piece.MoveTo(PositionHelper.GridToWorldPosition(e.ToPosition));
    }

    private void CardHovered(object sender, CardEventArgs cardEventArgs)
    { } // Highlight logic to be added here


    private void CardDropped(object sender, CardEventArgs cardEventArgs)
    {
        var validPositions = _engine.MoveSets.MoveSet(cardEventArgs.CardType).Positions(cardEventArgs.Position);

        if(validPositions.Count > 0)
        {
            _engine.PlayCard(_engine.MoveSets.MoveSet(cardEventArgs.CardType), cardEventArgs.Position);
            Debug.Log(cardEventArgs.CardType + "played at position : " + cardEventArgs.Position);
        }  
    }

}