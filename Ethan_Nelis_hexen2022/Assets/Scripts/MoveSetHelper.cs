using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class MoveSetHelper
{
    private List<Position> _validPositions = new List<Position>();

    public Position AddPositions(Position position, Position direction)
    {
        return new Position(position.Q + direction.Q, position.R + direction.R, position.S + direction.S);
    }

    public bool IsNeighbor(Position position1, Position position2)
    {
        return false;
    }

    public Position Right() => new Position(1, 0, -1);
    public Position Left() => new Position(-1, 0, 1);
    public Position ForwardRight() => new Position(1, -1, 0);
    public Position ForwardLeft() => new Position(0, -1, 1);
    public Position BackwardRight() => new Position(0, 1, -1);
    public Position BackwardLeft() => new Position(-1, 1, 0);


}