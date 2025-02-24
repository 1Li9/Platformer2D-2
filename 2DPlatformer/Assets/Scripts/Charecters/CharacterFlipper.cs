using UnityEngine;

public class CharacterFlipper
{
    private MonoBehaviour _context;
    private Quaternion _mirrorRotation;

    public CharacterFlipper(MonoBehaviour context)
    {
        _context= context;
        _mirrorRotation = Quaternion.Euler(0f, 180f, 0f);
    }

    public bool IsTurnedToRight { get; private set; } = true;

    public void Flip()
    {
        _context.transform.rotation *= _mirrorRotation;
        IsTurnedToRight = !IsTurnedToRight;
    }
}
