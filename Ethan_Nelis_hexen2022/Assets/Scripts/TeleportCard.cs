using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class TeleportCard : CardMoveSet
{
    public TeleportCard(Board board) : base(board)
    {
    }

    public override List<Position> Positions(Position hoverPosition)
    {
        List<Position> validPositions = new List<Position>();

        if(!Board.TryGetPieceAt(hoverPosition, out PieceView piece))
        {
            validPositions.Add(hoverPosition);
        }
        return validPositions;
    }

    public override bool Execute(Position hoverPosition, Position playerPosition)
    {
        return Board.Move(playerPosition, hoverPosition);
    }
}

