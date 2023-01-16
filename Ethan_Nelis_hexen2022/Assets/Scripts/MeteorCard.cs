using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorCard : CardMoveSet
{
    public MeteorCard (Board board): base(board)
    {
    }

    private List<Position> _positions = new List<Position>();

    public override List<Position> Positions(Position hoverPosition)
    {
        _positions.Clear();

        // Selects all the valid tiles neighboring the hoverPosition tile
        _positions.Add(hoverPosition);
        foreach (Vector3 direction in MoveSetHelper.Directions)
        {
            Position position = PositionHelper.Add(hoverPosition, PositionHelper.WorldToGridPosition(direction));
            if (Board.IsValidPosition(position))
            {
                _positions.Add(position);
            }
        }

        return _positions;
    }

    public override bool Execute(Position hoverPosition, Position playerPosition)
    {
        foreach(Position position in _positions)
        {
            if(Board.TryGetPieceAt(position, out PieceView piece) && piece.Type == PieceType.Enemy)
            {
                piece.Taken();
                Board.Take(position);
            }
        }
        return true;
    }
}
