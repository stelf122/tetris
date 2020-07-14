using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldController : MonoBehaviour, ITickable
{
    private FieldModel _fieldModel;
    private List<GameObject> _shapeObjects;
    private List<GameObject> _fieldObjects;

    public void Initialize(FieldModel fieldModel)
    {
        _fieldModel = fieldModel;

        _fieldModel.CreateField();
        _fieldModel.FillBorders();

        CreateField();
        CreateShapeObjects();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _fieldModel.MoveShapeLeft();

            UpdateShapePosition();
        }
            
        if (Input.GetKeyDown(KeyCode.D))
        {
            _fieldModel.MoveShapeRight();

            UpdateShapePosition();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            _fieldModel.MoveShapeDown();

            UpdateShapePosition();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            _fieldModel.RotateShape();

            UpdateShapePosition();
        }
    }

    private void ClearField()
    {
        foreach (var go in _fieldObjects)
        {
            Destroy(go);
        }
    }

    private void CreateField()
    {
        _fieldObjects = new List<GameObject>();

        for (int x = 0; x < _fieldModel.SizeX; x++)
        {
            for (int y = 0; y < _fieldModel.SizeY; y++)
            {
                if (_fieldModel.Field[x, y] == 1)
                {
                    GameObject box = Instantiate(_fieldModel.Box);

                    box.transform.position = new Vector2(x, y);

                    _fieldObjects.Add(box);
                }
            }
        }
    }

    public void Tick()
    {
        if (_fieldModel.Shape == null)
        {
            _fieldModel.CreateShape();
        }
        else
        {
            MoveShapeDown();
        }

        UpdateShapePosition();
    }

    private void MoveShapeDown()
    {
        if (!_fieldModel.MoveShapeDown())
        {
            _fieldModel.AddShapeToField();
            _fieldModel.CreateShape();

            ClearField();
            CreateField();
        }
    }

    private void UpdateShapePosition()
    {
        int objIndex = 0;

        for (int x = 0; x < _fieldModel.SizeX; x++)
        {
            for (int y = 0; y < _fieldModel.SizeY; y++)
            {
                if (_fieldModel.Shape[x, y] == 1)
                {
                    _shapeObjects[objIndex].transform.position = new Vector2(x, y);
                    _shapeObjects[objIndex].SetActive(true);

                    objIndex++;
                }
            }
        }

        for (int i = objIndex; i < _shapeObjects.Count; i++)
        {
            _shapeObjects[i].SetActive(false);
        }
    }

    private void CreateShapeObjects()
    {
        _shapeObjects = new List<GameObject>();

        for (int i = 0; i < 10; i++)
        {
            GameObject box = Instantiate(_fieldModel.Box);

            _shapeObjects.Add(box);

            box.SetActive(false);
        }
    }
}
