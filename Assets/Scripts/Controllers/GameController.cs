using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    [SerializeField] private FieldModel _fieldModel;
    [SerializeField] private FieldController _fieldController;
    [SerializeField] private TickModel _tickModel;
    [SerializeField] private TickController _tickController;

    private void Start()
    {
        _fieldController.Initialize(_fieldModel);
        _tickController.Initialize(_tickModel, new List<ITickable>()
        {
            _fieldController
        });
    }
}
