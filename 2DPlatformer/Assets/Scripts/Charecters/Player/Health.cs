public class Health
{
    public float HealthPoints { get; private set; }

    public Health(float health) =>
        HealthPoints = health;

    public bool IsAlive { get; private set; } = true;

    public void TakeDamage(float damage)
    {
        HealthPoints -= damage;

        if(HealthPoints <= 0)
        {
            HealthPoints = 0;
            IsAlive = false;
        }
    }
}
