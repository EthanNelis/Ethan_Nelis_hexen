using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PositionHelper
{
    public const int BoardRadius = 3;

    public const int TileSize = 1;

    public static Position WorldToGridPosition(Vector3 worldPosition)
    {
        worldPosition.z = -worldPosition.z;

        float q = ((Mathf.Sqrt(3) / 3 * worldPosition.x - 1f / 3 * worldPosition.z) / TileSize);

        float r = ((2f / 3 * worldPosition.z) / TileSize);

        float s = -q - r;

        return RoundCubeCoordinates(q, r, s);
    }

    private static Position RoundCubeCoordinates(float q, float r, float s)
    {
        int qInt = Mathf.RoundToInt(q);
        int rInt = Mathf.RoundToInt(r);
        int sInt = Mathf.RoundToInt(s);

        float qDifference = GetFractionalPart(q, qInt);
        float rDifference = GetFractionalPart(r, rInt);
        float sDifference = GetFractionalPart(s, sInt);
    
        if (qDifference > rDifference && qDifference > sDifference)
        {
            qInt = -rInt - sInt;
        }
        else if (rDifference > sDifference)
        {
            rInt = -qInt - sInt;
        }
        else
        {
            sInt = -qInt - rInt;
        }
        return new Position(qInt, rInt, sInt);
    }

    private static float GetFractionalPart(float floatValue, int intValue)
    {
        return Mathf.Abs(intValue - floatValue);
    }

    public static Vector3 GridToWorldPosition(Position gridPosition)
    {
        float xPosition = TileSize * (Mathf.Sqrt(3) * gridPosition.Q + Mathf.Sqrt(3) / 2 * gridPosition.R);

        float zPosition = -(TileSize * (3f / 2 * gridPosition.R));

        float yPositionDefault = 0f;

        return new Vector3(xPosition, yPositionDefault, zPosition);
    }
}
