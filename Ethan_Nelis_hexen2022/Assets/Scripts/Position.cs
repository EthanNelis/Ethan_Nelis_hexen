using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Position
{
    public readonly int Q;

    public readonly int R;

    public readonly int S;


    public Position(int q, int r, int s)
    {
        Q = q;
        R = r;
        S = s;
    }

    public override string ToString()
    {
        return $"Q: {Q} R: {R} S: {S}";
    }
}
