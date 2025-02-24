using System;
using UnityEngine;

public class TargetsMap : MonoBehaviour
{
    [SerializeField] private Target[] _targets;

    private int _currentTargetIndex = 0;

    public Target CurrentTarget { get; private set; }

    private void Awake()
    {
        if (_targets.Length == 0)
            throw new ArgumentOutOfRangeException(nameof(_targets) + " length equals zero");

        CurrentTarget = _targets[_currentTargetIndex];
    }

    public void SelectNextTarget()
    {
        _currentTargetIndex++;

        if (_currentTargetIndex >= _targets.Length)
            _currentTargetIndex = 0;

        CurrentTarget = _targets[_currentTargetIndex];
    }
}
