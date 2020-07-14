using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameController : MonoBehaviour
{
    [SerializeField] private FieldModel _fieldModel;
    [SerializeField] private FieldController _fieldController;
    [SerializeField] private TickModel _tickModel;
    [SerializeField] private TickController _tickController;
    [SerializeField] private ScoreModel _scoreModel;
    [SerializeField] private ScoreController _scoreController;
    [SerializeField] private GameView _gameView;
    [SerializeField] private StartView _startView;
    [SerializeField] private GameOverView _gameOverView;

    private void Start()
    {
        _fieldController.Initialize(_fieldModel);
        _scoreController.Inititalize(_fieldModel, _scoreModel);
        _gameView.Inititialize(_scoreModel, _fieldModel);
        _gameOverView.Initialize();
        _startView.Initialize();

        _tickController.Initialize(_tickModel, new List<ITickable>()
        {
            _fieldController,
            _scoreController
        });

        _gameView.Hide();
        _startView.Show();
        _gameOverView.Hide();

        _startView.PlayClicked += PlayClickedHandler;
        _gameOverView.RestartClicked += RestartClickedHandler;

        _scoreModel.GameOver += GameOverHandler;
    }

    private void GameOverHandler()
    {
        _tickController.Stop();

        _gameView.Hide();
        _gameOverView.Show();
    }

    private void RestartClickedHandler()
    {
        _gameOverView.Hide();
        _gameView.Show();

        _fieldController.Restart();
        _tickController.Play();
    }

    private void PlayClickedHandler()
    {
        _startView.Hide();
        _gameView.Show();
        _tickController.Play();
    }
}
