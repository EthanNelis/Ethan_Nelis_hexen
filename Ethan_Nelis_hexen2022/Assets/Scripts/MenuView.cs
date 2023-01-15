using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

internal class MenuView : MonoBehaviour
{
    public event EventHandler StartClicked;

    [SerializeField]
    private Button _startButton;

    private void Start()
    {
        _startButton.onClick.AddListener(() => OnStartClicked(EventArgs.Empty));
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    protected virtual void OnStartClicked(EventArgs e)
    {
        var handler = StartClicked;
        handler?.Invoke(this, e);
    }
}
