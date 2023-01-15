using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

internal class PlayingState : GameState
{
    public const string Name = "Playing";

    private BoardView _boardView;

    private Board _board;

    private Engine _engine;

    private Deck _deck;

    public override void OnEnter()
    {
        var loading = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        loading.completed += OnSceneLoaded;
    }

    public override void OnExit()
    {
        SceneManager.UnloadSceneAsync(1);
    }

    public PlayingState()
    {
        _board = new Board(PositionHelper.BoardRadius);

        _engine = new Engine(_board);
    }

    private void OnSceneLoaded(AsyncOperation obj)
    {
        _deck = GameObject.FindObjectOfType<Deck>();

        _boardView = GameObject.FindObjectOfType<BoardView>();

        _boardView.CardHoveredOverTile += CardHovered;
        _boardView.CardDroppedOnTile += CardDropped;
        _boardView.StopHoveredOverTile += StopHovered;

        var pieceViews = GameObject.FindObjectsOfType<PieceView>();
        foreach (PieceView pieceView in pieceViews)
        {
            _board.Place(pieceView, PositionHelper.WorldToGridPosition(pieceView.WorldPosition));
        }

        _board.PieceMoved += (s, e) => e.Piece.MoveTo(PositionHelper.GridToWorldPosition(e.ToPosition));

    }

    private void CardHovered(object sender, CardEventArgs cardEventArgs)
    {
        var validPositions = _engine.MoveSets.For(cardEventArgs.CardView.Type).Positions(cardEventArgs.Position);
        _boardView.SetActivePositions(validPositions);
    }

    private void CardDropped(object sender, CardEventArgs cardEventArgs)
    {
        var validPositions = _engine.MoveSets.For(cardEventArgs.CardView.Type).Positions(cardEventArgs.Position);

        if (validPositions.Count > 0)
        {
            if (_engine.PlayCard(_engine.MoveSets.For(cardEventArgs.CardView.Type), cardEventArgs.Position))
            {
                cardEventArgs.CardView.DestroyCard();
                _deck.DrawCard();
            }

            List<Position> emptyList = new List<Position>();
            _boardView.SetActivePositions(emptyList);
        }
    }

    private void StopHovered(object sender, EventArgs e)
    {
        List<Position> emptyList = new List<Position>();
        _boardView.SetActivePositions(emptyList);
    }

}
