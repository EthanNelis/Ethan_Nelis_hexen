using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PositionHelper
{
    public const int BoardRadius = 3;

    public const int TileSize = 1;

    public static Position WorldToGridPosition(Vector3 worldPosition)
    {
        int q = Mathf.RoundToInt((Mathf.Sqrt(3) / 3f * worldPosition.x - 1f / 3f * worldPosition.z) / TileSize);

        int r = Mathf.RoundToInt((2f / 3f * worldPosition.z) / TileSize);

        int s = -q - r;

        return new Position(q, r, s);
    }

    public static Vector3 GridToWorldPosition(Position gridPosition)
    {
        float xPosition = TileSize * (Mathf.Sqrt(3) * gridPosition.Q + Mathf.Sqrt(3) / 2f * gridPosition.R);

        float zPosition = TileSize * (3f / 2f * gridPosition.R);

        float yPositionDefault = 0f;

        return new Vector3(xPosition, yPositionDefault, zPosition);
    }

    public static Position Subtract(Position position1, Position position2)
        => new Position(position1.Q - position2.Q, position1.R - position2.R, position1.S - position2.S);

    public static Position Add(Position position1, Position position2)
        => new Position(position1.Q + position2.Q, position1.R + position2.R, position1.S + position2.S);

    public static Position GetDirection(Position position1, Position position2)
    {
        Position direction = Subtract(position1,position2);

        int Q = direction.Q;
        if(Q != 0)
        {
            Q /= Mathf.Abs(direction.Q);
        }

        int R = direction.R;
        if (R != 0)
        {
            R /= Mathf.Abs(direction.R);
        }

        int S = direction.S;
        if (S != 0)
        {
            S /= Mathf.Abs(direction.S);
        }

        return new Position(Q, R, S);
    }

    public static int Distance(Position position1, Position position2)
    {
        Position difference = Subtract(position1, position2);

        return (Mathf.Abs(difference.Q) + Mathf.Abs(difference.R) + Mathf.Abs(difference.S)) / 2;
    }
}
