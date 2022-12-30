using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


internal abstract class MoveSet
{
    protected Board Board { get; }

    protected MoveSet(Board board)
    {
        Board = board;
    }

    public abstract List<Position> Positions(Position fromPosition);

    internal virtual bool Execute(Position fromPosition, Position toPosition)
    {
        var pieceTaken = Board.TryGetPieceAt(toPosition, out var toPiece);

        Board.Take(toPosition);

        return Board.Move(fromPosition, toPosition);
    }
}

