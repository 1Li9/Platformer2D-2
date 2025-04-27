using System;
using UnityEngine;

public abstract class SpawnableObject : MonoBehaviour
{
    [SerializeField] private SpawnableObjectView _view;

    public event Action<SpawnableObject> WasInteracted;

    public SpawnableObjectView View { get => _view; set => _view = value; }
    public bool CanTake { get; set; } = true;

    public void Interact()
    {
        WasInteracted.Invoke(this);
    }
}