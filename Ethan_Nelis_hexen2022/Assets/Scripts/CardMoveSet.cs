using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public abstract class CardMoveSet
{
    protected Board Board { get; }

    protected CardMoveSet(Board board)
    {
        Board = board;
    }

    public abstract List<Position> Positions(Position hoverPosition);

    public virtual bool Execute(Position hoverPosition, Position playerPosition)
    {
        return false;
    }
}