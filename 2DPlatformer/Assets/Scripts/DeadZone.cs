using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private float _damage = 100f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamageble damageble))
            damageble.TryTakeDamage(_damage); 
    }
}
