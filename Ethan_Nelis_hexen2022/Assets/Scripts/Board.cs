using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PiecePlacedEventArgs
{
    public PieceView Piece { get; }

    public Position ToPosition { get; }

    public PiecePlacedEventArgs(PieceView piece, Position toPosition)
    {
        Piece = piece;
        ToPosition = toPosition;
    }
}

public class PieceMovedEventArgs
{
    public PieceView Piece { get; }

    public Position FromPosition { get; }

    public Position ToPosition { get; }

    public PieceMovedEventArgs(PieceView piece, Position fromPosition, Position toPosition)
    {
        Piece = piece;
        FromPosition = fromPosition;
        ToPosition = toPosition;
    }
}

public class PieceTakenEventArgs
{
    public PieceView Piece { get; }

    public Position FromPosition { get; }

    public PieceTakenEventArgs(PieceView piece, Position fromPosition)
    {
        Piece = piece;
        FromPosition = fromPosition;
    }
}

public class Board
{
    private readonly int _radius;

    private Dictionary<Position, PieceView> _pieces = new Dictionary<Position, PieceView>();

    private Position _playerPosition = new Position(0, 0, 0);

    public Position PlayerPosition => _playerPosition;


    public event EventHandler<PieceMovedEventArgs> PieceMoved;
    public event EventHandler<PieceTakenEventArgs> PieceTaken;
    public event EventHandler<PiecePlacedEventArgs> PiecePlaced;


    public Board(int radius)
    {
        _radius = radius;
    }


    public bool TryGetPieceAt(Position position, out PieceView piece)
    => _pieces.TryGetValue(position, out piece);

    internal bool IsValidPosition(Position position)
    => (position.Q <= _radius && position.Q >= -_radius)
    && (position.R <= _radius && position.R >= -_radius)
    && (position.S <= _radius && position.S >= -_radius);


    public bool Place(PieceView piece, Position toPosition)
    {
        if (!IsValidPosition(toPosition))
            return false;

        if (_pieces.ContainsKey(toPosition))
            return false;

        if (_pieces.ContainsValue(piece))
            return false;

        _pieces[toPosition] = piece;

        OnPiecePlaced(new PiecePlacedEventArgs(piece, toPosition));

        return true;
    }

    public bool Move(Position fromPosition, Position toPosition)
    {
        if (!IsValidPosition(toPosition))
            return false;

        if (!_pieces.TryGetValue(fromPosition, out var piece))
            return false;

        if (_pieces.ContainsKey(toPosition))
            return false;

        if (!_pieces.Remove(fromPosition))
            return false;

        _pieces[toPosition] = piece;

        if (piece.Type == PieceType.Player)
        {
            _playerPosition = toPosition;
        }

        OnPieceMoved(new PieceMovedEventArgs(piece, fromPosition, toPosition));

        return true;
    }

    public bool Take(Position fromPosition)
    {
        if (!_pieces.TryGetValue(fromPosition, out var piece))
            return false;

        if (!_pieces.Remove(fromPosition))
            return false;

        OnPieceTaken(new PieceTakenEventArgs(piece, fromPosition));

        return true;
    }

    protected virtual void OnPieceMoved(PieceMovedEventArgs e)
    {
        var handler = PieceMoved;
        handler?.Invoke(this,e);
    }

    protected virtual void OnPiecePlaced(PiecePlacedEventArgs e)
    {
        var handler = PiecePlaced;
        handler?.Invoke(this, e);
    }

    protected virtual void OnPieceTaken(PieceTakenEventArgs e)
    {
        var handler = PieceTaken;
        handler?.Invoke(this, e);
    }

}

