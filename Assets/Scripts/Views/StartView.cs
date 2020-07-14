using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class StartView : BaseWindow
{
    [SerializeField] private Button _play;
    [SerializeField] private Button _settings;
    [SerializeField] private Button _exit;

    public event Action PlayClicked = delegate { };

    public void Initialize()
    {
        _play.onClick.AddListener(PlayClick);
    }

    private void PlayClick()
    {
        PlayClicked();
    }
}
