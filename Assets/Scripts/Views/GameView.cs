using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameView : BaseWindow
{
    [SerializeField] private Text _score;
    [SerializeField] private Transform _figure;

    private ScoreModel _scoreModel;
    private FieldModel _fieldModel;
    private Image[] _figureImages;

    public void Inititialize(ScoreModel scoreModel, FieldModel fieldModel)
    {
        _scoreModel = scoreModel;
        _fieldModel = fieldModel;

        _scoreModel.ScoreChanged += UpdateScore;
        _fieldModel.NextShapeChanged += UpdateShape;

        _figureImages = _figure.GetComponentsInChildren<Image>();

        UpdateScore();
        UpdateShape();
    }

    private void UpdateShape()
    {
        for (int y = 0; y < 2; y++) 
        {
            for (int x = 0; x < 4; x++)
            {
                _figureImages[x + y * 4].enabled = _fieldModel.NextShape[y, x] != 0;

                if (_figureImages[x + y * 4].enabled)
                {
                    _figureImages[x + y * 4].color = _fieldModel.ShapeColors[_fieldModel.NextShape[y, x] - 1];
                }
            }
        }
    }

    private void UpdateScore()
    {
        _score.text = "Score: " + _scoreModel.Score;
    }
}
