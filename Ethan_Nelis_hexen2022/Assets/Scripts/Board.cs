using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceMovedEventArgs : EventArgs
{
    public PieceMovedEventArgs(PieceView piece, Position fromPosition, Position toPosition)
    {
        Piece = piece;
        FromPosition = fromPosition;
        ToPosition = toPosition;
    }

    public PieceView Piece { get; }
    public Position FromPosition { get; }
    public Position ToPosition { get; }
}

public class PieceTakenEventArgs : EventArgs
{
    public PieceTakenEventArgs(PieceView piece, Position fromPosition)
    {
        Piece = piece;
        FromPosition = fromPosition;
    }

    public PieceView Piece { get; }
    public Position FromPosition { get; }
}

public class PiecePlacedEventArgs : EventArgs 
{
    public PiecePlacedEventArgs(PieceView piece, Position toPosition)
    {
        Piece = piece;
        ToPosition = toPosition;
    }

    public PieceView Piece { get; }
    public Position ToPosition { get; }
}

public class Board
{
    public Board()
    {

    }

    private Dictionary<Position, PieceView> _pieces = new Dictionary<Position, PieceView>();

    public event EventHandler<PieceMovedEventArgs> PieceMoved;
    public event EventHandler<PieceTakenEventArgs> PieceTaken;
    public event EventHandler<PiecePlacedEventArgs> PiecePlaced;

    
    public bool TryGetPieceAt(Position position, out PieceView piece)
    => _pieces.TryGetValue(position, out piece);

    public bool IsValid(Position position)
    => position.Q + position.R + position.S == 0;

    public bool Move(Position fromPosition, Position toPosition)
    {
        if (!IsValid(toPosition))
        {
            return false;
        }

        if (_pieces.ContainsKey(toPosition))
        {
            return false;
        }

        if(!_pieces.TryGetValue(fromPosition, out PieceView piece))
        {
            return false;
        }

        _pieces.Remove(fromPosition);
        _pieces[toPosition] = piece;


        OnPieceMoved(new PieceMovedEventArgs(piece, fromPosition, toPosition));

        return true;
    }

    public bool Take(Position fromPosition)
    {
        if (!IsValid(fromPosition))
        {
            return false;
        }

        if (!_pieces.ContainsKey(fromPosition))
        {
            return false;
        }

        if(!_pieces.TryGetValue(fromPosition, out PieceView piece))
        {
            return false;
        }

        _pieces.Remove(fromPosition);

        OnPieceTaken(new PieceTakenEventArgs(piece, fromPosition));

        return true;

    }

    public bool Place(Position position, PieceView piece)
    {
        if(piece == null)
        {
            return false;
        }

        if (!IsValid(position))
        {
            return false;
        }

        if (_pieces.ContainsKey(position))
        {
            return false;
        }

        if (_pieces.ContainsValue(piece))
        {
            return false;
        }

        _pieces[position] = piece;

        OnPiecePlaced(new PiecePlacedEventArgs(piece, position));

        return true;
    }

    protected virtual void OnPieceMoved(PieceMovedEventArgs e)
    {
        var handler = PieceMoved;
        handler?.Invoke(this, e);
    }

    protected virtual void OnPieceTaken(PieceTakenEventArgs e)
    {
        var handler = PieceTaken;
        handler?.Invoke(this, e);
    }

    protected virtual void OnPiecePlaced(PiecePlacedEventArgs e)
    {
        var handler = PiecePlaced;
        handler?.Invoke(this, e);
    }

    
}
