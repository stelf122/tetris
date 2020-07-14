using UnityEngine;
using System.Collections;
using System;

[CreateAssetMenu(menuName = "Scriptables/ScoreModel")]
public class ScoreModel : ScriptableObject
{
    [SerializeField] private int _score;

    public int Score { get => _score; set => _score = value; }

    public event Action ScoreChanged = delegate { };
    public event Action GameOver = delegate { };

    public void AddScore(int value)
    {
        _score += value;

        ScoreChanged();
    }

    public void ClearScore()
    {
        _score = 0;

        ScoreChanged();

        GameOver();
    }
}
