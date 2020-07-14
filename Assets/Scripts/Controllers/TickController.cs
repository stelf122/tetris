using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TickController : MonoBehaviour
{
    private TickModel _model;
    private List<ITickable> _controllers;

    public void Initialize(TickModel model, List<ITickable> controllers)
    {
        _model = model;
        _controllers = controllers;

        InvokeRepeating("Tick", _model.RepeatRate, _model.RepeatRate);
    }

    private void Tick()
    {
        foreach (var controller in _controllers)
        {
            controller.Tick();
        }
    }
}
