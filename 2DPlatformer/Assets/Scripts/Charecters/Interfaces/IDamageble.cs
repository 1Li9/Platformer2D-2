public interface IDamageble : ITransformable
{
    public bool IsAlive { get; }

    public bool TryTakeDamage(float damage);
}