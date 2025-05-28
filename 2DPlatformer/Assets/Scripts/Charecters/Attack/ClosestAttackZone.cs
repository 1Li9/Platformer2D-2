using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ClosestAttackZone : MonoBehaviour, IAttackZone
{
    private List<IDamageble> _damagebles;

    public bool CanAttack => _damagebles.Count > 0;

    public IDamageble Damageble => FindClosest();

    private void Awake()
    {
        _damagebles = new List<IDamageble>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamageble damageble))
            _damagebles.Add(damageble);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamageble damageble) && _damagebles.Contains(damageble))
            _damagebles.Remove(damageble);
    }

    private IDamageble FindClosest()
    {
        if(_damagebles.Count == 0)
            return null;

        IDamageble closest = _damagebles.FirstOrDefault();

        foreach(IDamageble damageble in _damagebles)
        {
            if ((damageble.Transform.position - transform.position).sqrMagnitude < (closest.Transform.position - transform.position).sqrMagnitude)
                closest = damageble;
        }

        return closest;
    }
}