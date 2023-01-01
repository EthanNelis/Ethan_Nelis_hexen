using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class PushbackCard : CardMoveSet
{
    public PushbackCard(Board board) : base(board)
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

