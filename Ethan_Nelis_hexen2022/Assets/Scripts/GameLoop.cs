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
    { } // Debug.Log($"{cardEventArgs.CardType} hovered over position {cardEventArgs.Position}");


    private void CardDropped(object sender, CardEventArgs cardEventArgs)
    {
        if (_board.TryGetPieceAt(cardEventArgs.Position, out var piece))
        {
            Debug.Log($"Position contains {piece.Name}");

            var toPosition = new Position(cardEventArgs.Position.Q , cardEventArgs.Position.R + 1, cardEventArgs.Position.S - 1);
            _board.Move(cardEventArgs.Position, toPosition);
        }
    }

}
