using UnityEngine;
using System;

public abstract class Charecter : MonoBehaviour
{
    public abstract event Action<float> HealthChanged;
    public abstract event Action<float> MaxHealthChanged;

    public abstract Health Health { get; protected set; }
}
