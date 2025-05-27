using UnityEngine;

public class AttackZone : MonoBehaviour
{
    public IDamageble Damageble { get; private set; }
    public bool CanAttack => Damageble != null;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IDamageble damageble))
            Damageble = damageble;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out IDamageble _))
            Damageble = null;
    }
}