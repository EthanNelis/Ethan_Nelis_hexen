using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SwipeCard : CardMoveSet
{
    public SwipeCard(Board board) : base(board)
    {
    }

    private List<Position> _positions = new List<Position>();

    private bool canExecute;

    public override List<Position> Positions(Position hoverPosition)
    {
        // Selects all the valid tiles neighboring the player tile
        foreach (Vector3 direction in MoveSetHelper.Directions)
        {
            Position position = PositionHelper.Add(Board.PlayerPosition, PositionHelper.WorldToGridPosition(direction));
            if (Board.IsValid(position))
            {
                _positions.Add(position);
            }
        }

        canExecute = true;

        // If the previously selected positions contain the hoverposition:
        // add the hoverposition and its neighbors to a new List which you return
        if (_positions.Contains(hoverPosition))
        {
            List<Position> filteredPositions = new List<Position>();
            filteredPositions.Add(hoverPosition);

            foreach (Position position in _positions)
            {
                if (PositionHelper.Distance(position, hoverPosition) == 1 || position.Equals(hoverPosition))
                {
                    filteredPositions.Add(position);
                }
            }

            _positions = filteredPositions;
            return _positions;
        }

        canExecute = false;

        return _positions;
    }

    public override bool Execute(Position hoverPosition, Position playerPosition)
    {
        if (canExecute)
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
