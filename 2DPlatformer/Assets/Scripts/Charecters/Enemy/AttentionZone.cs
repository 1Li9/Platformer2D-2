using UnityEngine;

public class AttentionZone : MonoBehaviour
{
    public bool IsPlayerSpotted;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player _))
            IsPlayerSpotted = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player _))
            IsPlayerSpotted = false;
    }
}
