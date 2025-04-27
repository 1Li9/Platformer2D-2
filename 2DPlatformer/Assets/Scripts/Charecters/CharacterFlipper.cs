using UnityEngine;

public class CharacterFlipper
{
    private Transform _context;
    private Quaternion _mirrorRotation;

    public CharacterFlipper(Transform context)
    {
        _context= context;
        _mirrorRotation = Quaternion.Euler(0f, 180f, 0f);
    }

    public bool IsTurnedToRight { get; private set; } = true;

    public void Flip()
    {
        _context.rotation *= _mirrorRotation;
        IsTurnedToRight = !IsTurnedToRight;
    }
}
