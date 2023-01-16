using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

internal class MenuState : GameState
{
    public const string Name = "Menu";

    private MenuView _menuView;

    public override void OnEnter()
    {
        var loading = SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
        loading.completed += OnSceneLoaded;
    }

    public override void OnExit()
    {
        _menuView.Hide();
        _menuView.StartClicked -= (s, e) => StateMachine.MoveTo(PlayingState.Name);
        SceneManager.UnloadSceneAsync(2);
    }

    private void OnSceneLoaded(AsyncOperation obj)
    {
        _menuView = GameObject.FindObjectOfType<MenuView>(true);
        if (_menuView != null)
        {
            _menuView.StartClicked += (s, e) => StateMachine.MoveTo(PlayingState.Name);
            _menuView.Show();
        }
    }


}
