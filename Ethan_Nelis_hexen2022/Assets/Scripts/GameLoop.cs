using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    private Board _board;

    private void Start()
    {
        _board = new Board();
        _board.PieceMoved += (s, e) => e.Piece.MoveTo(PositionHelper.GridToWorldPosition(e.ToPosition));
        _board.PieceTaken += (s, e) => e.Piece.Taken();
        _board.PiecePlaced += (s, e) => e.Piece.Placed(PositionHelper.GridToWorldPosition(e.ToPosition));

        PieceView[] pieceViews = FindObjectsOfType<PieceView>();

        foreach(PieceView pieceView in pieceViews)
        {
            _board.Place(PositionHelper.WorldToGridPosition(pieceView.WorldPosition), pieceView);
        }

        BoardView boardView = FindObjectOfType<BoardView>();
        boardView.PositionClicked += OnPositionClicked;
    }

    private void OnPositionClicked(object sender, PositionEventArgs e)
    {
        if(_board.TryGetPieceAt(e.Position, out var piece))
        {
            // TODO : Adjust code to be able to move to correct positions
            Position toPosition = new Position(e.Position.Q, e.Position.R, e.Position.S);
            _board.Move(e.Position, toPosition);
        }
    }
}
