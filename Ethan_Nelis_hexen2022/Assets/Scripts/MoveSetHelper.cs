using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class MoveSetHelper
{
    public static Vector3[] Directions 
        = new Vector3[] { Right(), Left(), ForwardRight(), ForwardLeft(), BackwardRight(), BackwardLeft() };
  
    public static Vector3 Right() => new Vector3(1, 0, -1);
    public static Vector3 Left() => new Vector3(-1, 0, 1);
    public static Vector3 ForwardRight() => new Vector3(1, -1, 0);
    public static Vector3 ForwardLeft() => new Vector3(0, -1, 1);
    public static Vector3 BackwardRight() => new Vector3(0, 1, -1);
    public static Vector3 BackwardLeft() => new Vector3(-1, 1, 0);
}