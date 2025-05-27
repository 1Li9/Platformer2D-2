public interface IDamageble
{
    public bool IsAlive { get; }

    public bool TryTakeDamage(float damage);
}
