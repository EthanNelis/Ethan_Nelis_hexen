using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SlashCard : CardMoveSet
{
    public SlashCard(Board board) : base(board)
    {
    }

    private List<Position> _positions = new List<Position>();

    private bool _canExecute;

    public override List<Position> Positions(Position hoverPosition)
    {
        // Selects all valid tiles in straight lines from the player position (not included)
        foreach(Vector3 direction in MoveSetHelper.Directions)
        {
            Position position = PositionHelper.Add(Board.PlayerPosition, PositionHelper.WorldToGridPosition(direction));
            while (Board.IsValid(position))
            {
                _positions.Add(position);

                position = PositionHelper.Add(position, PositionHelper.WorldToGridPosition(direction));
            }
        }

        _canExecute = true;

        // If one of the previously selected tiles contains the hoverposition:
        // creates a straight line from player position (not included) passing through hover position
        foreach(Position position in _positions)
        {
            if (hoverPosition.Equals(position))
            {
                _positions.Clear();

                // same value means on same line in that axis
                if(hoverPosition.Q == Board.PlayerPosition.Q 
                    || hoverPosition.R == Board.PlayerPosition.R 
                    || hoverPosition.S == Board.PlayerPosition.S)
                {
                    Position direction = PositionHelper.GetDirection(hoverPosition, Board.PlayerPosition);

                    Position pos = PositionHelper.Add(Board.PlayerPosition, direction);
                    while (Board.IsValid(pos))
                    {
                        _positions.Add(pos);

                        pos = PositionHelper.Add(pos, direction);
                    }
                    return _positions;
                }
            }
        }

        _canExecute = false;

        return _positions;
    }

    public override bool Execute(Position hoverPosition, Position playerPosition)
    {
        if (_canExecute)
        {
            foreach (Position position in _positions)
            {
                if (Board.TryGetPieceAt(position, out PieceView piece))
                {
                    piece.Taken();
                    Board.Take(position);
                }
            }
        }
        return true;

    }
}

