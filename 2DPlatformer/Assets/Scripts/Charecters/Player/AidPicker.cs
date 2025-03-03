using UnityEngine;

public class AidPicker : MonoBehaviour
{
    [SerializeField] private Player _player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Aid aid))
            _player.Heal(aid.HealthPoints);
    }
}
