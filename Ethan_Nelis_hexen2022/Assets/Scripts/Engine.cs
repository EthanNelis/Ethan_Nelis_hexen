using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Engine
{
    private readonly Board _board;

    public MoveSetCollection MoveSets { get; }

    public Engine(Board board)
    {
        _board = board;
        MoveSets = new MoveSetCollection(_board);
    }

    public bool PlayCard(CardMoveSet cardMoveSet, Position hoverPosition)
    {
        return cardMoveSet.Execute(hoverPosition, _board.PlayerPosition);
    }
}