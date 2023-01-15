using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    private Board _board = new Board(PositionHelper.BoardRadius);
    private Engine _engine;
    private BoardView _boardView;
    private Deck _deck;

    private GameStateMachine _gameStateMachine;
    private void OnEnable()
    {
        _gameStateMachine = new GameStateMachine();
        _gameStateMachine.Register("Menu", new MenuState());
        _gameStateMachine.Register("Playing", new PlayingState());
        _gameStateMachine.InitialState = MenuState.Name;    
    }
}