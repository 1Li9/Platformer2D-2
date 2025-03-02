using System;
using UnityEngine;

public class AttentionZone : MonoBehaviour
{
    public event Action PlayerSpotted;
    public event Action PlayerLost;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player _))
            PlayerSpotted?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player _))
            PlayerLost?.Invoke();
    }
}
