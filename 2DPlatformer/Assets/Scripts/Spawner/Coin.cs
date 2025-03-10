using UnityEngine;

public class Coin : SpawnableObject 
{
    [SerializeField] private int _amount;
    public int Amount
    {
        get
        {
            if (CanTake == false)
                return 0;

            Interact();
            return _amount;
        }
    }
}
