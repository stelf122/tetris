using UnityEngine;
using System.Collections;
using System;

public class ScoreController : MonoBehaviour, ITickable
{
    private FieldModel _fieldModel;
    private ScoreModel _scoreModel;

    public void Inititalize(FieldModel fieldModel, ScoreModel scoreModel)
    {
        _fieldModel = fieldModel;
        _scoreModel = scoreModel;

        _scoreModel.Score = 0;
    }

    public void Tick()
    {
        for (int y = 1; y < _fieldModel.SizeY; y++)
        {
            for (int x = 1; x < _fieldModel.SizeX - 1; x++)
            {
                if (_fieldModel.Field[x, y] != 0 && _fieldModel.Shape[x, y] != 0)
                {
                    _scoreModel.ClearScore();
                    return;
                }
            }
        }

        for (int y = 1; y < _fieldModel.SizeY; y++)
        {
            bool fullLine = true;

            for (int x = 1; x < _fieldModel.SizeX - 1; x++)
            {
                if (_fieldModel.Field[x, y] == 0)
                {
                    fullLine = false;
                }
            }

            if (fullLine)
            {
                _scoreModel.AddScore(1);
                _fieldModel.ClearLine(y);
            }
        }
    }
}
