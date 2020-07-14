using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class GameOverView : BaseWindow
{
    [SerializeField] private Button _restart;
    [SerializeField] private Button _menu;
    [SerializeField] private Button _exit;

    public event Action RestartClicked = delegate { };
    public event Action MenuClicked = delegate { };

    public void Initialize()
    {
        _restart.onClick.AddListener(RestartClick);
        _menu.onClick.AddListener(MenuClick);
    }

    private void MenuClick()
    {
        MenuClicked();
    }

    private void RestartClick()
    {
        RestartClicked();
    }
}
