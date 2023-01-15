using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal abstract class GameState
{
    public GameStateMachine StateMachine { get; internal set; }

    public virtual void OnEnter() { }

    public virtual void OnExit() { }
}

