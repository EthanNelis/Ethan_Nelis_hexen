using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Engine
{
    private readonly Board _board;

    public Engine(Board board)
    {
        _board = board;
    }

    public bool Move(Position fromPosition, Position toPosition)
    {
        return false;
    }

}

