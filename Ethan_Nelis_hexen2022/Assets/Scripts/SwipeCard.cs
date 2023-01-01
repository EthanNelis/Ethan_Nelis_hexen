using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SwipeCard : CardMoveSet
{
    public SwipeCard(Board board) : base(board)
    {
    }

    public override List<Position> Positions(Position hoverPosition)
    {
        throw new NotImplementedException();
    }

    public override bool Execute(Position hoverPosition, Position playerPosition)
    {
        return base.Execute(hoverPosition, playerPosition);
    }
}
