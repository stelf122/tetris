using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "Scriptables/FieldModel")]
public class FieldModel : ScriptableObject
{
    [SerializeField] private int _sizeX;
    [SerializeField] private int _sizeY;
    [SerializeField] private GameObject _box;

    private int[,] _field;
    private int[,] _shape;

    private int[][,] _shapes = new int[][,] 
    { 
        new int[2, 4] { { 0, 1, 1, 0 },
                        { 0, 1, 1, 0 } },
        new int[2, 4] { { 0, 0, 0, 0 },
                        { 1, 1, 1, 1 } },
        new int[2, 4] { { 1, 0, 0, 0 },
                        { 1, 1, 1, 0 } },
        new int[2, 4] { { 0, 0, 1, 0 },
                        { 1, 1, 1, 0 } },
        new int[2, 4] { { 0, 1, 1, 0 },
                        { 1, 1, 0, 0 } },
        new int[2, 4] { { 1, 1, 0, 0 },
                        { 0, 1, 1, 0 } },
        new int[2, 4] { { 0, 1, 0, 0 },
                        { 1, 1, 1, 0 } }
    };

    public float SizeX { get => _sizeX; }
    public float SizeY { get => _sizeY; }
    public GameObject Box { get => _box; }
    public int[,] Field { get => _field; }
    public int[,] Shape { get => _shape; }

    public void CreateField()
    {
        _field = new int[_sizeX, _sizeY];
    }

    public void FillBorders()
    {
        for (int y = 0; y < _sizeY; y++)
        {
            _field[0, y] = 1;
            _field[_sizeX - 1, y] = 1;
        }

        for (int x = 1; x < _sizeX; x++)
        {
            _field[x, 0] = 1;
        }
    }

    public void CreateShape()
    {
        _shape = new int[_sizeX, _sizeY];

        int[,] randomShape = _shapes[Random.Range(0, _shapes.Length)];

        int xOffset = _sizeX / 2 - 2;

        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 2; y++)
            {
                if (randomShape[y, x] != 0)
                {
                    _shape[xOffset + x, _sizeY - 2 + y] = randomShape[y, x];
                }
            }
        }
    }

    public void AddShapeToField()
    {
        for (int x = 0; x < _sizeX; x++)
        {
            for (int y = 0; y < _sizeY - 1; y++)
            {
                if (_shape[x, y] != 0)
                {
                    _field[x, y] = 1;
                }
            }
        }
    }

    public bool MoveShapeDown()
    {
        if (!CanShapeMoveDown())
            return false;

        for (int x = 0; x < _sizeX; x++)
        {
            for (int y = 0; y < _sizeY - 1; y++)
            {
                if (_shape[x, y + 1] != 0)
                {
                    _shape[x, y] = _shape[x, y + 1];
                    _shape[x, y + 1] = 0;
                }
            }
        }

        return true;
    }

    private bool CanShapeMoveDown()
    {
        for (int x = 0; x < _sizeX; x++)
        {
            for (int y = 0; y < _sizeY - 1; y++)
            {
                if (_shape[x, y + 1] != 0)
                {
                    if (_field[x, y] == 1)
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }

    public void MoveShapeRight()
    {
        for (int x = _sizeX; x > 0; x--)
        {
            for (int y = 0; y < _sizeY; y++)
            {
                if (_shape[x - 1, y] != 0)
                {
                    if (_field[x, y] == 1)
                    {
                        return;
                    }
                    else
                    {
                        _shape[x, y] = _shape[x - 1, y];
                        _shape[x - 1, y] = 0;
                    }
                }
            }
        }
    }

    public void MoveShapeLeft()
    {
        for (int x = 0; x < _sizeX - 1; x++)
        {
            for (int y = 0; y < _sizeY; y++)
            {
                if (_shape[x + 1, y] != 0)
                {
                    if (_field[x, y] == 1)
                    {
                        return;
                    }
                    else
                    {
                        _shape[x, y] = _shape[x + 1, y];
                        _shape[x + 1, y] = 0;
                    }
                }
            }
        }
    }

    public void RotateShape()
    {
        int startX, startY;

        FindStartingPoint(out startX, out startY);

        Debug.Log(startX + " " + startY);

        int[,] oldShape = new int[_sizeX, _sizeY];

        Array.Copy(_shape, oldShape, _shape.Length);

        int maxSize = 5;

        for (int y = 0; y < maxSize; y++)
        {
            for (int x = 0; x < maxSize; x++)
            {
                int newX = startX + x;
                int newY = startY + y;

                int oldX = startX + maxSize - 1 - y;
                int oldY = startY + x;

                if (newX >= _sizeX || newY >= _sizeY)
                {
                    continue;
                }
                else if (oldX >= _sizeX || oldY >= _sizeY)
                {
                    _shape[newX, newY] = 0;
                    continue;
                }

                _shape[newX, newY] = oldShape[oldX, oldY];
            }
        }

        //_shape[0, 0] = oldShape[3, 0];
        //_shape[1, 0] = oldShape[3, 1];
        //_shape[2, 0] = oldShape[3, 2];
        //_shape[3, 0] = oldShape[3, 3];

        //_shape[0, 1] = oldShape[2, 0];
        //_shape[1, 1] = oldShape[2, 1];
        //_shape[2, 1] = oldShape[2, 2];
        //_shape[3, 1] = oldShape[2, 3];
    }

    private void FindStartingPoint(out int startX, out int startY)
    {
        startY = 0;
        startX = 0;

        for (int y = 0; y < _sizeY; y++)
        {
            for (int x = 0; x < _sizeX; x++)
            {
                if (startY == 0 && _shape[x, y] != 0)
                {
                    startY = y;
                }
            }
        }

        for (int x = 0; x < _sizeX; x++)
        {
            for (int y = 0; y < _sizeY; y++)
            {
                if (startX == 0 && _shape[x, y] != 0)
                {
                    startX = x;
                }
            }
        }
    }
}
