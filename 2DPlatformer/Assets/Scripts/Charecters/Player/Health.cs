public class Health
{
    public Health(float health) =>
        HealthPoints = health;

    public float HealthPoints { get; private set; }
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

    public void Heal(float health)
    {
        if (IsAlive == false)
            return;

        HealthPoints += health;
    }
}
