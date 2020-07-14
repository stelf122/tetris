using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Scriptables/TickModel")]
public class TickModel : ScriptableObject
{
    [SerializeField] private float _repeatRate;

    public float RepeatRate { get => _repeatRate; }
}
