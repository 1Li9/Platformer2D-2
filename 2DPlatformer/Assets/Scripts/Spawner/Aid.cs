using UnityEngine;

public class Aid : SpawnableObject
{
    [SerializeField] private float _healthPoints;

    public float HealthPoints
    {
        get
        {
            if (CanTake == false)
                return 0;

            Interact();
            return _healthPoints;
        }
    }
}
